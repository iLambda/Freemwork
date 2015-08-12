using System;
using Freemwork.Primitives.Graphic;
using Freemwork.Primitives.Math;
using Freemwork.Utilities;
using Microsoft.Xna.Framework.Graphics;

namespace Freemwork.Primitives.Resources
{
    public class XNAText : IText
    {
        public SpriteFont Font { get; private set; }
        public String Name { get { return Font.Texture.Name; } }
        public string FontName { get { return Font.Texture.Name; } }
        public String Text { get; set; }
        public Size2D<int> FullSize { get { return Measure(); } }
        public bool Loaded { get; private set; }
        public bool Visible { get; set; }
        public Color Fill { get; set; }
        public Vector2 Origin { get; set; }
        public MirrorEffect MirrorEffect { get; set; }
        public float Depth { get; set; }

        public XNAText(SpriteFont Font)
        {
            this.Font = Font;
            this.Visible = true;
            this.Fill = new Color(255, 255, 255, 255);
            this.MirrorEffect = MirrorEffect.None;
            this.Depth = 0f;
        }

        public Size2D<int> Measure()
        {
            var size = Font.MeasureString(Text);
            return new Size2D<int>((int)Maths.Ceil(size.X), (int)size.Y);
        }

        public Size2D<int> Measure(String String)
        {
            var size = Font.MeasureString(Text);
            return new Size2D<int>((int)Maths.Ceil(size.X), (int)Maths.Ceil(size.Y));
        }

        public IResource Clone()
        {
            return new XNAText(Font)
            {
                Loaded = Loaded, 
                Text =  Text, 
                Fill = Fill, 
                MirrorEffect = MirrorEffect, 
                Origin = Origin, 
                Visible = Visible,
                Depth = Depth
            };
        }
    }
}
