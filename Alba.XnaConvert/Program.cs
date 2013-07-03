using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Alba.Framework.Sys;
using Alba.Framework.Text;
using Alba.XnaConvert.CommandLine;
using Alba.XnaConvert.Common;

namespace Alba.XnaConvert
{
    internal class Program
    {
        [ImportMany]
        public IEnumerable<Lazy<IContentService, ContentServiceMetadata>> ContentServices { get; set; }

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
                if (options.InputFile != null) {
                    ConvertFile(loader, options.InputFile, options.OutputFileDir);
                }
                else if (options.InputDir != null) {
                    if (!options.InputDir.EndsWith("\\"))
                        options.InputDir += "\\";
                    string[] inputFiles = Directory.GetFiles(options.InputDir, options.InputMask,
                        options.IsRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                    Console.WriteLine("Found {0} files matching mask '{1}' in directory '{2}'.",
                        inputFiles.Length, options.InputMask, options.InputDir);
                    foreach (string inputFile in inputFiles) {
                        string outputFile = Path.Combine(
                            options.OutputFileDir,
                            Path.ChangeExtension(inputFile.RemovePrefix(options.InputDir), "png"));
                        ConvertFile(loader, inputFile, outputFile);
                    }
                }
                else {
                    throw new UserException("Input file or directory not specified.");
                }
            }
        }

        private static void ConvertFile (IContentService loader, string inputFile, string outputFile)
        {
            try {
                if (File.Exists(outputFile) && new FileInfo(outputFile).Length > 0) {
                    Console.WriteLine("Skipping file '{0}' (output file already exists)...", inputFile);
                    return;
                }
                Console.WriteLine("Loading file '{0}'...", inputFile);
                IAsset texture = loader.LoadTexture2D(inputFile);
                Console.WriteLine("Saving file '{0}'...", outputFile);
                Directory.CreateDirectory(Path.GetDirectoryName(outputFile));
                texture.SaveToFile(outputFile);
                Console.WriteLine("Saved successfully.");
                loader.Unload();
            }
            catch (OutOfMemoryException) {
                Console.WriteLine("Not enough memory to continue operation.");
                Exit(1);
            }
            catch (Exception e) {
                Console.WriteLine("Failed to save file. {0}", e.GetFullMessage());
            }
        }

        private void RunListLibsVerb (ListLibsSubOptions options)
        {
            foreach (var metadata in ContentServices.SelectMany(cs => cs.Metadata.Items)
                .Where(m => options.IsAll || m.IsPublic)
                .OrderBy(m => m.Name).ThenBy(m => m.Version))
                Console.WriteLine("* Library: {0} Version: {1}", metadata.Name, metadata.Version);
        }

        private void ComposeContentServices ()
        {
            new CompositionContainer(new DirectoryCatalog(".", "Alba.XnaConvert.Loader.*.dll")).ComposeParts(this);
        }

        private IContentService GetContentService (string name, string version)
        {
            var lazyService = ContentServices.FirstOrDefault(cs =>
                cs.Metadata.Items.Any(meta => meta.Name.EqualsCaseOrd(name) && meta.Version.EqualsCaseOrd(version)));
            if (lazyService == null)
                throw new UserException("Content loader for '{0}' with version '{1}' not found.".Fmt(name, version));
            Console.WriteLine("Loading content loader for '{0}' with version '{1}'...".Fmt(name, version));
            IContentService service = lazyService.Value;
            Console.WriteLine("Loaded content loader successfully.");
            return service;
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