using System.Collections.Generic;
using System.Linq;

namespace Alba.XnaConvert.Common
{
    public static class ContentServiceMultiMetadataExts
    {
        public static IEnumerable<IContentServiceMetadata> GetMetadata (this IContentServiceMultiMetadata @this)
        {
            return @this.Name.Zip(@this.Version, (name, version) => new ContentServiceMetadata(name, version));
        }

        private class ContentServiceMetadata : IContentServiceMetadata
        {
            public string Name { get; private set; }
            public string Version { get; private set; }

            public ContentServiceMetadata (string name, string version)
            {
                Name = name;
                Version = version;
            }
        }
    }
}