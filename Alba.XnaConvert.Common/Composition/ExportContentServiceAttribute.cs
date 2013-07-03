using System;
using System.ComponentModel.Composition;

namespace Alba.XnaConvert.Common
{
    [MetadataAttribute, AttributeUsage (AttributeTargets.Class, AllowMultiple = true)]
    public class ExportContentServiceAttribute : Attribute, IContentServiceMetadata
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public bool IsPublic { get; set; }

        public ExportContentServiceAttribute (string name, string version, bool isPublic = true)
        {
            Version = version;
            Name = name;
            IsPublic = isPublic;
        }
    }
}