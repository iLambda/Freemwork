using System;
using Freemwork.Playstates;
using Freemwork.Primitives.Math;
using Freemwork.Primitives.Resources;
using Freemwork.Services;
using Freemwork.Utilities;
using Freemwork.Utilities.Attributes;

namespace Freemwork.World.Objects.Components.Graphics2D
{
    [UncompatibleComponent(typeof (SpriteHolder))]
    [UncompatibleComponent(typeof (SpriteAnimator))]
    [NeededComponent(typeof (Identity2D))]
    [BoundsDefiningProperty("Bounds")]
    public sealed class CompositeSpriteHolder : IGameComponent
    {
        public Vector2 Center { get; set; }
        public ISprite Tileset { get; set; }
        public Tuple<int, int>[,,] Tiles { get; set; }
        public int TileSize { get; set; }

        public float MinDepth { get; set; }
        public float MaxDepth { get; set; }

        public Rectangle<float> Bounds
        {
            get
            {
                return new Rectangle<float>(-Center.X, -Center.Y, Tiles.GetLength(0)*TileSize,
                    Tiles.GetLength(1)*TileSize);
            }
        }

        public CompositeSpriteHolder(ISprite Tileset, int[,,] Tiles, int TileSize)
        {
            if (!(Tileset.FullSize.Width/(float) TileSize).IsInteger() ||
                !(Tileset.FullSize.Height/(float) TileSize).IsInteger())
                throw new Exception("The TileSize doesn't fit the image.");

            this.Tileset = Tileset;
            this.TileSize = TileSize;

            var sizeX = Tiles.GetLength(0);
            var sizeY = Tiles.GetLength(1);
            var sizeZ = Tiles.GetLength(2);
            var tilesX = Tileset.FullSize.Width/TileSize;
            var tiles = new Tuple<int, int>[sizeX, sizeY, sizeZ];

            for (int i = 0; i < sizeX; i++)
                for (int j = 0; j < sizeY; j++)
                    for (int k = 0; k < sizeZ; k++)
                        tiles[i, j, k] = new Tuple<int, int>(Tiles[i, j, k]%tilesX, Tiles[i, j, k]/tilesX);

            this.Tiles = tiles;

            MinDepth = 0.5f;
            MaxDepth = 0.5f;
        }

        public CompositeSpriteHolder(ISprite Tileset, Tuple<int, int>[,,] Tiles, int TileSize)
        {
            if (!(Tileset.FullSize.Width/(float) TileSize).IsInteger() ||
                !(Tileset.FullSize.Height/(float) TileSize).IsInteger())
                throw new Exception("The TileSize doesn't fit the image.");

            this.Tileset = Tileset;
            this.Tiles = Tiles;
            this.TileSize = TileSize;

            MinDepth = 0.5f;
            MaxDepth = 0.5f;
        }

        public CompositeSpriteHolder(String TilesetName, int[,,] Tiles, int TileSize)
        {

            this.Tileset = ServiceLocator.ResourceService.GetOrLoad<ISprite>(TilesetName);

            if (!(Tileset.FullSize.Width/(float) TileSize).IsInteger() ||
                !(Tileset.FullSize.Height/(float) TileSize).IsInteger())
                throw new Exception("The TileSize doesn't fit the image.");

            this.TileSize = TileSize;

            var sizeX = Tiles.GetLength(0);
            var sizeY = Tiles.GetLength(1);
            var sizeZ = Tiles.GetLength(2);
            var tilesX = Tileset.FullSize.Width/TileSize;
            var tiles = new Tuple<int, int>[sizeX, sizeY, sizeZ];

            for (int i = 0; i < sizeX; i++)
                for (int j = 0; j < sizeY; j++)
                    for (int k = 0; k < sizeZ; k++)
                        tiles[i, j, k] = new Tuple<int, int>(Tiles[i, j, k]%tilesX, Tiles[i, j, k]/tilesX);

            this.Tiles = tiles;

            MinDepth = 0.5f;
            MaxDepth = 0.5f;
        }

        public CompositeSpriteHolder(String TilesetName, Tuple<int, int>[,,] Tiles, int TileSize)
        {
            this.Tileset = ServiceLocator.ResourceService.GetOrLoad<ISprite>(TilesetName);

            if (!(Tileset.FullSize.Width/(float) TileSize).IsInteger() ||
                !(Tileset.FullSize.Height/(float) TileSize).IsInteger())
                throw new Exception("The TileSize doesn't fit the image.");

            this.Tiles = Tiles;
            this.TileSize = TileSize;

            MinDepth = 0.5f;
            MaxDepth = 0.5f;
        }


        public void Update(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {

        }

        public void Draw(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {
            var screenCoordinates = Worldspawn.Camera.Viewport;
            var x = (int) ((screenCoordinates.Left/TileSize).Floor());
            var y = (int) ((screenCoordinates.Top/TileSize).Floor());
            var w = (int) ((screenCoordinates.Width/TileSize).Floor()) + 2;
            var h = (int) ((screenCoordinates.Height/TileSize).Floor()) + 2;

            var identity2D = Owner.QueryComponent<Identity2D>();
            var transform = identity2D.Transform;
            var region = Tileset.Region;
            var size = Tileset.Size;
            var emptyTransform = Transform2D.Identity;
            var depth = Tileset.Depth;

            Tileset.Size = new Size2D<int>(TileSize, TileSize);

            for (int i = x; i < x + w; i++)
            {
                for (int j = y; j < y + h; j++)
                {
                    for (int k = 0; k < Tiles.GetLength(2); k++)
                    {
                        var tile = GetTile(i, j, k);

                        if (tile == null)
                            continue;
                        if (tile.Item1 < 0 || tile.Item2 < 0)
                            continue;

                        Tileset.Region = new Rectangle<int>(tile.Item1*TileSize, tile.Item2*TileSize, TileSize, TileSize);
                        Tileset.Depth = Tiles.GetLength(2) == 1
                            ? MinDepth
                            : Maths.Lerp(MinDepth, MaxDepth, (float) k/(Tiles.GetLength(2) - 1));
                        emptyTransform.Position = new Vector2(i*TileSize, j*TileSize) - Center;
                        ServiceLocator.GraphicsService.Draw(Tileset,
                            Transform2D.Compose(emptyTransform, identity2D.CameraTransform));

                    }
                    emptyTransform.Position += new Vector2(0, TileSize);
                }
            }

            identity2D.Transform = transform;
            Tileset.Region = region;
            Tileset.Size = size;
            Tileset.Depth = depth;
        }

        private Tuple<int, int> GetTile(int I, int J, int K)
        {
            if (I < 0 || I >= Tiles.GetLength(0))
                return null;
            if (J < 0 || J >= Tiles.GetLength(1))
                return null;
            if (K < 0 || K >= Tiles.GetLength(2))
                return null;

            return Tiles[I, J, K];
        }

        public IGameComponent Clone()
        {
            return new CompositeSpriteHolder(Tileset, Tiles, TileSize);
        }
    }
}
