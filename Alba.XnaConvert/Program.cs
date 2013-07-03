using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using Alba.Framework.Text;
using Alba.XnaConvert.CommandLine;
using Alba.XnaConvert.Common;

namespace Alba.XnaConvert
{
    internal class Program
    {
        [ImportMany]
        public IEnumerable<Lazy<IContentService, IContentServiceMultiMetadata>> ContentServices { get; set; }

        private static void Main (string[] args)
        {
            try {
                new Program().MainInternal(args);
                Console.WriteLine("Done!");
                Exit(0);
            }
            catch (UserException e) {
                Console.WriteLine("Error!");
                Console.WriteLine(e.Message);
                Exit(1);
            }
            catch (Exception e) {
                Console.WriteLine("Unexpected error!");
                Console.WriteLine(e);
                Exit(1);
            }
        }

        private void MainInternal (string[] args)
        {
            Options options = Options.ParseCommandLine(args);
            if (options == null)
                Environment.Exit(1);

            ComposeContentServices();
            if (options.ConvertVerb != null)
                RunConvertVerb(options.ConvertVerb);
            else if (options.ListLibsVerb != null)
                RunListLibsVerb(options.ListLibsVerb);
        }

        private void RunConvertVerb (ConvertSubOptions options)
        {
            var loader = GetContentService(options.LoaderName, options.LoaderVersion);
            using (loader) {
                loader.LoadTexture2D(options.InputFile).SaveToFile(options.OutputFile);
                loader.Unload();
            }
        }

        private void RunListLibsVerb (ListLibsSubOptions options)
        {
            foreach (var metadata in ContentServices
                .SelectMany(cs => cs.Metadata.GetMetadata())
                .Where(m => options.IsAll || m.IsPublic)
                .Select(m => new { m.Name, m.Version })
                .Distinct())
                Console.WriteLine("* Library: {0} Version: {1}", metadata.Name, metadata.Version);
        }

        private void ComposeContentServices ()
        {
            new CompositionContainer(new DirectoryCatalog(".", "Alba.XnaConvert.Loader.*.dll")).ComposeParts(this);
        }

        private IContentService GetContentService (string name, string version)
        {
            var service = ContentServices.FirstOrDefault(cs =>
                cs.Metadata.GetMetadata().Any(meta => meta.Name.EqualsCaseOrd(name) && meta.Version.EqualsCaseOrd(version)));
            if (service == null)
                throw new UserException("Loader for '{0}' with version '{1}' not found.".Fmt(name, version));
            return service.Value;
        }

        private static void Exit (int exitCode)
        {
            if (Debugger.IsAttached) {
                Console.WriteLine("Press any key...");
                Console.ReadKey();
            }
            Environment.Exit(exitCode);
        }
    }
}