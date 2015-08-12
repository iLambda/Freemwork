using Freemwork.Playstates;

namespace Freemwork.World.Objects.Components
{
    public interface IGameComponent
    {
        void Update(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID);
        void Draw(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID);
        IGameComponent Clone();
    }

}
