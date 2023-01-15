namespace Wagtail;

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;


/// <summary></summary>
public sealed class Ptr<T> : IDisposable where T : struct
{
    /// <summary></summary>
    public IntPtr Base { set; get; }


    /// <summary></summary>
    public Ptr()
    {
        Base = Marshal.AllocHGlobal(cb: Marshal.SizeOf<T>());
        if(Base == IntPtr.Zero)
        {
            // TODO
            throw new Exception();
        }
    }

    /// <summary></summary>
    public void Dispose()
    {
#if DEBUG
        Debug.WriteLine($"DEBUG | {nameof(Dispose)} {Base.ToInt():X}({typeof(T).FullName})");
#endif

        Marshal.FreeHGlobal(Base);
    }
}
