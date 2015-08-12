using System;

namespace Freemwork.Primitives.Systems.Automaton
{
    public struct FiniteTransition : IEquatable<FiniteTransition>
    {
        public String SourceState;
        public String DestinationState;
        public String Label;

        public FiniteTransition(String SourceState, String DestinationState, String Label)
        {
            this.SourceState = SourceState;
            this.DestinationState = DestinationState;
            this.Label = Label;
        }

        public bool Equals(FiniteTransition Other)
        {
            return string.Equals(SourceState, Other.SourceState) && string.Equals(DestinationState, Other.DestinationState) && string.Equals(Label, Other.Label);
        }

        public override bool Equals(object Object)
        {
            if (ReferenceEquals(null, Object)) return false;
            return Object is FiniteTransition && Equals((FiniteTransition)Object);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (SourceState != null ? SourceState.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (DestinationState != null ? DestinationState.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Label != null ? Label.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
