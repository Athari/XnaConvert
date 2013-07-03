using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alba.Framework.Collections;
using Alba.Framework.Linq;

namespace Alba.XnaConvert.Common
{
    public class ContentServiceMetadata
    {
        public List<IContentServiceMetadata> Items { get; private set; }

        public ContentServiceMetadata (IDictionary<string, object> data)
        {
            string[] names = GetValues(data, o => o.Name);
            string[] versions = GetValues(data, o => o.Version);
            bool[] isPublics = GetValues(data, o => o.IsPublic);
            Items = names.Length.Range()
                .Select(i => (IContentServiceMetadata)new ExportContentServiceAttribute(names[i], versions[i], isPublics[i]))
                .ToList();
        }

        private static T[] GetValues<T> (IDictionary<string, object> data, Expression<Func<IContentServiceMetadata, T>> propExpr)
        {
            return (T[])data[Props.GetName(propExpr)];
        }
    }
}