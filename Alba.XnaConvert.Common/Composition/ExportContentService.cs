using System;
using System.ComponentModel.Composition;

namespace Alba.XnaConvert.Common
{
    [MetadataAttribute, AttributeUsage (AttributeTargets.Class, AllowMultiple = true)]
    public class ExportContentService : ExportAttribute, IContentServiceMetadata
    {
        public string Name { get; set; }
        public string Version { get; set; }

        public ExportContentService (string name, string version) : base(typeof(IContentService))
        {
            Version = version;
            Name = name;
        }
    }
}