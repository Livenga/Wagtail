namespace Wagtail.Win32;

/// <summary></summary>
public enum ShutDownReason : uint
{
    /// <summary></summary>
    MAJOR_APPLICATION = 0x00040000,

    /// <summary></summary>
    MAJOR_HARDWARE = 0x00010000,

    /// <summary></summary>
    MAJOR_LEGACY_API = 0x00070000,

    /// <summary></summary>
    MAJOR_OPERATINGSYSTEM = 0x00020000,

    /// <summary></summary>
    MAJOR_OTHER = 0x00000000,

    /// <summary></summary>
    MAJOR_POWER = 0x00060000,

    /// <summary></summary>
    MAJOR_SOFTWARE = 0x00030000,

    /// <summary></summary>
    MAJOR_SYSTEM = 0x00050000,

    /// <summary></summary>
    MINOR_BLUESCREEN = 0x0000000F,

    /// <summary></summary>
    MINOR_CORDUNPLUGGED = 0x0000000b,

    /// <summary></summary>
    MINOR_DISK = 0x00000007,

    /// <summary></summary>
    MINOR_ENVIRONMENT = 0x0000000c,

    /// <summary></summary>
    MINOR_HARDWARE_DRIVER = 0x0000000d,

    /// <summary></summary>
    MINOR_HOTFIX = 0x00000011,

    /// <summary></summary>
    MINOR_HOTFIX_UNINSTALL = 0x00000017,

    /// <summary></summary>
    MINOR_HUNG = 0x00000005,

    /// <summary></summary>
    MINOR_INSTALLATION = 0x00000002,

    /// <summary></summary>
    MINOR_MAINTENANCE = 0x00000001,

    /// <summary></summary>
    MINOR_MMC = 0x00000019,

    /// <summary></summary>
    MINOR_NETWORK_CONNECTIVITY = 0x00000014,

    /// <summary></summary>
    MINOR_NETWORKCARD = 0x00000009,

    /// <summary></summary>
    MINOR_OTHER = 0x00000000,

    /// <summary></summary>
    MINOR_OTHERDRIVER = 0x0000000e,

    /// <summary></summary>
    MINOR_POWER_SUPPLY = 0x0000000a,

    /// <summary></summary>
    MINOR_PROCESSOR = 0x00000008,

    /// <summary></summary>
    MINOR_RECONFIG = 0x00000004,

    /// <summary></summary>
    MINOR_SECURITY = 0x00000013,

    /// <summary></summary>
    MINOR_SECURITYFIX = 0x00000012,

    /// <summary></summary>
    MINOR_SECURITYFIX_UNINSTALL = 0x00000018,

    /// <summary></summary>
    MINOR_SERVICEPACK = 0x00000010,

    /// <summary></summary>
    MINOR_SERVICEPACK_UNINSTALL = 0x00000016,

    /// <summary></summary>
    MINOR_TERMSRV = 0x00000020,

    /// <summary></summary>
    MINOR_UNSTABLE = 0x00000006,

    /// <summary></summary>
    MINOR_UPGRADE = 0x00000003,

    /// <summary></summary>
    MINOR_WMI = 0x00000015,

    /// <summary></summary>
    FLAG_USER_DEFINED = 0x40000000,

    /// <summary></summary>
    FLAG_PLANNED = 0x80000000,
}
