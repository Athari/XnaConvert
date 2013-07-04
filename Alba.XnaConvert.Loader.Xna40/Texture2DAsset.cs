using Alba.Framework.IO;
using Alba.XnaConvert.Common;
using Microsoft.Xna.Framework.Graphics;

namespace Alba.XnaConvert.Loader.Xna40
{
    public class Texture2DAsset : IAsset
    {
        private readonly Texture2D _asset;

        public Texture2DAsset (Texture2D asset)
        {
            _asset = asset;
        }

        public void SaveToFile (string filename)
        {
            using (var file = Streams.CreateFile(filename))
                _asset.SaveAsPng(file, _asset.Width, _asset.Height);
        }
    }
}