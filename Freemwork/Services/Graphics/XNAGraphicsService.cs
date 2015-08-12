using System;
using Freemwork.Primitives.Graphic;
using Freemwork.Primitives.Math;
using Freemwork.Primitives.Resources;
using Freemwork.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace Freemwork.Services.Graphics
{
    public sealed class XNAGraphicsService : IGraphicsService
    {
        private SpriteBatch spriteBatch;
        private GraphicsDeviceManager graphicsDeviceManager;
        private GraphicsDevice graphicsDevice;
        private XNAContext context;
        private Texture2D whiteTexture;
        private Matrix transform = Matrix.Identity;
        private float scale = 1f;
        private float invScale = 1f;
        private Interpolation interpolation = Interpolation.PointClamp;
        private SamplerState samplerState = SamplerState.PointClamp;

        public Size2D<int> ActualViewport { get { return KeepViewportRatio ? ScaledViewport : new Size2D<int>(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height); } } 
        public Size2D<int> ScaledViewport { get; private set; }

        public Interpolation InterpolationMode
        {
            get { return interpolation; }
            set
            {
                if(interpolation == value) return;
                interpolation = value;

                switch (InterpolationMode)
                {
                    case Interpolation.PointClamp:
                        samplerState = SamplerState.PointClamp;
                        break;
                    case Interpolation.PointWrap:
                        samplerState = SamplerState.PointWrap;
                        break;
                    case Interpolation.LinearClamp:
                        samplerState = SamplerState.LinearClamp;
                        break;
                    case Interpolation.LinearWrap:
                        samplerState = SamplerState.LinearWrap;
                        break;
                    case Interpolation.AnisotropicClamp:
                        samplerState = SamplerState.AnisotropicClamp;
                        break;
                    case Interpolation.AnisotropicWrap:
                        samplerState = SamplerState.AnisotropicWrap;
                        break;
                    default:
                        throw new NotImplementedException("Unimplemented interpolation mode for this service.");
                }
            }
        }

        public Size2D<int> ViewportSize { get; set; }
        public TimeSpan FrameDuration { get; private set; }
        public Primitives.Graphic.Color ClearColor { get; set; }
        public Matrix3x3 ViewportTransform { get; private set; }
        public Matrix3x3 InvertViewportTransform { get; private set; }
        public bool KeepViewportRatio { get; set; }
        public Primitives.Graphic.Color SideColor { get; set; }
        public bool IsMouseVisible 
        {
            get { return context.IsMouseVisible; }
            set { context.IsMouseVisible = value; } 
        }


        public XNAGraphicsService(XNAContext Context, GraphicsDeviceManager GraphicsDeviceManager, GraphicsDevice GraphicsDevice, SpriteBatch SpriteBatch, Size2D<int> ViewportSize, bool KeepViewportRatio = false)
        {
            this.KeepViewportRatio = KeepViewportRatio;
            SideColor = new Primitives.Graphic.Color(0, 0, 0, 255);

            graphicsDeviceManager = GraphicsDeviceManager;
            graphicsDevice = GraphicsDevice;
            spriteBatch = SpriteBatch;
            context = Context;
            whiteTexture = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            whiteTexture.SetData(new[] { Color.White });
            this.ViewportSize = ViewportSize;

            ClearColor = new Primitives.Graphic.Color(0, 0, 0, 255);
            FrameDuration = TimeSpan.FromMilliseconds(1000f / 33f);

            context.IsFixedTimeStep = true;
            context.TargetElapsedTime = FrameDuration;

            ComputeSize();
        }

        private void ComputeSize()
        {
            var sX = (float)graphicsDevice.Viewport.Width / ViewportSize.Width;
            var sY = (float)graphicsDevice.Viewport.Height / ViewportSize.Height;
            scale = Math.Min(sX, sY);
            invScale = 1f / scale;

            transform = Matrix.CreateScale(scale, scale, 1);
            ViewportTransform = Matrix3x3.CreateScale(scale);
            InvertViewportTransform = Matrix3x3.CreateScale(invScale);
            ScaledViewport = KeepViewportRatio
                ? ViewportSize
                : new Size2D<int>((int)(invScale * graphicsDevice.Viewport.Width),
                    (int)(invScale * graphicsDevice.Viewport.Height));
        }

        public void BeginDraw()
        {
            graphicsDevice.SetRenderTarget(null);

            ComputeSize();

            if (KeepViewportRatio)
            {
                var actualSize = new Size2D<int>((int)(ViewportSize.Width * scale), (int)(ViewportSize.Height * scale));
                var matrix = Matrix3x3.CreateTranslation(invScale * Math.Abs(actualSize.Width - graphicsDevice.Viewport.Width) / 2f, invScale * Math.Abs(actualSize.Height - graphicsDevice.Viewport.Height) / 2f);
                var inverseMat = Matrix3x3.CreateTranslation(-invScale * Math.Abs(actualSize.Width - graphicsDevice.Viewport.Width) / 2f, -invScale * Math.Abs(actualSize.Height - graphicsDevice.Viewport.Height) / 2f);

                ViewportTransform = matrix * ViewportTransform;
                InvertViewportTransform = InvertViewportTransform * inverseMat;
                transform = Matrix.CreateTranslation(invScale * Math.Abs(actualSize.Width - graphicsDevice.Viewport.Width) / 2f, invScale * Math.Abs(actualSize.Height - graphicsDevice.Viewport.Height) / 2f, 0)
                    * transform;
            }
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, samplerState, DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, transform);
        }

        public void EndDraw()
        {
            if (KeepViewportRatio)
            {
                var sideColor = new Color(SideColor.R, SideColor.G, SideColor.B, SideColor.A);
                var actualSize = new Size2D<int>((int)(ViewportSize.Width * scale), (int)(ViewportSize.Height * scale));

                if ((int)(invScale * graphicsDevice.Viewport.Width) > ViewportSize.Width)
                {
                    var bandWidth = (int)(invScale * Math.Abs(actualSize.Width - graphicsDevice.Viewport.Width) / 2f);

                    spriteBatch.Draw(whiteTexture, new Rectangle(-bandWidth * 2, 0, bandWidth * 2, ViewportSize.Height), null, sideColor, 0f, Microsoft.Xna.Framework.Vector2.Zero, SpriteEffects.None, 0.95f);
                    spriteBatch.Draw(whiteTexture, new Rectangle(ViewportSize.Width, 0, 2 * bandWidth, ViewportSize.Height), null, sideColor, 0f, Microsoft.Xna.Framework.Vector2.Zero, SpriteEffects.None, 0.95f);
                }
                if ((int)(invScale * graphicsDevice.Viewport.Height) > ViewportSize.Height)
                {
                    var bandHeight = (int)(invScale * Math.Abs(actualSize.Height - graphicsDevice.Viewport.Height) / 2f);

                    spriteBatch.Draw(whiteTexture, new Rectangle(0, -bandHeight * 2, ViewportSize.Width, bandHeight * 2), null, sideColor, 0f, Microsoft.Xna.Framework.Vector2.Zero, SpriteEffects.None, 0.95f);
                    spriteBatch.Draw(whiteTexture, new Rectangle(0, ViewportSize.Height, ViewportSize.Width, 2 * bandHeight), null, sideColor, 0f, Microsoft.Xna.Framework.Vector2.Zero, SpriteEffects.None, 0.95f);
                }
            }
            spriteBatch.End();
        }

        public void Clear()
        {
            graphicsDevice.Clear(new Color(ClearColor.R, ClearColor.G, ClearColor.B, ClearColor.A));
        }
        public void Draw(ISprite Sprite, Transform2D Transform)
        {
            var sprite = (XNASprite)Sprite;

            if (sprite.Visible)
                spriteBatch.Draw(sprite.Texture,
                    new Rectangle((int)Transform.Position.X, (int)Transform.Position.Y, (int)(sprite.Size.Width * Transform.Scale.X), (int)(sprite.Size.Height * Transform.Scale.Y)),
                    new Rectangle(sprite.Region.X, sprite.Region.Y, sprite.Region.Width, sprite.Region.Height),
                    new Color(sprite.Fill.R, sprite.Fill.G, sprite.Fill.B, sprite.Fill.A),
                    Transform.Rotation * (Maths.Pi / 180f),
                    new Microsoft.Xna.Framework.Vector2(sprite.Origin.X, sprite.Origin.Y),
                    ((sprite.MirrorEffect & MirrorEffect.VerticalMirror) != 0 ? SpriteEffects.FlipVertically : 0) | ((sprite.MirrorEffect & MirrorEffect.HorizontalMirror) != 0 ? SpriteEffects.FlipHorizontally : 0),
                    Maths.Clamp(sprite.Depth, 0f, 1f) * 0.9f);
        }

        public void Draw(IText Text, Transform2D Transform)
        {
            var text = (XNAText)Text;

            if (text.Visible)
                spriteBatch.DrawString(text.Font,
                    text.Text,
                    new Microsoft.Xna.Framework.Vector2(Transform.Position.X, Transform.Position.Y),
                    new Color(text.Fill.R, text.Fill.G, text.Fill.B, text.Fill.A),
                    Transform.Rotation * (Maths.Pi / 180f),
                    new Microsoft.Xna.Framework.Vector2(text.Origin.X, text.Origin.Y),
                    new Microsoft.Xna.Framework.Vector2(Transform.Scale.X, Transform.Scale.Y),
                    ((text.MirrorEffect & MirrorEffect.VerticalMirror) != 0 ? SpriteEffects.FlipVertically : 0) | ((text.MirrorEffect & MirrorEffect.HorizontalMirror) != 0 ? SpriteEffects.FlipHorizontally : 0),
                    Maths.Clamp(text.Depth, 0f, 1f) * 0.9f);
        }
    }
}
