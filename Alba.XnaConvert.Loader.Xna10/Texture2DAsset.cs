using Alba.XnaConvert.Common;
using Microsoft.Xna.Framework.Graphics;

namespace Alba.XnaConvert.Loader.Xna10
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
            _asset.Save(filename, ImageFileFormat.Png);
        }
    }
}