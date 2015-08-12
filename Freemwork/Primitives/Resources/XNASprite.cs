using System;
using Freemwork.Primitives.Graphic;
using Freemwork.Primitives.Math;
using Microsoft.Xna.Framework.Graphics;

namespace Freemwork.Primitives.Resources
{
    public sealed class XNASprite : ISprite
    {
        private Texture2D texture = null;

        public Size2D<int> FullSize
        {
            get
            {
                if(texture == null)
                    throw new NullReferenceException("The XNASprite is not loaded.");
                return new Size2D<int>(texture.Width, texture.Height);
            }
        }

        public Rectangle<int> Region { get; set; }
        public Vector2 Origin { get; set; }
        public Size2D<int> Size { get; set; }
        public Color Fill { get; set; }
        public MirrorEffect MirrorEffect { get; set; }
        public bool Visible { get; set; }
        public float Depth { get; set; }

        public string Name
        {
            get
            {
                if (texture == null)
                    throw new NullReferenceException("The XNASprite is not loaded.");
                return texture.Name;
            }
        }

        public Texture2D Texture { get { return texture; } }
        public bool Loaded { get { return texture != null; } }

        public XNASprite(Texture2D Texture2D)
        {
            this.texture = Texture2D;
            this.Region = new Rectangle<int>(0, 0, texture.Width, texture.Height);
            this.Origin = new Vector2(0, 0);
            this.Size = FullSize;
            this.Fill = new Color(255, 255, 255);
            this.MirrorEffect = MirrorEffect.None;
            this.Visible = true;
            this.Depth = 0.5f;
        }

        public IResource Clone()
        {
            return new XNASprite(texture)
            {
                Region = Region,
                Origin = Origin,
                Size = Size,
                Fill = Fill,
                MirrorEffect = MirrorEffect,
                Visible = Visible,
                Depth = Depth
            };
        }
    }
}
