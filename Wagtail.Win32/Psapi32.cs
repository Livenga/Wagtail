namespace Wagtail.Win32;

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using Wagtail.Win32.Enums;


/// <summary></summary>
public static class Psapi
{
    private const string L = "psapi.dll";

    /// <summary></summary>
    [DllImport(L, EntryPoint = "GetModuleFileNameExA", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Unicode)]
    public static extern int GetModuleFileNameExA([In]IntPtr hProcess, [In]IntPtr hModule, [Out]StringBuilder lpFileName, [In]int nSize);

    /// <summary></summary>
    [DllImport(L, EntryPoint = "GetModuleFileNameExW", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Unicode)]
    public static extern int GetModuleFileNameExW([In]IntPtr hProcess, [In]IntPtr hModule, [Out]StringBuilder lpFileName, [In]int nSize);


    /// <summary></summary>
    public static string GetModuleFileName(
            int processId,
            int bufferSize = 4096,
            bool isUnicode = true)
    {
        int errCode;

        var ptr = Kernel32.OpenProcess(
                OpenProcessDesiredAccess.PROCESS_QUERY_INFORMATION | OpenProcessDesiredAccess.PROCESS_VM_READ,
                false,
                processId);
        errCode = Kernel32.GetLastError();
        if(ptr == IntPtr.Zero)
        {
            throw new Win32Exception(Kernel32.GetLastError());
        }

        var sb = new StringBuilder(bufferSize);
        var ret = isUnicode
            ? GetModuleFileNameExW(ptr, IntPtr.Zero, sb, bufferSize)
            : GetModuleFileNameExA(ptr, IntPtr.Zero, sb, bufferSize);
        errCode = Kernel32.GetLastError();

        if(ret == 0)
        {
            Kernel32.CloseHandle(ptr);
            throw new Win32Exception(errCode);
        }
        Kernel32.CloseHandle(ptr);

        return sb.ToString();
    }
}
