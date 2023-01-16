namespace Wagtail.ProcessWrapperForm;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


/// <summary></summary>
public partial class MainForm : Form
{
    /// <summary></summary>
    public MainForm()
    {
        InitializeComponent();
    }


#region private Events
    /// <summary></summary>
    private void OnShown(object source, EventArgs e)
    {
        Location = new Point(0, 0);
    }

    private bool _isProcessing = false;
    private Process? _process = null;

    /// <summary></summary>
    private void OnExecuteClick(object source, EventArgs e)
    {
        if(_isProcessing)
        {
            return;
        }

        var processPath = processText.Text;
        var args = argsText.Text;

        if(string.IsNullOrEmpty(processPath))
        {
            return;
        }

        // プロセス実行
        _isProcessing = true;
        executeButton.Enabled = false;

        var psi = new ProcessStartInfo(processPath, args)
        {
            UseShellExecute = false,
            RedirectStandardError = true,
            StandardErrorEncoding = Encoding.UTF8,
        };

        _process = new Process()
        {
            EnableRaisingEvents = true,
            StartInfo = psi,
        };
        _process.Exited += OnProcessExited;

        _process.Start();
    }

    /// <summary></summary>
    private void OnProcessExited(object source, EventArgs e)
    {
#if DEBUG
        Debug.WriteLine($"{GetType().FullName}.{nameof(OnProcessExited)}");
#endif
        _isProcessing = false;
        InvokeSwitchControlEnabled(executeButton, true);

        if(source is Process p)
        {
            p.Dispose();
        }
    }
#endregion

    /// <summary></summary>
    private void InvokeSwitchControlEnabled(Control ctrl, bool isEnabled)
    {
        if(InvokeRequired)
        {
            Invoke(InvokeSwitchControlEnabled, new object[] { ctrl, isEnabled });
            return;
        }

        ctrl.Enabled = isEnabled;
    }
}
