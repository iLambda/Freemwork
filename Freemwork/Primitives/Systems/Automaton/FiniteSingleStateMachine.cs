using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Freemwork.Primitives.Systems.Automaton
{
    public class FiniteSingleStateMachine
    {
        private ObservableCollection<IFiniteState> states = new ObservableCollection<IFiniteState>();
        private HashSet<FiniteTransition> transitions = new HashSet<FiniteTransition>();
        private String currentStateId = null;
        private IFiniteState currentState = null;

        public IList<IFiniteState> States { get { return states; } }
        public ISet<FiniteTransition> Transitions { get { return transitions; } }
        public IFiniteState CurrentState { get { return currentState; } protected set { currentState = value; } }
        public String CurrentStateIdentifier { get { return currentStateId; } protected set { currentStateId = value; } }

        public FiniteSingleStateMachine(IFiniteState StartState)
        {
            states.CollectionChanged += OnStateCollectionChanged;

            States.Add(StartState);
            CurrentState = StartState;
            CurrentStateIdentifier = StartState.Identifier;
        }

        public FiniteSingleStateMachine(String StartState, params IFiniteState[] States)
        {
            states.CollectionChanged += OnStateCollectionChanged;

            foreach (var finiteState in States)
                states.Add(finiteState);

            var adequateState = States.Single(St => St.Identifier == StartState);
            CurrentState = adequateState;
            CurrentStateIdentifier = adequateState.Identifier;
        }

        public FiniteSingleStateMachine(String StartState, IEnumerable<IFiniteState> States)
        {
            states.CollectionChanged += OnStateCollectionChanged;

            var finiteStates = States as IFiniteState[] ?? States.ToArray();
            foreach (var finiteState in finiteStates)
                states.Add(finiteState);

            var adequateState = finiteStates.Single(St => St.Identifier == StartState);
            CurrentState = adequateState;
            CurrentStateIdentifier = adequateState.Identifier;
        }

        private void OnStateCollectionChanged(object Sender, NotifyCollectionChangedEventArgs Args)
        {
            switch (Args.Action)
            {
                case NotifyCollectionChangedAction.Replace:
                    foreach (var state in Args.NewItems.OfType<IFiniteState>())
                        state.RequestTransition += OnTransitionRequested;
                    foreach (var state in Args.OldItems.OfType<IFiniteState>())
                        state.RequestTransition -= OnTransitionRequested;
                    break;
                case NotifyCollectionChangedAction.Add:
                    foreach (var state in Args.NewItems.OfType<IFiniteState>())
                        state.RequestTransition += OnTransitionRequested;
                    break;
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Reset:
                    foreach (var state in Args.OldItems.OfType<IFiniteState>())
                        state.RequestTransition -= OnTransitionRequested;
                    break;
            }
        }

        private void OnTransitionRequested(IFiniteState Sender, String Label)
        {
            var count = transitions.Count(Tr => Tr.SourceState == Sender.Identifier && Tr.Label == Label);
            if (count == 0) return;

            var adequateTransition = transitions.Single(Tr => Tr.SourceState == Sender.Identifier && Tr.Label == Label);
            currentStateId = adequateTransition.DestinationState;
            currentState = States.Single(St => St.Identifier == adequateTransition.DestinationState);
        }        
    }

    public class FiniteSingleStateMachine<T> where T : class, IFiniteState
    {
        private ObservableCollection<T> states = new ObservableCollection<T>();
        private HashSet<FiniteTransition> transitions = new HashSet<FiniteTransition>();
        private String currentStateId = null;
        private T currentState = null;

        public IList<T> States { get { return states; } }
        public ISet<FiniteTransition> Transitions { get { return transitions; } }
        public T CurrentState { get { return currentState; } protected set { currentState = value; } }
        public String CurrentStateIdentifier { get { return currentStateId; } protected set { currentStateId = value; } }


        public FiniteSingleStateMachine(T StartState)
        {
            states.CollectionChanged += OnStateCollectionChanged;

            States.Add(StartState);
            CurrentState = StartState;
            CurrentStateIdentifier = StartState.Identifier;
        }

        public FiniteSingleStateMachine(String StartState, params T[] States)
        {
            states.CollectionChanged += OnStateCollectionChanged;

            foreach (var finiteState in States)
                states.Add(finiteState);

            var adequateState = States.Single(St => St.Identifier == StartState);
            CurrentState = adequateState;
            CurrentStateIdentifier = adequateState.Identifier;
        }

        public FiniteSingleStateMachine(String StartState, IEnumerable<T> States)
        {
            states.CollectionChanged += OnStateCollectionChanged;

            var finiteStates = States as T[] ?? States.ToArray();
            foreach (var finiteState in finiteStates)
                states.Add(finiteState);

            var adequateState = finiteStates.Single(St => St.Identifier == StartState);
            CurrentState = adequateState;
            CurrentStateIdentifier = adequateState.Identifier;
        }

        private void OnStateCollectionChanged(object Sender, NotifyCollectionChangedEventArgs Args)
        {
            switch (Args.Action)
            {
                case NotifyCollectionChangedAction.Replace:
                    foreach (var state in Args.NewItems.OfType<T>())
                        state.RequestTransition += OnTransitionRequested;
                    foreach (var state in Args.OldItems.OfType<T>())
                        state.RequestTransition -= OnTransitionRequested;
                    break;
                case NotifyCollectionChangedAction.Add:
                    foreach (var state in Args.NewItems.OfType<T>())
                        state.RequestTransition += OnTransitionRequested;
                    break;
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Reset:
                    foreach (var state in Args.OldItems.OfType<T>())
                        state.RequestTransition -= OnTransitionRequested;
                    break;
            }
        }

        private void OnTransitionRequested(IFiniteState Sender, String Label)
        {
            var count = transitions.Count(Tr => Tr.SourceState == Sender.Identifier && Tr.Label == Label);
            if (count == 0) return;

            var adequateTransition = transitions.Single(Tr => Tr.SourceState == Sender.Identifier && Tr.Label == Label);
            currentStateId = adequateTransition.DestinationState;
            currentState = States.Single(St => St.Identifier == adequateTransition.DestinationState);
        }
    }
}
