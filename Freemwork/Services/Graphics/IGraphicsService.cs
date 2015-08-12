using System;
using Freemwork.Primitives.Graphic;
using Freemwork.Primitives.Math;
using Freemwork.Primitives.Resources;

namespace Freemwork.Services.Graphics
{
    public interface IGraphicsService : IService
    {
        TimeSpan FrameDuration { get; }
        Color ClearColor { get; set; }
        bool KeepViewportRatio { get; set; }
        Color SideColor { get; set; }
        bool IsMouseVisible { get; set; }
        Size2D<int> ActualViewport { get; }
        Size2D<int> ScaledViewport { get; }
        Interpolation InterpolationMode { get; set; }
        Size2D<int> ViewportSize { get; set; }

        Matrix3x3 ViewportTransform { get; }
        Matrix3x3 InvertViewportTransform { get; }

        void BeginDraw();
        void EndDraw();
        void Draw(ISprite Sprite, Transform2D Transform);
        void Draw(IText Text, Transform2D Transform);
        void Clear();
    }
}
