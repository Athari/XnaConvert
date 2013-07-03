using CommandLine;

namespace Alba.XnaConvert.CommandLine
{
    internal class ListLibsSubOptions
    {
        [Option ('a', "all", DefaultValue = false, HelpText = "List all libraries, including aliases.")]
        public bool IsAll { get; set; }
    }
}