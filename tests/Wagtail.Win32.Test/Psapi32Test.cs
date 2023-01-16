namespace Wagtail.Win32;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Wagtail;
using Wagtail32 = Wagtail.Win32;


/// <summary></summary>
[TestClass]
public class Psapi32Test
{
    /// <summary></summary>
    [TestMethod]
    public void GetWindowModuleNames()
    {
        User32.EnumWindows(OnEnumWindows, IntPtr.Zero);
    }


    /// <summary></summary>
    private bool OnEnumWindows(IntPtr hWnd, IntPtr lParam)
    {
        int processId = 0;
        var threadId = User32.GetWindowThreadProcessId(hWnd, out processId);

        try
        {
            var moduleFileName = Psapi.GetModuleFileName(processId, 1024, true);
            Console.WriteLine($"{processId} {moduleFileName}");
        }
        catch(Exception ex)
        {
            Console.Error.WriteLine($"ERROR({processId}) | {ex.GetType().FullName} {ex.Message}");
            Console.Error.WriteLine(ex.StackTrace);
        }

        return true;
    }
}
