namespace Wagtail.Injection;

using CommandLine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Automation;
using Wagtail32 = Wagtail.Win32;


/// <summary></summary>
internal class Program
{
    //
    private static List<WindowInfo> windowInfos = new ();

    /// <summary></summary>
    static void Main(string[] args)
    {
        string vcrId = "303127";
        string key = "Wagtail.ProcessWrapperForm";
        if(args.Any())
        {
            var parsed = CommandLine.Parser.Default.ParseArguments<Options>(args);
            switch(parsed.Tag)
            {
                case ParserResultType.Parsed:
                    var opts = parsed.Value;
                    key   = opts.Key ?? throw new NullReferenceException();
                    vcrId = opts.VcrId ?? throw new NullReferenceException();
                    break;
            }
        }

        Wagtail32.User32.EnumWindows(OnEnumWindows, IntPtr.Zero);
        var wi = windowInfos.FirstOrDefault(wi => wi.Text.Contains(key) && (wi.ModuleName?.Contains(key) ?? false));
        if(wi != null)
        {
            var aeRoot = AutomationElement.FromHandle(hwnd: wi.Handle);
#if DEBUG
            Console.Error.WriteLine($"{aeRoot.Current.Name} {aeRoot.Current.NativeWindowHandle}");
#endif

            var aeProcessText = aeRoot.FindFirst(
                    TreeScope.Subtree,
                    new PropertyCondition(AutomationElement.AutomationIdProperty, "processText"));
            var aeArgsText = aeRoot.FindFirst(
                    TreeScope.Subtree,
                    new PropertyCondition(AutomationElement.AutomationIdProperty, "argsText"));

            var aeExecuteButton = aeRoot.FindFirst(
                    TreeScope.Subtree,
                    new PropertyCondition(AutomationElement.AutomationIdProperty, "executeButton"));

            if(aeProcessText == null || aeArgsText == null)
            {
                Console.Error.WriteLine($"ProcessText({aeProcessText?.Current.NativeWindowHandle:X}) ArgsText({aeArgsText?.Current.NativeWindowHandle:X})");
                return;
            }
            Console.Error.WriteLine($"ProcessText Handle: {aeProcessText.Current.NativeWindowHandle}");
            Console.Error.WriteLine($"ArgsText Handle:    {aeArgsText.Current.NativeWindowHandle}");

            var hProcessText = new IntPtr(aeProcessText.Current.NativeWindowHandle);
            var hArgsText = new IntPtr(aeArgsText.Current.NativeWindowHandle);

            try
            {
                var path = GetScriptPathByVcrId(vcrId: vcrId);

                SetTextFromHandle(hProcessText, path);
                SetTextFromHandle(hArgsText, string.Empty);
            }
            catch(Exception ex)
            {
#if DEBUG
                Console.Error.WriteLine($"{ex.GetType().FullName} {ex.Message}");
#endif
                Console.Error.WriteLine($"Vcr ID \"{vcrId}\" に対応するスクリプトファイルが見つかりません.");
                return;
            }

            // ボタンの押下
            Wagtail32.User32.SendMessage(
                    new IntPtr(aeExecuteButton.Current.NativeWindowHandle),
                    0x00F5,
                    IntPtr.Zero,
                    IntPtr.Zero);

            Console.Error.WriteLine($"Please Any Keys...");
            Console.ReadKey();
        }
    }

    /// <summary></summary>
    private static string GetScriptPathByVcrId(string vcrId)
    {
        var rgx = new Regex("^(?<VcrId>[0-9]{1,8})\\.cmd$", RegexOptions.Compiled);

        var scriptDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "scripts");

        return Directory
            .GetFiles(path: scriptDirectory)
            .FirstOrDefault(f => rgx.IsMatch(Path.GetFileName(f))
                    && rgx.Match(Path.GetFileName(f)).Groups["VcrId"].Value == vcrId) ?? throw new NullReferenceException();
    }

    /// <summary></summary>
    private static string GetTextFromHandle(IntPtr hWnd)
    {
        int errCode;
        var textLength = (int)Wagtail32.User32.SendMessage(hWnd, 0x000e, IntPtr.Zero, IntPtr.Zero);
        errCode = Wagtail32.Kernel32.GetLastError();

        if(textLength == 0)
        {
            return errCode == 0
                ? string.Empty
                : throw new Win32Exception(errCode); 
        }
#if DEBUG
        Debug.WriteLine($"{errCode}\t{textLength}");
#endif

        var ptr = Marshal.AllocHGlobal(cb: (textLength + 1) * 2);
        var copiedLength = (int)Wagtail32.User32.SendMessage(hWnd, 0x000d, new IntPtr(textLength + 1), ptr);
        var str = Marshal.PtrToStringUni(ptr, copiedLength);

        Marshal.FreeHGlobal(ptr);

        return str;
    }

    /// <summary></summary>
    private static void SetTextFromHandle(
            IntPtr hWnd,
            string value)
    {
        var ptr = Marshal.StringToHGlobalUni(s: value);

        Wagtail32.User32.SendMessage(hWnd, 0x000c, IntPtr.Zero, ptr);

        Marshal.FreeHGlobal(ptr);
    }


    /// <summary></summary>
    private static bool OnEnumWindows(IntPtr hWnd, IntPtr lParam)
    {
        var windowText = Wagtail32.User32.GetWindowText(hWnd);
#if DEBUG
        Console.Error.WriteLine($"\t{windowText}");
#endif
        if(windowText == null)
        {
            return true;
        }

        int processId = 0;
        var threadId = Wagtail32.User32.GetWindowThreadProcessId(hWnd, out processId);
        string? moduleName = null;
        try
        {
            moduleName = Wagtail32.Psapi.GetModuleFileName(processId);
        }
        catch { }

        windowInfos.Add(
                new WindowInfo(hWnd, processId, threadId, windowText, moduleName));

        return true;
    }


    /// <summary></summary>
    private sealed class WindowInfo
    {
        /// <summary></summary>
        public IntPtr Handle => _handle;

        /// <summary></summary>
        public int ProcessId => _processId;

        /// <summary></summary>
        public int ThreadId => _threadId;

        /// <summary></summary>
        public string Text => _text;

        /// <summary></summary>
        public string? ModuleName => _moduleName;


        private readonly IntPtr _handle;
        private readonly int _processId;
        private readonly int _threadId;
        private readonly string _text;
        private readonly string? _moduleName;


        /// <summary></summary>
        public WindowInfo(
                IntPtr handle,
                int processId,
                int threadId,
                string text,
                string? moduleName)
        {
            _handle = handle;
            _processId = processId;
            _threadId = threadId;
            _text = text;
            _moduleName = moduleName;
        }
    }
}
