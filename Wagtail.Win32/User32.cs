namespace Wagtail.Win32;

using System;
using System.ComponentModel;
using System.Text;
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
    [DllImport(L, EntryPoint = "EnumChildWindows", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Auto)]
    public static extern bool EnumChildWindows(
            [In]IntPtr hWndParent,
            [In]EnumWindowsProc lpEnumFunc,
            [In]IntPtr lParam);


    /// <summary></summary>
    [DllImport(L, EntryPoint = "IsWindowUnicode", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Auto)]
    public static extern bool IsWindowUnicode([In]IntPtr hWnd);

    /// <summary></summary>
    [DllImport(L, EntryPoint = "GetParent", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Auto)]
    public static extern IntPtr GetParent([In]IntPtr hWnd);

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
    [DllImport(L, EntryPoint = "GetClassNameA", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Ansi)]
    public static extern int GetClassNameA([In]IntPtr hWnd, [Out]StringBuilder lpClassName, [In]int nMaxCount);

    /// <summary></summary>
    [DllImport(L, EntryPoint = "GetClassNameW", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Unicode)]
    public static extern int GetClassNameW([In]IntPtr hWnd, [Out]StringBuilder lpClassName, [In]int nMaxCount);


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
    [DllImport(L, EntryPoint = "GetCursorPos", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Auto)]
    public static extern bool GetCursorPos([Out]out Point lpPoint);

    /// <summary></summary>
    [DllImport(L, EntryPoint = "MoveWindow", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Auto)]
    public static extern bool MoveWindow([In]IntPtr hWnd, [In]int x, [In]int y, [In]int nWidht, [In]int nHeight, [In]bool bRepaint);

    /// <summary></summary>
    [DllImport(L, EntryPoint = "WindowFromPoint", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Auto)]
    public static extern IntPtr WindowFromPoint([In]Point point);

    /// <summary></summary>
    [DllImport(L, EntryPoint = "ChildWindowFromPointEx", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Auto)]
    public static extern IntPtr ChildWindowFromPointEx([In]IntPtr hWnd, [In]Point pt, [In]int flags);

    /// <summary></summary>
    [DllImport(L, EntryPoint = "GetWindowRect", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Auto)]
    public static extern bool GetWindowRect([In]IntPtr hWnd, [Out]out Rect rect);

    /// <summary></summary>
    [DllImport(L, EntryPoint = "RealChildWindowFromPoint", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Auto)]
    public static extern IntPtr RealChildWindowFromPoint([In]IntPtr hWnd, [In]Point pt);

    /// <summary></summary>
    [DllImport(L, EntryPoint = "ScreenToClient", SetLastError = true, ExactSpelling = false, CharSet = CharSet.Auto)]
    public static extern bool ScreenToClient([In]IntPtr hWnd, [In, Out]ref Point lpPoint);

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

    /// <summary></summary>
    public static string GetClassName(IntPtr hWnd, int length = 1024)
    {
        var isUnicode = IsWindowUnicode(hWnd);
        var size = length * (isUnicode ? 2 : 1);
        var sb = new StringBuilder(size);
        var ret = isUnicode
            ? GetClassNameW(hWnd, sb, length)
            : GetClassNameA(hWnd, sb, length);

        if(ret == 0)
        {
            throw new Win32Exception(ret);
        }

        return sb.ToString();
    }
}
