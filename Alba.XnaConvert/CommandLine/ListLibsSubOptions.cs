using CommandLine;

namespace Alba.XnaConvert.CommandLine
{
    internal class ListLibsSubOptions
    {
        [Option ('a', "all", DefaultValue = false, HelpText = "Include all aliases.")]
        public bool IsAll { get; set; }
    }
}