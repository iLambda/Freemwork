using System;
using Freemwork.Primitives.Graphic;
using Freemwork.Primitives.Math;
using Freemwork.Primitives.Resources;

namespace Freemwork.Services.Graphics
{
    public sealed class NullGraphicsService : IGraphicsService, INullService
    {
        public TimeSpan FrameDuration { get { return TimeSpan.Zero; } }
        public Color ClearColor { get { return new Color(128, 128, 128); } set { } }
        public Size2D<int> ScaledViewport { get; private set; }
        public Interpolation InterpolationMode { get; set; }
        public Size2D<int> ViewportSize { get; set; }
        public Matrix3x3 ViewportTransform { get { return Matrix3x3.Identity; } }
        public Matrix3x3 InvertViewportTransform { get { return Matrix3x3.Identity; } }
        public bool KeepViewportRatio { get; set; }
        public Color SideColor { get; set; }
        public bool IsMouseVisible { get; set; }
        public Size2D<int> ActualViewport { get; private set; }

        public NullGraphicsService()
        {
            ScaledViewport = default(Size2D<int>);
            ActualViewport = default(Size2D<int>);
        }

        public void BeginDraw()
        {
            #if DEBUG_VERBOSE
                if(Debugger.IsAttached)
                    Debug.WriteLine("BeginDraw()");
            #endif
        }

        public void EndDraw()
        {
            #if DEBUG_VERBOSE
                if (Debugger.IsAttached)
                    Debug.WriteLine("EndDraw()");
            #endif
        }

        public void Draw(ISprite Sprite, Transform2D Transform)
        {
            #if DEBUG_VERBOSE
                if (Debugger.IsAttached)
                    Debug.WriteLine("Draw()");
            #endif
        }

        public void Draw(IText Sprite, Transform2D Transform)
        {
#if DEBUG_VERBOSE
                if (Debugger.IsAttached)
                    Debug.WriteLine("Draw()");
#endif
        }


        public void Clear()
        {
            #if DEBUG_VERBOSE
                if (Debugger.IsAttached)
                    Debug.WriteLine("Clear()");
            #endif
        }

    }
}
