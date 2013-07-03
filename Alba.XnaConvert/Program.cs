using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Alba.XnaConvert.Common;

namespace Alba.XnaConvert
{
    internal class Program
    {
        private static void Main (string[] args)
        {
            try {
                new Program().RunInternal(args);
                Console.WriteLine("Done!");
            }
            catch (Exception e) {
                Console.WriteLine("Error!");
                Console.WriteLine(e);
            }
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private void RunInternal (string[] args)
        {
            string inputFile = args[0];
            string outputFile = args[1];
            string loaderName = args[2];
            string loaderVersion = args[3];

            var loaderAssembly = Assembly.LoadFile(Path.GetFullPath(string.Format("Alba.XnaConvert.Loader.{0}{1}.dll", loaderName, loaderVersion)));
            var loaderType = loaderAssembly.ExportedTypes.Single(t => typeof(IContentService).IsAssignableFrom(t));
            var loader = (IContentService)Activator.CreateInstance(loaderType);
            using (loader) {
                loader.LoadTexture2D(inputFile).SaveToFile(outputFile);
                loader.Unload();
            }
        }
    }
}