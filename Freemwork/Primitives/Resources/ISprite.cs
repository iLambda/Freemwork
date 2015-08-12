using Freemwork.Primitives.Graphic;
using Freemwork.Primitives.Math;

namespace Freemwork.Primitives.Resources
{
    public interface ISprite : IResource
    {
        Size2D<int> FullSize { get; }
        Size2D<int> Size { get; set; }
        Rectangle<int> Region { get; set; }
        Vector2 Origin { get; set; }
        MirrorEffect MirrorEffect { get; set; }
        Color Fill { get; set; }
        bool Visible { get; set; }
        float Depth { get; set; }
    }
}