using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Freemwork.Utilities;
using Freemwork.Utilities.Attributes;
using Freemwork.World.Objects.Components.Graphics2D;

namespace Freemwork.World.Objects.Components.Debugging
{
    [NeededComponent(typeof(TextHolder))]
    public sealed class PropertyDisplayer : IGameComponent
    {
        public HashSet<Tuple<String, Object>> Properties { get; set; }
        
        public PropertyDisplayer(HashSet<Tuple<String, Object>> Properties)
        {
            this.Properties = Properties;
        }

        public void Update(Playstates.PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {
            var text = Owner.QueryComponent<TextHolder>();
            var builder = new StringBuilder();

            foreach (var property in Properties)
            {
                var split = property.Item1.Split(' ');
                var useful = split.First();
                var propName = useful.Split('.').Last();
                var className = useful.Substring(0, useful.Length - propName.Length - 1);
                
                var classType = Extensions.GetTypeInfo(Type.GetType(className));
                var propType = classType.GetDeclaredProperty(propName);
                var fieldType = classType.GetDeclaredField(propName);

                builder.Append(propName);
                if (split.Length > 1)
                {
                    builder.Append(" (");
                    builder.Append(property.Item1.Substring(useful.Length + 1));
                    builder.Append(")");
                }
                builder.Append(" : ");
                builder.Append(propType != null ? propType.GetValue(property.Item2) : fieldType.GetValue(property.Item2));
                builder.AppendLine();
            }

            text.Text.Text = builder.ToString();
        }

        public void Draw(Playstates.PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {

        }

        public IGameComponent Clone()
        {
            return new PropertyDisplayer(Properties);
        }
    }
}
