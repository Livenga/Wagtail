namespace Wagtail.Win32;

/// <summary></summary>
public enum PrivilegeAttribute : uint
{
    /// <summary></summary>
    SE_PRIVILEGE_ENABLED_BY_DEFAULT = 0x00000001,

    /// <summary></summary>
    SE_PRIVILEGE_ENABLED = 0x00000002,

    /// <summary></summary>
    SE_PRIVILEGE_REMOVED = 0x00000004,

    /// <summary></summary>
    SE_PRIVILEGE_USED_FOR_ACCESS = 0x80000000,
}
