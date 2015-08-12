using Freemwork.Primitives.Math;
using Freemwork.World;

namespace Freemwork.View.Strategies
{
    public interface ICameraScalingStrategy
    {
        Vector2 GetScale(Worldspawn Worldspawn, Camera2D Owner);
    }
}
