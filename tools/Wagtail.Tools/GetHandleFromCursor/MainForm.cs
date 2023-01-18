namespace GetHandleFromCursor;

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wagtail;
using Wagtail.Win32;


/// <summary></summary>
public partial class MainForm : Form
{
    private IntPtr _handle = IntPtr.Zero;
    private CancellationTokenSource? _tokenSource = null;


    /// <summary></summary>
    public MainForm()
    {
        InitializeComponent();
    }

    #region private Events
    /// <summary></summary>
    private void OnShown(object source, EventArgs e)
    {
        _handle = Handle;
        Task.Run(CursorMonitorAsync);
    }

    /// <summary></summary>
    private void OnFormClosing(object source, FormClosingEventArgs e)
    {
        _tokenSource?.Cancel(true);
        _tokenSource?.Dispose();
        _tokenSource = null;
        // e.Cancel = true;
    }
    #endregion

    /// <summary></summary>
    private async Task CursorMonitorAsync()
    {
        if(_tokenSource != null)
        {
            return;
        }

        var interval = 1000 / 5;
        bool isRunning = true;

        var ptCursor = new Wagtail.Win32.Point();
        var rectWindow = new Wagtail.Win32.Rect();
        _tokenSource = new();

        while(isRunning)
        {
#if DEBUG
            Debug.WriteLine($"DEBUG | Running...");
#endif
            if(Wagtail.Win32.User32.GetWindowRect(_handle, out rectWindow)
                    && Wagtail.Win32.User32.GetCursorPos(out ptCursor))
            {
                var cX = (rectWindow.Left + rectWindow.Right) / 2;
                var cY = (rectWindow.Top + rectWindow.Bottom) / 2;

                var requiredX = (cX < ptCursor.X
                        ? ptCursor.X - (rectWindow.Right + 32)
                        : (rectWindow.Left - 32) - ptCursor.X) < 0;
                var requiredY = (cY < ptCursor.Y
                        ? ptCursor.Y - (rectWindow.Bottom + 32)
                        : (rectWindow.Top - 32) - ptCursor.X) < 0;

                if(requiredX && requiredY)
                {
                    var x = requiredX ? ptCursor.X + 16 : rectWindow.Left;
                    var y = requiredY ? ptCursor.Y + 16 : rectWindow.Top;

                    User32.MoveWindow(_handle, x, y, rectWindow.Right - rectWindow.Left, rectWindow.Bottom - rectWindow.Top, true);
                }
            }

            try
            {
                await Task.Delay(interval, _tokenSource?.Token ?? throw new NullReferenceException());
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"ERROR | {ex.GetType().FullName} {ex.Message}");
                Debug.WriteLine(ex.StackTrace);

                isRunning = false;
            }
        }
    }
}
