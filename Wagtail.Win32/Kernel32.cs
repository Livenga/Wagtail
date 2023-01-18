namespace Wagtail.Win32;

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Wagtail.Win32.Enums;


/// <summary></summary>
public static class Kernel32
{
    /// <summary></summary>
    public const string L = "kernel32.dll";

    /// <summary></summary>
    [DllImport(L, EntryPoint = "GetLastError", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Auto)]
    public static extern int GetLastError();

    /// <summary></summary>
    [DllImport(L, EntryPoint = "OpenProcess", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Auto)]
    public static extern IntPtr OpenProcess(
            [In]OpenProcessDesiredAccess dwDesiredAccess,
            [In]bool bInheritHandle,
            [In]int dwProcessId);

    /// <summary></summary>
    [DllImport(L, EntryPoint = "GetCurrentProcess", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Auto)]
    public static extern IntPtr GetCurrentProcess();

    /// <summary></summary>
    [DllImport(L, EntryPoint = "CloseHandle", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Auto)]
    public static extern bool CloseHandle(IntPtr hObject);
}
