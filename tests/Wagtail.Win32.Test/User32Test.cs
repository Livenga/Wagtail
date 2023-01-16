namespace Wagtail.Win32.Test;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Wagtail;
using Wagtail32 = Wagtail.Win32;


/// <summary></summary>
[TestClass]
public class User32Test
{
    /// <summary></summary>
    [TestMethod]
    public void EnumWindowsTest()
    {
        Wagtail32.User32.EnumWindows(OnEnumWindows, IntPtr.Zero);
    }

    /// <summary></summary>
    private bool OnEnumWindows(IntPtr hWnd, IntPtr lParam)
    {
        int pid = 0;

        var tid = Wagtail32.User32.GetWindowThreadProcessId(hWnd, out pid);
        Console.WriteLine($"{hWnd.ToInt():X}({pid}:{tid}) {Wagtail32.User32.GetWindowText(hWnd)}");
        return true;
    }
}
