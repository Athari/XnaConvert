using System;

namespace Alba.XnaConvert.Common
{
    public interface IContentService : IDisposable
    {
        IAsset LoadTexture2D (string filename);
        void Unload ();
    }
}