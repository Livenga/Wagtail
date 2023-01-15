namespace Wagtail.Win32;

using System.Runtime.InteropServices;

/// <summary></summary>
[StructLayout(LayoutKind.Sequential)]
public struct Luid
{
    /// <summary></summary>
    public uint LowPart;

    /// <summary></summary>
    public uint HighPart;
}
