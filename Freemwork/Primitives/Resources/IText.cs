using System;
using Freemwork.Primitives.Graphic;
using Freemwork.Primitives.Math;

namespace Freemwork.Primitives.Resources
{
    public interface IText : IResource
    {
        String Text { get; set; }
        Size2D<int> FullSize { get; }
        String FontName { get; }
        bool Visible { get; set; }
        Color Fill { get; set; }
        Vector2 Origin { get; set; }
        MirrorEffect MirrorEffect { get; set; }
        float Depth { get; set; }

        Size2D<int> Measure();
        Size2D<int> Measure(String String);
    }
}
