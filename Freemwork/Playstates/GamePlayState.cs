using System;
using System.Collections.Generic;
using Freemwork.Primitives.Graphic;
using Freemwork.Primitives.Input;
using Freemwork.Primitives.Input.Commands;
using Freemwork.Primitives.Input.Devices;
using Freemwork.Primitives.Math;
using Freemwork.Services;
using Freemwork.Services.File;
using Freemwork.Utilities;
using Freemwork.View.Strategies;
using Freemwork.World;
using Freemwork.World.Objects;
using Freemwork.World.Objects.Components.Debugging;
using Freemwork.World.Objects.Components.Graphics2D;
using Freemwork.World.Objects.Components.Input;

namespace Freemwork.Playstates
{
    public sealed class GamePlayState : PlayState
    {
        private Worldspawn worldspawn = new Worldspawn();

        public override Worldspawn Worldspawn { get { return worldspawn; } }
        public override string Identifier { get { return "Game"; } }

        public GamePlayState()
        {
            
        }
    }
}
