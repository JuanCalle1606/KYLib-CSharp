using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static KYLib.System.Windows;

namespace KYLib.System;

#pragma warning disable IDE1006
internal static class Linux
{
	[DllImport("libc")]
	internal static extern int getuid();
}
#pragma warning restore IDE1006
internal static class Windows
{
	/// <summary>
	/// Tests whether the current user is a member of the Administrator's group.
	/// </summary>
	/// <returns>Returns TRUE if the user is a member of the Administrator's group; otherwise, FALSE.</returns>
	/// <remarks>
	/// This function is a wrapper for CheckTokenMembership.
	/// It is recommended to call that function directly to determine Administrator group status rather than calling IsUserAnAdmin.
	///' </remarks>
	[DllImport("shell32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	internal static extern bool IsUserAnAdmin();

	[StructLayout(LayoutKind.Sequential)]
	public struct ProcessBasicInformation
	{
		public IntPtr Reserved1;
		public IntPtr PebBaseAddress;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public IntPtr[] Reserved2;
		public IntPtr UniqueProcessId;
		public IntPtr Reserved3;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct UnicodeString
	{
		public ushort Length;
		public ushort MaximumLength;
		public IntPtr Buffer;
	}

	// This is not the real struct!
	// I faked it to get ProcessParameters address.
	// Actual struct definition:
	// https://docs.microsoft.com/en-us/windows/win32/api/winternl/ns-winternl-peb
	[StructLayout(LayoutKind.Sequential)]
	public struct PEB
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public IntPtr[] Reserved;
		public IntPtr ProcessParameters;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct RtlUserProcessParameters
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public byte[] Reserved1;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
		public IntPtr[] Reserved2;
		public UnicodeString ImagePathName;
		public UnicodeString CommandLine;
	}

	public enum PROCESSINFOCLASS
	{
		ProcessBasicInformation = 0x00,
		ProcessDebugPort = 0x07,
		ProcessExceptionPort = 0x08,
		ProcessAccessToken = 0x09,
		ProcessWow64Information = 0x1A,
		ProcessImageFileName = 0x1B,
		ProcessDebugObjectHandle = 0x1E,
		ProcessDebugFlags = 0x1F,
		ProcessExecuteFlags = 0x22,
		ProcessInstrumentationCallback = 0x28,
		MaxProcessInfoClass = 0x64
	}

	[DllImport("ntdll.dll")]
	public static extern int NtQueryInformationProcess(IntPtr hProcess,
													PROCESSINFOCLASS pic,
													out ProcessBasicInformation pbi,
													int cb,
													out int pSize);


	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool ReadProcessMemory(
		IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer,
		uint nSize, out uint lpNumberOfBytesRead);

	public static bool ReadStructFromProcessMemory<TStruct>(
		IntPtr hProcess, IntPtr lpBaseAddress, out TStruct? val)
	{
		val = default;
		var structSize = Marshal.SizeOf<TStruct>();
		var mem = Marshal.AllocHGlobal(structSize);
		try
		{
			if (ReadProcessMemory(
				hProcess, lpBaseAddress, mem, (uint)structSize, out var len) &&
				(len == structSize))
			{
				val = Marshal.PtrToStructure<TStruct>(mem);
				return true;
			}
		}
		finally
		{
			Marshal.FreeHGlobal(mem);
		}
		return false;
	}

	public static bool ReadUnicodeStringFromProccessMemory(IntPtr hProcess, UnicodeString str, out string? val)
	{
		val = string.Empty;
		var strLen = str.MaximumLength;
		var mem = Marshal.AllocHGlobal(strLen);

		try
		{
			if (ReadProcessMemory(hProcess, str.Buffer, mem, strLen, out var len))
			{
				val = Marshal.PtrToStringUni(mem);
				return true;
			}
		}
		finally
		{
			Marshal.FreeHGlobal(mem);
		}
		return false;
	}
}