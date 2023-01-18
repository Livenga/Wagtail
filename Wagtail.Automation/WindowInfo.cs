namespace Wagtail.Automation;

using Wagtail.Win32;
using System;
using System.Collections.Generic;


/// <summary></summary>
public class WindowInfo
{
    /// <summary></summary>
    public IntPtr Handle => _handle;

    /// <summary></summary>
    public string? Text => _text;

    /// <summary></summary>
    public int ProcessId => _processId;

    /// <summary></summary>
    public int ThreadId => _threadId;

    /// <summary></summary>
    public bool IsUnicode => User32.IsWindowUnicode(_handle);

    /// <summary></summary>
    public IntPtr ParentHandle => User32.GetParent(_handle);

    /// <summary></summary>
    public string ClassName => User32.GetClassName(_handle, 1024);


    private readonly IntPtr _handle;
    private readonly string? _text;
    private readonly int _processId;
    private readonly int _threadId;


    /// <summary></summary>
    private WindowInfo(
            IntPtr  handle,
            string? text,
            int     processId,
            int     threadId)
    {
        _handle    = handle;
        _text      = text;
        _processId = processId;
        _threadId  = threadId;
    }


    /// <summary></summary>
    public string? TryGetModuleFileName(
            int size = 4096,
            string? defaultValue = null)
    {
        try
        {
            return Psapi.GetModuleFileName(_processId, size, true);
        }
        catch
        {
            return defaultValue;
        }
    }

    private static List<WindowInfo>? _windowInfos = null;
    /// <summary></summary>
    public static IEnumerable<WindowInfo> GetWindows(IntPtr hWnd)
    {
        if(_isProcessing)
        {
            throw new Exception();
        }

        _windowInfos?.Clear();
        _windowInfos = new ();

        var ret = hWnd == IntPtr.Zero
            ? User32.EnumWindows(OnEnumWindows, IntPtr.Zero)
            : User32.EnumChildWindows(hWnd, OnEnumWindows, IntPtr.Zero);

        _isProcessing = false;
        return _windowInfos.ToArray();
    }


    private static bool _isProcessing = false;
    /// <summary></summary>
    private static bool OnEnumWindows(IntPtr hWnd, IntPtr lParam)
    {
        if(_isProcessing)
        {
            return false;
        }

        int pid = 0;
        var threadId = User32.GetWindowThreadProcessId(hWnd, out pid);

        _windowInfos?.Add(
                new WindowInfo(
                    handle:    hWnd,
                    text:      User32.GetWindowText(hWnd),
                    processId: pid,
                    threadId:  threadId));

        return true;
    }
}
