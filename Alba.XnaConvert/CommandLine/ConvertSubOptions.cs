using CommandLine;

namespace Alba.XnaConvert.CommandLine
{
    internal class ConvertSubOptions
    {
        [Option ('l', "library", Required = true, DefaultValue = "XNA", HelpText = "Library name.")]
        public string LoaderName { get; set; }

        [Option ('v', "version", Required = true, DefaultValue = "4.0", HelpText = "Library version.")]
        public string LoaderVersion { get; set; }

        [Option ('i', "input", Required = true, HelpText = "Input file (*.xnb).")]
        public string InputFile { get; set; }

        [Option ('o', "output", Required = true, HelpText = "Output file (*.png for Texture2D).")]
        public string OutputFile { get; set; }
    }
}