using System.Collections.Generic;

// ReSharper disable LoopCanBeConvertedToQuery
namespace Alba.XnaConvert.Common
{
    public static class ContentServiceMultiMetadataExts
    {
        public static IEnumerable<IContentServiceMetadata> GetMetadata (this IContentServiceMultiMetadata @this)
        {
            for (int i = 0; i < @this.Name.Length; i++)
                yield return new ContentServiceMetadata(@this.Name[i], @this.Version[i], @this.IsPublic[i]);
        }

        private class ContentServiceMetadata : IContentServiceMetadata
        {
            public string Name { get; private set; }
            public string Version { get; private set; }
            public bool IsPublic { get; private set; }

            public ContentServiceMetadata (string name, string version, bool isPublic)
            {
                Name = name;
                Version = version;
                IsPublic = isPublic;
            }
        }
    }
}