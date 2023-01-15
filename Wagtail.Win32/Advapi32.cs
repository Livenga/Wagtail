namespace Wagtail.Win32;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;


/// <summary></summary>
public static class Advapi32
{
    public const string L = "Advapi32.dll";


    /// <summary></summary>
    [DllImport(L, EntryPoint = "OpenProcessToken", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Auto)]
    public static extern bool OpenProcessToken(
            [In]IntPtr hProcess,
            [In]int dwDesiredAccess,
            [Out]out IntPtr hToken);

    /// <summary></summary>
    [DllImport(L, EntryPoint = "InitiateSystemShutdownExW", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Unicode)]
    public static extern bool InitiateSystemShutdownEx(
            [In]string? lpMachineName,
            [In]string? lpMessage,
            [In]int dwTimeout,
            [In]bool bForceAppsClosed,
            [In]bool bRebootAfterShutdown,
            [In]ShutDownReason dwReason);

    /// <summary></summary>
    [DllImport(L, EntryPoint = "LookupPrivilegeValueW", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Unicode)]
    private static extern bool _LookupPrivilegeValueW(
            [In]string? lpSystemName,
            [In]string lpName,
            [Out]IntPtr lpLuid);

    /// <summary></summary>
    [DllImport(L, EntryPoint = "LookupPrivilegeDisplayNameW", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Unicode)]
    public static extern bool _LookupPrivilegeDisplayNameW(
            [In]string? lpSystemName,
            [In]string lpName,
            [Out]IntPtr lpDisplayName,
            [In, Out]ref int cchDisplayName,
            [Out]out int lpLanguageId);

    /// <summary></summary>
    [DllImport(L, EntryPoint = "AdjustTokenPrivileges", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Auto)]
    public static extern bool _AdjustTokenPrivileges(
            [In]IntPtr hToken,
            [In]bool bDisableAllPrivileges,
            [In]IntPtr hNewState,
            [In]int dwBufferLength,
            [Out]IntPtr hPreviousState,
            [Out]IntPtr dwReturnLength);



    /// <summary>
    /// </summary>
    public static Luid LookupPrivilegeValue(
            string? systemName,
            string name)
    {
        var size = Marshal.SizeOf<Luid>();
        var ptr = Marshal.AllocHGlobal(cb: size);
#if DEBUG
        Debug.WriteLine($"DEBUG | {name} {size} {ptr.ToInt():X}");
#endif

        if(! _LookupPrivilegeValueW(systemName, name, ptr))
        {
            var eCode = Kernel32.GetLastError();
#if DEBUG
            Debug.WriteLine($"{name} ({eCode})");
#endif
            Marshal.FreeHGlobal(ptr);
            throw new Win32Exception(eCode);
        }

        Debug.WriteLine($"{Marshal.ReadInt32(ptr)} {Marshal.ReadInt32(ptr + Marshal.SizeOf<int>())}");

        //var luid = (Luid)Marshal.PtrToStructure(ptr, typeof(Luid));
        var luid = Marshal.PtrToStructure<Luid>(ptr);
        //Marshal.PtrToStructure<Luid>(ptr, luid);

        Marshal.FreeHGlobal(ptr);

        return luid;
    }

    /// <summary>
    /// </summary>
    public static string LookupPrivilegeDisplayName(
            string? systemName,
            string name)
    {
        int displayNameLength = 0, languageId = 0;

        _LookupPrivilegeDisplayNameW(systemName, name, IntPtr.Zero, ref displayNameLength, out languageId);
        if(displayNameLength == 0)
        {
            throw new Win32Exception(Kernel32.GetLastError());
        }

        var ptr = Marshal.AllocHGlobal(displayNameLength * 2 + 1);
        if(! _LookupPrivilegeDisplayNameW(systemName, name, ptr, ref displayNameLength, out languageId))
        {
            var eCode = Kernel32.GetLastError();
            Marshal.FreeHGlobal(ptr);
            throw new Win32Exception(eCode);
        }

        var displayName = Marshal.PtrToStringUni(ptr);
        Marshal.FreeHGlobal(ptr);

        return displayName;
    }

    /// <summary></summary>
    public static bool AdjustTokenPrivileges(
            IntPtr hToken,
            bool disableAllPrivileges,
            TokenPrivileges newState)
    {
        var size = Marshal.SizeOf<int>()
            + (Marshal.SizeOf<LuidAndAttributes>() * newState.PrivilegeCount);

        var ptr = Marshal.AllocHGlobal(cb: size);
        Marshal.WriteInt32(ptr, newState.PrivilegeCount);
        var offset = Marshal.SizeOf<int>();
        for(var i = 0; i < newState.PrivilegeCount; ++i)
        {
            Marshal.StructureToPtr<LuidAndAttributes>(newState.Privileges[i], ptr + offset, true);
            offset += Marshal.SizeOf<LuidAndAttributes>();
        }
#if DEBUG
        Debug.WriteLine($"DEBUG | Token Hex => {string.Join(" ", Enumerable.Range(0, size).Select(idx => $"{Marshal.ReadByte(ptr, idx):X2}"))}");
#endif

        var result = _AdjustTokenPrivileges(hToken, false, ptr, 0, IntPtr.Zero, IntPtr.Zero);
#if DEBUG
#endif
        Marshal.FreeHGlobal(ptr);
        if(! result)
        {
            throw new Win32Exception(Kernel32.GetLastError());
        }

        return true;
    }


}
