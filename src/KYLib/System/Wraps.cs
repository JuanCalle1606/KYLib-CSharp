using System.Runtime.InteropServices;

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
}