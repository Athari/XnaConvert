using CommandLine;
using CommandLine.Text;

namespace Alba.XnaConvert.CommandLine
{
    internal class Options
    {
        [VerbOption ("convert", HelpText = "Convert file from one format to another.")]
        public ConvertSubOptions ConvertVerb { get; set; }

        [VerbOption ("listlibs", HelpText = "List supported libraries and versions.")]
        public ListLibsSubOptions ListLibsVerb { get; set; }

        [HelpVerbOption (HelpText = "Get help on available command line options.")]
        public string GetUsage (string verb)
        {
            HelpText text = HelpText.AutoBuild(this, verb);
            text.Heading = "Utility for converting XNA XNB files";
            text.Copyright = "https://github.com/Athari/XnaConvert";
            return text;
        }

        public static Options ParseCommandLine (string[] args)
        {
            var options = new Options();
            bool result = Parser.Default.ParseArguments(args, options, (_1, _2) => { });
            return result ? options : null;
        }
    }
}