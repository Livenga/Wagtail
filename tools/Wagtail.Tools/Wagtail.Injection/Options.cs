namespace Wagtail.Injection;

using CommandLine;


/// <summary></summary>
public sealed class Options
{
    /// <summary></summary>
    [Option('k', "key", Required = false, Default = "303127")]
    public string Key { set; get; } = "303127";

    /// <summary></summary>
    [Option('v', "vcr-id", Required = false, Default = "Wagtail.ProcessWrapperForm")]
    public string VcrId { set; get; } = "Wagtail.ProcessWrapperForm";
}
