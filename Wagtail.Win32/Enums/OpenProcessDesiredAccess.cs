namespace Wagtail.Win32.Enums;

/// <summary></summary>
public enum OpenProcessDesiredAccess : uint
{
    /// <summary></summary>
    DELETE = 0x00010000,

    /// <summary></summary>
    READ_CONTROL = 0x00020000,

    /// <summary></summary>
    SYNCHRONIZE = 0x00100000,

    /// <summary></summary>
    WRITE_DAC = 0x00040000,

    /// <summary></summary>
    WRITE_OWNER = 0x00080000,

    /// <summary></summary>
    PROCESS_CREATE_PROCESS = 0x0080,

    /// <summary></summary>
    PROCESS_CREATE_THREAD = 0x0002,

    /// <summary></summary>
    PROCESS_DUP_HANDLE = 0x0040,

    /// <summary></summary>
    PROCESS_QUERY_INFORMATION = 0x0400,

    /// <summary></summary>
    PROCESS_QUERY_LIMITED_INFORMATION = 0x1000,

    /// <summary></summary>
    PROCESS_SET_INFORMATION = 0x0200,

    /// <summary></summary>
    PROCESS_SET_QUOTA = 0x0100,

    /// <summary></summary>
    PROCESS_SUSPEND_RESUME = 0x0800,

    /// <summary></summary>
    PROCESS_TERMINATE = 0x0001,

    /// <summary></summary>
    PROCESS_VM_OPERATION = 0x0008,

    /// <summary></summary>
    PROCESS_VM_READ = 0x0010,

    /// <summary></summary>
    PROCESS_VM_WRITE = 0x0020,

}
