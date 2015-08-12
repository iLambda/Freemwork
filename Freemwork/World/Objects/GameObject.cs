using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using Freemwork.Playstates;
using Freemwork.Primitives.Math;
using Freemwork.Utilities;
using Freemwork.Utilities.Attributes;
using Freemwork.World.Objects.Components;

namespace Freemwork.World.Objects
{
    public delegate void ComponentAddedEventHandler(GameObject Sender, IGameComponent Component);
    public delegate void ComponentRemovedEventHandler(GameObject Sender, IGameComponent Component);

    public sealed class GameObject
    {
        private ObservableCollection<IGameComponent> components = new ObservableCollection<IGameComponent>();
        private Type partitionningBoundsComponent = null;
        private PropertyInfo boundsProperty2D = null;
        private PropertyInfo boundsProperty3D = null;

        public IList<IGameComponent> Components { get { return components; } }
        public String Tag { get; set; }
        public Object AttachedData { get; set; }
        public IGameComponent this[Type Type]
        {
            get { return QueryComponent(Type); }
            set { Components.Add(value); }
        }

        public Type PartitionningBoundsComponent
        {
            get { return partitionningBoundsComponent; }
            set
            {
                if (partitionningBoundsComponent == value) return;
                partitionningBoundsComponent = value;
                if (partitionningBoundsComponent == null)
                {
                    boundsProperty2D = null;
                    boundsProperty3D = null;
                    partitionningBoundsComponent = null;
                    return;
                }

#if NETFX_CORE
                var attributes = Extensions.GetTypeInfo(partitionningBoundsComponent).GetCustomAttributes().ToArray();
#else
                var attributes = System.Attribute.GetCustomAttributes(GameComponent.GetType());
#endif
                var attribute = attributes.OfType<BoundsDefiningPropertyAttribute>().First();
                var propName = attribute.PropertyName;
                var prop = Extensions.GetTypeInfo(partitionningBoundsComponent).GetDeclaredProperty(propName);

                if(prop == null)
                    throw new Exception("The property name {0} doesn't exist on the type {1}.".FormatString(propName, partitionningBoundsComponent));

                if (prop.PropertyType == typeof (Rectangle<float>))
                {
                    boundsProperty2D = prop;
                    boundsProperty3D = null;
                }
                else
                {
                    boundsProperty2D = null;
                    boundsProperty3D = null;
                    
                    throw new Exception("The given size defining property is not a Rectangle<float>, nor a Box<float>.");
                }
            }
        }

        public Rectangle<float>? PartitionningBounds2D
        {
            get { return boundsProperty2D != null ? (Rectangle<float>?)boundsProperty2D.GetValue(QueryComponent(PartitionningBoundsComponent, true)) : null; }
        }

        public event ComponentAddedEventHandler ComponentAdded;
        public event ComponentRemovedEventHandler ComponentRemoved;

        public GameObject()
        {   
            components.CollectionChanged += OnComponentsChanged;
        }

        public void Update(PlayState CurrentState, Worldspawn Worldspawn, int GOID)
        {
            foreach (var component in Components)
                component.Update(CurrentState, Worldspawn, this, GOID);
        }

        public void Draw(PlayState CurrentState, Worldspawn Worldspawn, int GOID)
        {
            foreach (var component in Components)
                component.Draw(CurrentState, Worldspawn, this, GOID);
        }

        public bool HasComponent(Type Type, bool CheckSubclasses = false)
        {
            return CheckSubclasses
                ? Components.Any(Co => Extensions.GetTypeInfo(Type).IsAssignableFrom(Extensions.GetTypeInfo(Co.GetType()))) 
                : Components.Any(Co => Co.GetType() == Type);
        }
        public bool HasComponent<T>(bool CheckSubclasses = false)
        {
            return HasComponent(typeof (T), CheckSubclasses);
        }

        public T QueryComponent<T>(bool CheckSubclasses = false) where T : IGameComponent
        {
            return (T)QueryComponent(typeof(T), CheckSubclasses);
        }

        public GameObject Clone(bool CopyTag = false, bool CopyAttachedData = false)
        {
            var gameObject = new GameObject();
            
            if (CopyTag) gameObject.Tag = Tag;
            if (CopyAttachedData) gameObject.AttachedData = AttachedData;
            
            foreach (var gameComponent in Components)
                gameObject.Components.Add(gameComponent.Clone());

            gameObject.PartitionningBoundsComponent = PartitionningBoundsComponent;

            return gameObject;
        }

        public IGameComponent QueryComponent(Type Type, bool CheckSubclasses = false)
        {
            if (!Extensions.GetTypeInfo(typeof(IGameComponent)).IsAssignableFrom(Extensions.GetTypeInfo(Type)))
                throw new Exception("Type provided is not a component.");

            return CheckSubclasses 
                ? Components.FirstOrDefault(C => Extensions.GetTypeInfo(Type).IsAssignableFrom(Extensions.GetTypeInfo(C.GetType()))) 
                : Components.FirstOrDefault(C => C.GetType() == Type);
        }

        private void OnComponentsChanged(object Sender, NotifyCollectionChangedEventArgs Args)
        {
            switch (Args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var gameComponent in Args.NewItems.OfType<IGameComponent>())
                    {
                        AddComponentTest(gameComponent);
                        if (ComponentAdded != null) 
                            ComponentAdded(this, gameComponent);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                case NotifyCollectionChangedAction.Remove:
                    foreach (var gameComponent in Args.OldItems.OfType<IGameComponent>())
                        if (ComponentRemoved != null) ComponentRemoved(this, gameComponent);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    foreach (var gameComponent in Args.NewItems.OfType<IGameComponent>())
                    {
                        AddComponentTest(gameComponent);
                        if (ComponentAdded != null)
                            ComponentAdded(this, gameComponent);
                    }
                    foreach (var gameComponent in Args.OldItems.OfType<IGameComponent>())
                        if (ComponentRemoved != null) ComponentRemoved(this, gameComponent);
                    break;
            }
        }

        private void AddComponentTest(IGameComponent GameComponent)
        {
            if(Components.Count(Co => Co.GetType() == GameComponent.GetType()) > 1)
                throw new Exception("There is already a {0} component.".FormatString(GameComponent.GetType()));

#if NETFX_CORE
            var attributes = Extensions.GetTypeInfo(GameComponent.GetType()).GetCustomAttributes().ToArray();
#else
            var attributes = System.Attribute.GetCustomAttributes(GameComponent.GetType());
#endif       
            Type neededType = default(Type);
            Type uncompatibleType = default(Type);
            
            if (attributes.OfType<NeededComponentAttribute>().Any(At => Components.All(It => { neededType = At.NeededType; return !(Extensions.GetTypeInfo(At.NeededType).IsAssignableFrom(Extensions.GetTypeInfo(It.GetType()))); })))
                throw new Exception("{0} needs a {1} component to work properly.".FormatString(GameComponent.GetType(), neededType));
            if (attributes.OfType<UncompatibleComponentAttribute>().Any(At => Components.Any(It => { uncompatibleType = At.UncompatibleType; return Extensions.GetTypeInfo(At.UncompatibleType).IsAssignableFrom(Extensions.GetTypeInfo(It.GetType())); })))
                throw new Exception("{0} cannot coexist with a {1} component.".FormatString(GameComponent.GetType(), uncompatibleType));
        }


    }
}
