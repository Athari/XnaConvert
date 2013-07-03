using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
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
                new Program().RunInternal(args);
                Console.WriteLine("Done!");
            }
            catch (ApplicationException e) {
                Console.WriteLine("Error!");
                Console.WriteLine(e.Message);
            }
            catch (Exception e) {
                Console.WriteLine("Unexpected error!");
                Console.WriteLine(e);
            }
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private void RunInternal (string[] args)
        {
            ComposeContentServices();

            string inputFile = args[0];
            string outputFile = args[1];
            string loaderName = args[2];
            string loaderVersion = args[3];

            var loader = GetContentService(loaderName, loaderVersion);
            using (loader) {
                loader.LoadTexture2D(inputFile).SaveToFile(outputFile);
                loader.Unload();
            }
        }

        private void ComposeContentServices ()
        {
            new CompositionContainer(new DirectoryCatalog(".", "Alba.XnaConvert.Loader.*.dll")).ComposeParts(this);
        }

        private IContentService GetContentService (string name, string version)
        {
            var service = ContentServices.FirstOrDefault(cs =>
                cs.Metadata.GetMetadata().Any(meta =>
                    string.Equals(meta.Name, name, StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(meta.Version, version, StringComparison.OrdinalIgnoreCase)
                    ));
            if (service == null)
                throw new ApplicationException(string.Format("Loader for '{0}' with version '{1}' not found.", name, version));
            return service.Value;
        }
    }
}