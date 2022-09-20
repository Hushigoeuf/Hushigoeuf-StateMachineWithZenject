using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Hushigoeuf.StateMachine
{
    public class SMStateMachine : ISMMonoInteraction, ISMSwitchInteraction
    {
        public readonly SMState InitialState;
        public readonly object Owner;
        public readonly SMMonoDebugger MonoDebugger;

        public bool StateIsDefined => CurrentState != null;
        public SMState CurrentState { get; private set; }
        public SMState PreviousState { get; private set; }
        public ISMDebugInteraction Debugger { get; }
        public bool IsEnabled { get; set; } = true;
        public bool IsEnabledAndSupported => StateIsDefined && IsEnabled;

        public SMStateMachine(SMState initialState, object owner)
        {
            InitialState = initialState;
            Owner = owner;
            MonoDebugger = CreateMonoDebugger();
            Debugger = MonoDebugger;
        }

        private SMMonoDebugger CreateMonoDebugger()
        {
            var instance = new GameObject("Debugger - " + GetType().Name);
            if (Owner != null) instance.name += " (" + Owner.GetType().Name + ")";
            var debugger = instance.AddComponent<SMMonoDebugger>();
            debugger.Construct(this);
            return debugger;
        }

        public void Initialize()
        {
            if (InitialState != null)
                ChangeState(InitialState);
        }

        public void Update()
        {
            if (!IsEnabledAndSupported) return;
            CurrentState.OnUpdate();
            EvaluateTransitions();
        }

        public void FixedUpdate()
        {
            if (!IsEnabledAndSupported) return;
            CurrentState.OnFixedUpdate();
            EvaluateTransitions();
        }

        public void LateUpdate()
        {
            if (!IsEnabledAndSupported) return;
            CurrentState.OnLateUpdate();
            EvaluateTransitions();
        }

        public void Dispose()
        {
            if (MonoDebugger != null)
                Object.Destroy(MonoDebugger);
            (CurrentState as IDisposable)?.Dispose();
        }

        public void ChangeState(SMState newState)
        {
            Debugger.CreateMessageWhenStateChanged(newState, CurrentState);

            CurrentState?.Exit();
            {
                PreviousState = CurrentState;
                CurrentState = newState;
            }
            if (newState == null) return;

            CurrentState.Construct(this);
            CurrentState.Construct(Owner);
            if (!CurrentState.Initialized)
                CurrentState.Initialize();
            CurrentState.Enter();

            if (CurrentState.Equals(PreviousState))
                Debugger.CreateMessageWhenStateIsAlreadyActivated(PreviousState);
        }

        public void RestorePreviousState()
        {
            if (PreviousState == null)
            {
                Debugger.CreateMessageWhenStateIsNotCanRestored();
                return;
            }

            ChangeState(PreviousState);
        }

        private void EvaluateTransitions()
        {
            if (!StateIsDefined) return;

            foreach (var transition in CurrentState.TransitionList)
                if (transition.Evaluate(out var state))
                {
                    Debugger.CreateMessageWhenDecisionIsDecided(transition.Decision, state);
                    ChangeState(state);
                    return;
                }
        }
    }
}