namespace Wagtail.Win32;

using System;
using System.Runtime.InteropServices;


/// <summary></summary>
public static class User32
{
    //
    private const string L = "user32.dll";


    /// <summary></summary>
    [DllImport(L, EntryPoint = "EnumWindows", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Auto)]
    public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

    /// <summary></summary>
    [DllImport(L, EntryPoint = "IsWindowUnicode", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Auto)]
    public static extern bool IsWindowUnicode([In]IntPtr hWnd);

    /// <summary></summary>
    [DllImport(L, EntryPoint = "GetWindowThreadProcessId", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Auto)]
    public static extern int GetWindowThreadProcessId([In]IntPtr hWnd, [Out]out int lpdwProcessId);

    /// <summary></summary>
    [DllImport(L, EntryPoint = "GetWindowTextW", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Unicode)]
    public static extern int GetWindowTextW(
            [In]IntPtr hWnd,
            [Out]IntPtr lpString,
            [In]int nMaxCount);

    /// <summary></summary>
    [DllImport(L, EntryPoint = "GetWindowTextA", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Unicode)]
    public static extern int GetWindowTextA(
            [In]IntPtr hWnd,
            [Out]IntPtr lpString,
            [In]int nMaxCount);

    /// <summary></summary>
    [DllImport(L, EntryPoint = "GetWindowTextLengthA", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Ansi)]
    private static extern int GetWindowTextLengthA([In]IntPtr hWnd);

    /// <summary></summary>
    [DllImport(L, EntryPoint = "GetWindowTextLengthW", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Unicode)]
    private static extern int GetWindowTextLengthW([In]IntPtr hWnd);


    /// <summary></summary>
    [DllImport(L, EntryPoint = "SendMessage", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Unicode)]
    public static extern IntPtr SendMessage(
            [In]IntPtr hWnd,
            [In]uint msg,
            [In, Out]IntPtr wParam,
            [In, Out]IntPtr lParam);

    /// <summary></summary>
    public static int GetWindowTextLength(IntPtr hWnd) => IsWindowUnicode(hWnd)
        ? GetWindowTextLengthW(hWnd)
        : GetWindowTextLengthA(hWnd);

    /// <summary></summary>
    public static string? GetWindowText(IntPtr hWnd)
    {
        var length = GetWindowTextLength(hWnd);
        if(length == 0)
        {
            return null;
        }

        var isUnicode = IsWindowUnicode(hWnd);
        var size = (length + 1) * (isUnicode ? 2 : 1);

        var ptr = Marshal.AllocHGlobal(cb: size + 1);
        string? windowText = null;
        int ret;

        if(isUnicode)
        {
            ret = GetWindowTextW(hWnd, ptr, length + 1);
            if(ret != 0)
            {
                windowText = Marshal.PtrToStringUni(ptr);
            }
        }
        else
        {
            ret = GetWindowTextA(hWnd, ptr, length + 1);
            if(ret != 0)
            {
                windowText = Marshal.PtrToStringAnsi(ptr);
            }
        }
        Marshal.FreeHGlobal(ptr);

        return windowText;
    }
}
