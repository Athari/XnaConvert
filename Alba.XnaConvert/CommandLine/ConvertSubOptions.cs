using CommandLine;

namespace Alba.XnaConvert.CommandLine
{
    internal class ConvertSubOptions
    {
        [Option ('l', "library", DefaultValue = "XNA", HelpText = "Library name.")]
        public string LoaderName { get; set; }

        [Option ('v', "version", DefaultValue = "4.0", HelpText = "Library version.")]
        public string LoaderVersion { get; set; }

        [Option ('i', "input", MutuallyExclusiveSet = "input", HelpText = "Input file (*.xnb).")]
        public string InputFile { get; set; }

        [Option ('d', "inputdir", MutuallyExclusiveSet = "input", HelpText = "Input directory (with *.xnb files).")]
        public string InputDir { get; set; }

        [Option ('m', "mask", DefaultValue = "*.xnb", HelpText = "Input mask (with *.xnb files).")]
        public string InputMask { get; set; }

        [Option ('r', "recursive", DefaultValue = false, HelpText = "Process files in input directory recursively.")]
        public bool IsRecursive { get; set; }

        [Option ('o', "output", Required = true, HelpText = "Output file or directory.")]
        public string OutputFileDir { get; set; }
    }
}