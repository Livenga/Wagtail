namespace Wagtail.Win32.Test;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Wagtail;


/// <summary></summary>
[TestClass]
public class Advapi32Test
{
    /// <summary></summary>
    [TestMethod]
    public void AdjustTokenPrivileges()
    {
        var hToken = IntPtr.Zero;

        if(! Advapi32.OpenProcessToken(
                    hProcess: Kernel32.GetCurrentProcess(),
                    dwDesiredAccess: 0x0020 | 0x0008,
                    hToken: out hToken))
        {
            throw new Win32Exception(Kernel32.GetLastError());
        }
        Debug.WriteLine($"DEBUG | Token : {hToken.ToInt():X}");

        var token = new TokenPrivileges();
        token.PrivilegeCount = 1;
        token.Privileges = new LuidAndAttributes[1];
        token.Privileges[0].Attributes = 0x0002;
        token.Privileges[0].Luid = Advapi32.LookupPrivilegeValue(null, PrivilegeConstant.SE_SHUTDOWN_NAME);

        Advapi32.AdjustTokenPrivileges(hToken, false, token);

        var result = Advapi32.InitiateSystemShutdownEx(null, null, 0, true, true, ShutDownReason.FLAG_PLANNED);
        Console.WriteLine($"{result} {Kernel32.GetLastError()}");

        Kernel32.CloseHandle(hToken);
    }

    /// <summary></summary>
    [TestMethod]
    public void LookupPrivilegeValuesTest()
    {
        var flags = BindingFlags.Public | BindingFlags.Static;
        var privilegeValues = typeof(PrivilegeConstant).GetFields(flags)
            .Select(f => f.GetValue(null))
            .Cast<string>();

        foreach(var pv in privilegeValues)
        {
            try
            {
                var luid = Advapi32.LookupPrivilegeValue(null, pv);
                Console.WriteLine($"{pv} {luid.HighPart:X} {luid.LowPart:X}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR | {pv} {ex.Message}");
            }
        }
    }

    /// <summary></summary>
    [TestMethod]
    public void LookupPrivilegeDisplayNameTest()
    {
        var flags = BindingFlags.Public | BindingFlags.Static;
        var privilegeValues = typeof(PrivilegeConstant).GetFields(flags)
            .Select(f => f.GetValue(null))
            .Cast<string>();

        foreach(var pv in privilegeValues)
        {
            try
            {
                var displayName = Advapi32.LookupPrivilegeDisplayName(null, pv);
                Console.WriteLine($"{pv}\t{displayName}");
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine($"ERROR | {pv} {ex.Message}");
                Console.Error.WriteLine(ex.StackTrace);
            }
        }
    }


}
