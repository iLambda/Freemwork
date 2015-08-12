using Freemwork.Playstates;
using Freemwork.Primitives.Math;
using Freemwork.Primitives.Systems.Automaton;
using Freemwork.Services;
using Freemwork.Services.Audio;
using Freemwork.Services.Device;
using Freemwork.Services.File;
using Freemwork.Services.Graphics;
using Freemwork.Services.Input;
using Freemwork.Services.Resource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Freemwork
{
    public class XNAContext : Game
    {
        private GraphicsDeviceManager graphics;
        private Context context = new Context();

        public XNAContext()
        {
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            var spriteBatch = new SpriteBatch(GraphicsDevice);

            ServiceLocator.Provide<IGraphicsService>(new XNAGraphicsService(this, graphics, GraphicsDevice, spriteBatch, new Size2D<int>(800, 480))
            {
                KeepViewportRatio = true, 
                IsMouseVisible = true,
                ClearColor = Primitives.Graphic.Color.Black
            });
            ServiceLocator.Provide<IResourceService>(new XNAResourceService(Content));
            ServiceLocator.Provide<IInputService>(new XNAInputService());
            ServiceLocator.Provide<IDeviceService>(new RTDeviceService());
            ServiceLocator.Provide<IFileService>(new RTFileService());
            ServiceLocator.Provide<IAudioService>(new XNAAudioService()); 

            base.Initialize();
        }

        protected override void LoadContent()
        {
            context.PlayStateMachine = new FiniteSingleStateMachine<PlayState>(new GamePlayState());
            context.PlayStateMachine.Transitions.Add(new FiniteTransition("Menu", "Settings", "MenuToSettings"));
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime GameTime)
        {
            context.Update();
        }

        protected override void Draw(GameTime GameTime)
        {
            context.Draw();
        }
    }
}
