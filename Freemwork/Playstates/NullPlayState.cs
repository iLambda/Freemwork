using Freemwork.World;

namespace Freemwork.Playstates
{
    public sealed class NullPlayState : PlayState
    {
        private Worldspawn worldspawn = new Worldspawn();

        public override Worldspawn Worldspawn { get { return worldspawn; } }
        public override string Identifier { get { return "Null"; } }

        public override void Update()
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("Update() called in NullPlayState");
            #endif

            base.Update();
        }

        public override void Draw()
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("Draw() called in NullPlayState");
            #endif

            base.Draw();
        }
    }
}
