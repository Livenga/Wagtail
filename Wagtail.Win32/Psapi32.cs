namespace Wagtail32.Win32;

using System;
using System.Runtime.InteropServices;


/// <summary></summary>
public static class Psapi32
{
    private const string L = "psapi32.dll";

    /// <summary></summary>
    [DllImport(L, EntryPoint = "GetModuleFileNameExW", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Unicode)]
    public static extern int GetModuleFileNameExW(
            [In]IntPtr hProcess,
            [In]IntPtr hModule,
            [Out]IntPtr lpFileName,
            [In]int nSize);



    /// <summary></summary>
    public static string GetModuleFileName(int processId)
    {
        throw new NotImplementedException();
    }
}
