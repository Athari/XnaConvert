using System;
using System.IO;
using System.Windows.Forms;
using Alba.XnaConvert.Common;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Alba.XnaConvert.Loader.Xna4
{
    [ExportContentService ("XNA", "4.0")]
    [ExportContentService ("XNA", "4")]
    public class ContentService : ContentManager, IContentService
    {
        public ContentService () : base(new GraphicsService())
        {}

        public IAsset LoadTexture2D (string filename)
        {
            return new Texture2DAsset(Load<Texture2D>(filename));
        }

        protected override Stream OpenStream (string assetName)
        {
            return new FileStream(assetName, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        private class GraphicsService : IServiceProvider, IGraphicsDeviceService
        {
            private readonly Form _form;

            public GraphicsDevice GraphicsDevice { get; private set; }

            public event EventHandler<EventArgs> DeviceDisposing;
            public event EventHandler<EventArgs> DeviceReset;
            public event EventHandler<EventArgs> DeviceResetting;
            public event EventHandler<EventArgs> DeviceCreated;

            public object GetService (Type serviceType)
            {
                if (serviceType.IsAssignableFrom(GetType()))
                    return this;
                throw new InvalidOperationException(string.Format("Service '{0}' not implemented.", serviceType));
            }

            public GraphicsService ()
            {
                _form = new Form();
                GraphicsDevice = new GraphicsDevice(
                    GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef,
                    new PresentationParameters {
                        IsFullScreen = false,
                        DeviceWindowHandle = _form.Handle,
                    });
            }
        }
    }
}