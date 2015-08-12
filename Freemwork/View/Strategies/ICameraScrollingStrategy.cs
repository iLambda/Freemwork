using Freemwork.Primitives.Math;
using Freemwork.World;

namespace Freemwork.View.Strategies
{
    public interface ICameraScrollingStrategy
    {
        Vector2 GetPosition(Worldspawn Worldspawn, Camera2D Owner);
    }
}
