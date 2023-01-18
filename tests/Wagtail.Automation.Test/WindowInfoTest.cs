namespace Wagtail.Automation.Test;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Wagtail.Automation;


/// <summary></summary>
[TestClass]
public class WindowInfoTest
{
    /// <summary></summary>
    [TestMethod]
    public void GetWindowTest()
    {
        var wis = WindowInfo.GetWindows(IntPtr.Zero);
        foreach(var wi in wis)
        {
            Console.WriteLine($"{wi.ProcessId} {wi.ThreadId} {wi.Handle.ToInt():X} {wi.Text}");
        }
    }


    /// <summary></summary>
    [TestMethod]
    public void GetChildWindowTest()
    {
        var wis = WindowInfo.GetWindows(IntPtr.Zero);
        foreach(var wi in wis)
        {
            Console.WriteLine($"| {wi.ProcessId}({wi.ThreadId}) {wi.Handle.ToInt():X} {wi.Text}\t{wi.ClassName}");
            Console.WriteLine($"\t\t{TryGet<int, string>(pid => Wagtail.Win32.Psapi.GetModuleFileName(pid), wi.ProcessId, "catchNullValue")}");

            var cs = WindowInfo.GetWindows(wi.Handle);
            foreach(var c in cs)
            {
                Console.WriteLine($"\t{c.ProcessId}({c.ThreadId}) {c.Handle.ToInt():X} {c.Text}\t{c.ClassName}");
            }
        }
    }


    /// <summary></summary>
    private T? TryGet<A, T>(Func<A, T> func, A arg, T? defaultValue = null) where T : class
    {
        try
        {
            return func(arg);
        }
        catch
        {
            return defaultValue;
        }
    }
}
