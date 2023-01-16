namespace Wagtail.Win32;

using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.InteropServices;


/// <summary></summary>
public static class Psapi
{
    private const string L = "psapi.dll";

    /// <summary></summary>
    [DllImport(L, EntryPoint = "GetModuleFileNameExW", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Unicode)]
    public static extern int GetModuleFileNameExW(
            [In]IntPtr hProcess,
            [In]IntPtr hModule,
            [Out]IntPtr lpFileName,
            [In]int nSize);


    /// <summary></summary>
    public static string GetModuleFileName(
            int processId,
            int bufferSize = 1024,
            bool isUnicode = true)
    {
        int errCode;

        var ptr = Kernel32.OpenProcess(0x0400 | 0x0010, false, processId);
        errCode = Kernel32.GetLastError();
        if(ptr == IntPtr.Zero)
        {
            throw new Win32Exception(Kernel32.GetLastError());
        }

        var lpFileName = Marshal.AllocHGlobal(cb: bufferSize);
        var ret = GetModuleFileNameExW(ptr, IntPtr.Zero, lpFileName, bufferSize);
        errCode = Kernel32.GetLastError();

#if DEBUG
        Debug.WriteLine($"DEBUG | {nameof(GetModuleFileNameExW)} Length: {ret} (Error Code:{errCode}) {new Win32Exception(errCode).Message}");
#endif
        if(ret == 0)
        {
#if DEBUG
            Debug.WriteLine($"WARN | Process ID = {processId}");
#endif

            Marshal.FreeHGlobal(lpFileName);
            Kernel32.CloseHandle(ptr);

            throw new Win32Exception(errCode);
        }

        var moduleFileName = Marshal.PtrToStringUni(lpFileName, ret);
        Marshal.FreeHGlobal(lpFileName);

        Kernel32.CloseHandle(ptr);

        return moduleFileName;
    }
}
