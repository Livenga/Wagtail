namespace Wagtail.Shutdown;

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wagtail.Win32;


/// <summary></summary>
internal class Program
{
    /// <summary></summary>
    static void Main(string[] args)
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

        var result = Advapi32.InitiateSystemShutdownEx(null, null, 15, true, false, ShutDownReason.FLAG_PLANNED);
        Console.WriteLine($"{result} {Kernel32.GetLastError()}");

        Kernel32.CloseHandle(hToken);
    }
}
