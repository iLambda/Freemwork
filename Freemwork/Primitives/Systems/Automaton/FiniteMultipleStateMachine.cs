using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Freemwork.Primitives.Systems.Automaton
{
    public class FiniteMultipleStateMachine
    {
        private ObservableCollection<IFiniteState> states = new ObservableCollection<IFiniteState>();
        private HashSet<FiniteTransition> transitions = new HashSet<FiniteTransition>();
        private List<String> currentStateId = new List<string>();
        private List<IFiniteState> currentState = new List<IFiniteState>();

        public IList<IFiniteState> States { get { return states; } }
        public ISet<FiniteTransition> Transitions { get { return transitions; } }
        public List<IFiniteState> CurrentStates { get { return currentState; } protected set { currentState = value; } }
        public List<String> CurrentStatesIdentifier { get { return currentStateId; } protected set { currentStateId = value; } }


        public FiniteMultipleStateMachine()
        {
            states.CollectionChanged += OnStateCollectionChanged;
        }

        public FiniteMultipleStateMachine(params IFiniteState[] StartStates)
        {
            states.CollectionChanged += OnStateCollectionChanged;

            foreach (var finiteState in StartStates)
                States.Add(finiteState);

            CurrentStates = StartStates.ToList();
            CurrentStatesIdentifier = CurrentStates.Select(St => St.Identifier).ToList();
        }

        public FiniteMultipleStateMachine(IEnumerable<string> StartStates, params IFiniteState[] States)
        {         
            states.CollectionChanged += OnStateCollectionChanged;

            foreach (var finiteState in States)
                states.Add(finiteState);

            CurrentStates = States.Where(St => StartStates.Contains(St.Identifier)).ToList();
            CurrentStatesIdentifier = CurrentStates.Select(St => St.Identifier).ToList();
        }

        public FiniteMultipleStateMachine(IEnumerable<string> StartStates, IEnumerable<IFiniteState> States)
        {
            states.CollectionChanged += OnStateCollectionChanged;

            foreach (var finiteState in States)
                states.Add(finiteState);

            CurrentStates = states.Where(St => StartStates.Contains(St.Identifier)).ToList();
            CurrentStatesIdentifier = CurrentStates.Select(St => St.Identifier).ToList();
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
            var adequateTransitions = transitions.Where(Tr => Tr.SourceState == Sender.Identifier && Tr.Label == Label);
            CurrentStates.Remove(Sender);
            var added = adequateTransitions.Select(St => St.DestinationState).ToArray();
            CurrentStatesIdentifier.AddRange(added);
            CurrentStates.AddRange(States.Where(St => added.Contains(St.Identifier)));
        }        
    }

    public class FiniteMultipleStateMachine<T> where T : IFiniteState
    {
        private ObservableCollection<T> states = new ObservableCollection<T>();
        private HashSet<FiniteTransition> transitions = new HashSet<FiniteTransition>();
        private List<String> currentStateId = new List<string>();
        private List<T> currentState = new List<T>();

        public IList<T> States { get { return states; } }
        public ISet<FiniteTransition> Transitions { get { return transitions; } }
        public List<T> CurrentStates { get { return currentState; } protected set { currentState = value; } }
        public List<String> CurrentStatesIdentifier { get { return currentStateId; } protected set { currentStateId = value; } }


        public FiniteMultipleStateMachine()
        {
            states.CollectionChanged += OnStateCollectionChanged;
        }

        public FiniteMultipleStateMachine(params T[] StartStates)
        {
            states.CollectionChanged += OnStateCollectionChanged;

            foreach (var finiteState in StartStates)
                States.Add(finiteState);

            CurrentStates = StartStates.ToList();
            CurrentStatesIdentifier = CurrentStates.Select(St => St.Identifier).ToList();
        }

        public FiniteMultipleStateMachine(IEnumerable<string> StartStates, params T[] States)
        {
            states.CollectionChanged += OnStateCollectionChanged;

            foreach (var finiteState in States)
                states.Add(finiteState);

            CurrentStates = States.Where(St => StartStates.Contains(St.Identifier)).ToList();
            CurrentStatesIdentifier = CurrentStates.Select(St => St.Identifier).ToList();
        }

        public FiniteMultipleStateMachine(IEnumerable<string> StartStates, IEnumerable<T> States)
        {
            states.CollectionChanged += OnStateCollectionChanged;

            foreach (var finiteState in States)
                states.Add(finiteState);

            CurrentStates = states.Where(St => StartStates.Contains(St.Identifier)).ToList();
            CurrentStatesIdentifier = CurrentStates.Select(St => St.Identifier).ToList();
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
            var adequateTransitions = transitions.Where(Tr => Tr.SourceState == Sender.Identifier && Tr.Label == Label);
            CurrentStates.Remove((T)Sender);
            var added = adequateTransitions.Select(St => St.DestinationState).ToArray();
            CurrentStatesIdentifier.AddRange(added);
            CurrentStates.AddRange(States.Where(St => added.Contains(St.Identifier)));
        }
    }
}
