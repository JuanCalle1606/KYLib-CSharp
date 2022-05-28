using KYLib.System;
using KYLib.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;


namespace KYLib.Extensions;
public static class ProcessExtensions
{
	public static string CommandLine(this Process process)
	{
		Ensure.NotNull(process, nameof(process));
		if (Info.CurrentSystem.IsWindows())
		{
			IntPtr ptr;
			try
			{
				ptr = process.Handle;
			}
			catch (Exception)
			{
				return string.Empty;
			}


			if (Windows.NtQueryInformationProcess(ptr,
											Windows.PROCESSINFOCLASS.ProcessBasicInformation,
											out Windows.ProcessBasicInformation pbi,
											Marshal.SizeOf<Windows.ProcessBasicInformation>(),
											out var _) != 0)
				return string.Empty;
			if (pbi.PebBaseAddress == IntPtr.Zero) return string.Empty;

			if (Windows.ReadStructFromProcessMemory<Windows.PEB>(ptr, pbi.PebBaseAddress, out var peb))
				if (Windows.ReadStructFromProcessMemory<Windows.RtlUserProcessParameters>(ptr, peb.ProcessParameters, out var rupp))
					if (Windows.ReadUnicodeStringFromProccessMemory(ptr, rupp.CommandLine, out var cmd))
						return cmd ?? string.Empty;
		}
		else if(Info.CurrentSystem.IsLinux())
		{
			return File.ReadAllText($"/proc/{process.Id}/cmdline");
		}

		return string.Empty;
	}
}
