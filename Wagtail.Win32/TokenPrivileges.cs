namespace Wagtail.Win32;

using System;
using System.Runtime.InteropServices;


/// <summary></summary>
[StructLayout(LayoutKind.Sequential)]
public struct TokenPrivileges
{
    /// <summary></summary>
    public int PrivilegeCount;

    /// <summary></summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public LuidAndAttributes[] Privileges;
}

/// <summary></summary>
[StructLayout(LayoutKind.Sequential)]
public struct LuidAndAttributes
{
    /// <summary></summary>
    public Luid Luid;

    /// <summary></summary>
    public int Attributes;
}
