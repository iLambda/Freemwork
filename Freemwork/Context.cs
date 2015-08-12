using Freemwork.Playstates;
using Freemwork.Primitives.Systems.Automaton;
using Freemwork.Services;

namespace Freemwork
{
    public sealed class Context
    {
        public FiniteSingleStateMachine<PlayState> PlayStateMachine { get; set; }

        public void Update()
        {
            ServiceLocator.InputService.BeginFetch();
            PlayStateMachine.CurrentState.Update();
            ServiceLocator.InputService.EndFetch();
        }

        public void Draw()
        {
            ServiceLocator.GraphicsService.BeginDraw();
            ServiceLocator.GraphicsService.Clear();
            PlayStateMachine.CurrentState.Draw();
            ServiceLocator.GraphicsService.EndDraw();
        }
    }
}
