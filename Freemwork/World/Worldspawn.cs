using System;
using System.Collections.Generic;
using System.Linq;
using Freemwork.Playstates;
using Freemwork.Primitives.Math;
using Freemwork.Utilities;
using Freemwork.View;
using Freemwork.World.Objects;
using Freemwork.World.Objects.Components;
using Freemwork.World.Objects.Components.Graphics2D;

namespace Freemwork.World
{
    public sealed class Worldspawn
    {
        private static int nextGOID = 0;
        private Dictionary<int, GameObject> gameObjects = new Dictionary<int, GameObject>(); 
        private Dictionary<Tuple<int, int>, HashSet<KeyValuePair<int, GameObject>>> partitionning2D = new Dictionary<Tuple<int, int>, HashSet<KeyValuePair<int, GameObject>>>(); 
        private readonly Primitives.EqualityComparer<KeyValuePair<int, GameObject>> gobComparer = new Primitives.EqualityComparer<KeyValuePair<int, GameObject>>((X, Y) => X.Key == Y.Key, X => X.Key.GetHashCode());

        public Dictionary<int, GameObject> GameObjects { get { return gameObjects; } }
        public Camera2D Camera { get; private set; }
        public bool SpacePartitionning { get; set; }
        public Size2D<int> CellSize2D { get; set; }

        public GameObject this[int GOID, bool ReplaceIfPresent = false]
        {
            get { return GameObjects[GOID]; }
            set
            {
                if (GameObjects.ContainsKey(GOID))
                {
                    if(ReplaceIfPresent)
                        GameObjects[GOID] = value;
                    else
                        throw new Exception("The GameObject with ID {0} is already set. " +
                                            "Set ReplaceIfPresent to true if you want to replace it instead.");
                }
                else
                    GameObjects.Add(GOID, value);
            }
        }

        public Worldspawn()
        {
            Camera = new Camera2D();
            SpacePartitionning = false;
        }

        public void Update(PlayState Owner)
        {
            foreach (var gameObject in GameObjects.ToList())
            {
                if(SpacePartitionning) BeginUpdatePartitionning(gameObject);
                gameObject.Value.Update(Owner, this, gameObject.Key);
                if (SpacePartitionning) EndUpdatePartitionning(gameObject);
            }

            Camera.Update(this);
        }

        private void BeginUpdatePartitionning(KeyValuePair<int, GameObject> GameObject)
        {
            var identity = GameObject.Value.QueryComponent<Identity2D>();
            if (identity == null)
                return;

            var transform = identity.GlobalTransform;
            var bounds = GameObject.Value.PartitionningBounds2D;

            if (bounds.HasValue)
            {
                var box = bounds.Value.Transform(transform);
                var cornerX = (int)Math.Floor(box.X / CellSize2D.Width);
                var cornerY = (int)Math.Floor(box.Y / CellSize2D.Height);
                var sizeX = (int)Math.Floor((box.X + box.Width) / CellSize2D.Width);
                var sizeY = (int)Math.Floor((box.Y + box.Height) / CellSize2D.Height);

                for (int i = cornerX; i != sizeX + 1; i++)
                {
                    for (int j = cornerY; j < sizeY + 1; j++)
                    {
                        var tuple = Tuple.Create(i, j);
                        if (partitionning2D.ContainsKey(tuple))
                            partitionning2D[tuple].Remove(GameObject);
                    }
                }
            }
            else
            {
                var global = transform;
                var x = (int)Math.Floor(global.Position.X / CellSize2D.Width);
                var y = (int)Math.Floor(global.Position.Y / CellSize2D.Height);

                var tuple = Tuple.Create(x, y);
                if (partitionning2D.ContainsKey(tuple))
                    partitionning2D[tuple].Remove(GameObject);
            }
        }

        private void EndUpdatePartitionning(KeyValuePair<int, GameObject> GameObject)
        {
            var identity = GameObject.Value.QueryComponent<Identity2D>();
            if(identity == null)
                return;

            var transform = identity.GlobalTransform;
            var bounds = GameObject.Value.PartitionningBounds2D;

            if (bounds.HasValue)
            {
                var box = bounds.Value.Transform(transform);
                var cornerX = (int)Math.Floor(box.X / CellSize2D.Width);
                var cornerY = (int)Math.Floor(box.Y / CellSize2D.Height);
                var sizeX = (int)Math.Floor((box.X + box.Width) / CellSize2D.Width);
                var sizeY = (int)Math.Floor((box.Y + box.Height) / CellSize2D.Height);

                for (int i = cornerX; i != sizeX + 1; i++)
                {
                    for (int j = cornerY; j < sizeY + 1; j++)
                    {
                        var tuple = Tuple.Create(i, j);
                        if (!partitionning2D.ContainsKey(tuple))
                            partitionning2D[tuple] = new HashSet<KeyValuePair<int, GameObject>>(gobComparer);
                        partitionning2D[tuple].Add(GameObject);
                    }
                }    
            }
            else
            {
                var global = transform;
                var x = (int)Math.Floor(global.Position.X / CellSize2D.Width);
                var y = (int)Math.Floor(global.Position.Y / CellSize2D.Height);

                var tuple = Tuple.Create(x, y);
                if (!partitionning2D.ContainsKey(tuple)) 
                    partitionning2D[tuple] = new HashSet<KeyValuePair<int, GameObject>>(gobComparer);
                partitionning2D[tuple].Add(GameObject);
            }

        }

        public void Draw(PlayState Owner)
        {
            foreach (var gameObject in GameObjects.ToList())
                gameObject.Value.Draw(Owner, this, gameObject.Key);
            Camera.Draw(this);
        }


        public HashSet<KeyValuePair<int, GameObject>> GetNearObjects(int GridX, int GridY, int Radius = 1)
        {
            if (!SpacePartitionning) throw new Exception("Space Partitionning is not enabled.");

            var list = new HashSet<Tuple<int, int>>();

            for (int i = -Radius; i != Radius + 1; i++)
                for (int j = -Radius; j != Radius + 1; j++)
                    list.Add(Tuple.Create(i + GridX, j + GridY));

            return GetGridsObjects(list);
        }

        public HashSet<KeyValuePair<int, GameObject>> GetNearObjects<T>(int GridX, int GridY, int Radius = 1) where T : IGameComponent
        {
            return GetNearObjects(GridX, GridY, typeof(T), Radius);
        }

        public HashSet<KeyValuePair<int, GameObject>> GetNearObjects(GameObject Object, Type ComponentType, int Radius = 1)
        {
            var objects = GetNearObjects(Object, Radius);
            return new HashSet<KeyValuePair<int, GameObject>>(objects.Where(Obj => Obj.Value.HasComponent(ComponentType)), gobComparer);
        }

        public HashSet<KeyValuePair<int, GameObject>> GetNearObjects(GameObject Object, Type[] ComponentTypes, int Radius = 1)
        {
            var objects = GetNearObjects(Object, Radius);
            return new HashSet<KeyValuePair<int, GameObject>>(objects.Where(Obj => ComponentTypes.Any(It => Obj.Value.HasComponent(It))), gobComparer);
        }

        public HashSet<KeyValuePair<int, GameObject>> GetNearObjects(GameObject Object, int Radius = 1, params Type[] ComponentTypes)
        {
            var objects = GetNearObjects(Object, Radius);
            return new HashSet<KeyValuePair<int, GameObject>>(objects.Where(Obj => ComponentTypes.Any(It => Obj.Value.HasComponent(It))), gobComparer);
        }

        public HashSet<KeyValuePair<int, GameObject>> GetNearObjects<T>(GameObject Object, int Radius = 1) where T : IGameComponent
        {
            return GetNearObjects(Object, typeof(T), Radius);
        }

        public HashSet<KeyValuePair<int, GameObject>> GetNearObjects(GameObject Object, int Radius = 1)
        {
            if (!SpacePartitionning) throw new Exception("Space Partitionning is not enabled.");

            var identity = Object.QueryComponent<Identity2D>();
            var transform = identity.GlobalTransform;
            var bounds = Object.PartitionningBounds2D;

            if (bounds.HasValue)
            {
                var box = bounds.Value.Transform(transform);
                var cornerX = (int)Math.Floor(box.X / CellSize2D.Width);
                var cornerY = (int)Math.Floor(box.Y / CellSize2D.Height);
                var sizeX = (int)Math.Floor((box.X + box.Width) / CellSize2D.Width);
                var sizeY = (int)Math.Floor((box.Y + box.Height) / CellSize2D.Height);

                int w = sizeX - cornerX;
                int h = sizeY - cornerY;

                var list = new HashSet<Tuple<int, int>>();

                for (int i = -Radius; i != (w + Radius) + 1; i++)
                    for (int j = -Radius; j != (h + Radius) + 1; j++)
                        list.Add(Tuple.Create(i + cornerX, j + cornerY));

                return GetGridsObjects(list);
            }

            var global = transform;
            var x = (int)Math.Floor(global.Position.X / CellSize2D.Width);
            var y = (int)Math.Floor(global.Position.Y / CellSize2D.Height);

            return GetNearObjects(x, y, Radius);
        }

        public HashSet<KeyValuePair<int, GameObject>> GetNearObjects(int GridX, int GridY, Type ComponentType, int Radius = 1)
        {
            var objects = GetNearObjects(GridX, GridY, Radius);
            return new HashSet<KeyValuePair<int, GameObject>>(objects.Where(Obj => Obj.Value.HasComponent(ComponentType)), gobComparer);
        }

        public HashSet<KeyValuePair<int, GameObject>> GetOnScreenObjects()
        {
            if (!SpacePartitionning) throw new Exception("Space Partitionning is not enabled.");
            
            var viewport = Camera.Viewport;
            var a = Tuple.Create((int)Maths.Round(viewport.X / CellSize2D.Width), (int)Maths.Round(viewport.Y / CellSize2D.Height));
            var b = Tuple.Create((int)Maths.Round((viewport.X + viewport.Width) / CellSize2D.Width), (int)Maths.Round((viewport.Y + viewport.Height) / CellSize2D.Height));
            var inf = Tuple.Create(Maths.Min(a.Item1, b.Item1), Maths.Min(a.Item2, b.Item2));
            var sup = Tuple.Create(Maths.Max(a.Item2, b.Item2), Maths.Max(a.Item2, b.Item2));
            var cells = new List<Tuple<int, int>>();

            for (int i = inf.Item1; i != sup.Item1 + 1 ; i++)
                for (int j = inf.Item2; j != sup.Item2 + 1; j++)
                    cells.Add(Tuple.Create(i, j));

            return GetGridsObjects(cells);
        }

        public HashSet<KeyValuePair<int, GameObject>> GetGridObjects(int X, int Y)
        {
            if (!SpacePartitionning) throw new Exception("Space Partitionning is not enabled.");
            return partitionning2D[Tuple.Create(X, Y)];
        }

        public HashSet<KeyValuePair<int, GameObject>> GetGridObjects(Tuple<int, int> Grid)
        {
            if (!SpacePartitionning) throw new Exception("Space Partitionning is not enabled.");
            return partitionning2D[Grid];
        }

        public HashSet<KeyValuePair<int, GameObject>> GetGridsObjects(IEnumerable<Tuple<int, int>> Grids)
        {
            if (!SpacePartitionning) throw new Exception("Space Partitionning is not enabled.");

            var gridIds = new HashSet<KeyValuePair<int, GameObject>>(gobComparer);
            foreach (var tuple in Grids)
                if(partitionning2D.ContainsKey(tuple)) 
                    gridIds.UnionWith(partitionning2D[tuple]);

            return gridIds;
        }



        public GameObject QueryGameObject(String Tag)
        {
            return GameObjects.FirstOrDefault(Co => Co.Value.Tag == Tag).Value;
        }

        public int QueryGameObjectID(String Tag)
        {
            return GameObjects.FirstOrDefault(Co => Co.Value.Tag == Tag).Key;
        }

        public KeyValuePair<int, GameObject> QueryGameObjectAndID(String Tag)
        {
            return GameObjects.FirstOrDefault(Co => Co.Value.Tag == Tag);
        }
        
        public int GetNextGOID()
        {
            while (GameObjects.ContainsKey(nextGOID))
                nextGOID++;
            return nextGOID;
        }
    }
}
