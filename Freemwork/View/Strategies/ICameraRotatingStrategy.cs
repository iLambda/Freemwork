using Freemwork.World;

namespace Freemwork.View.Strategies
{
    public interface ICameraRotatingStrategy
    {
        float GetRotation(Worldspawn Worldspawn, Camera2D Owner);
    }
}
