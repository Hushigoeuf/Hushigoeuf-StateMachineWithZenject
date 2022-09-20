#define SM_DEBUG_ENABLED

using System;

namespace Hushigoeuf.StateMachine
{
    public abstract class SMState :
        ISMConstructInteraction,
        ISMSwitchInteraction,
        ISMInitializeInteraction,
        ISMStateInteraction,
        ISMUpdateListener,
        ISMFixedUpdateListener,
        ISMLateUpdateListener,
        IDisposable
    {
        public SMActionList ActionList { get; } = new SMActionList();
        public SMTransitionList TransitionList { get; } = new SMTransitionList();
        public SMStateMachine StateMachine { get; private set; }
        public object Owner { get; private set; }
        public bool Initialized { get; private set; }
        public bool StateInProgress { get; private set; }
        public bool IsEnabled { get; set; } = true;
        public bool IsEnabledAndSupported => StateInProgress && IsEnabled;

        public void Construct(SMStateMachine sm) => StateMachine = sm;
        public void Construct(object owner) => Owner = owner;

        public void Initialize()
        {
            if (Initialized) return;
            Initialized = true;

            SetActions(ActionList);
            SetTransitions(TransitionList);
            OnStateInitialize();

            foreach (var action in ActionList)
                if (!action.Initialized)
                    action.Initialize();
            foreach (var transition in TransitionList)
                if (!transition.Decision.Initialized)
                    transition.Decision.Initialize();
        }

        protected virtual void SetActions(SMActionList actionList)
        {
        }

        protected virtual void SetTransitions(SMTransitionList transitionList)
        {
        }

        public void Enter()
        {
            StateInProgress = true;
            OnStateEnter();

            foreach (var action in ActionList) action.Enter();
            foreach (var transition in TransitionList) transition.Decision.Enter();
        }

        public void Exit()
        {
            StateInProgress = false;
            OnStateExit();

            foreach (var action in ActionList) action.Exit();
            foreach (var transition in TransitionList) transition.Decision.Exit();
        }

        public virtual void OnUpdate()
        {
            if (!IsEnabledAndSupported) return;
            OnStateUpdate();

            foreach (var action in ActionList) action.OnUpdate();
            foreach (var transition in TransitionList) transition.Decision.OnUpdate();
        }

        public void OnFixedUpdate()
        {
            if (!IsEnabledAndSupported) return;
            OnStateFixedUpdate();

            foreach (var action in ActionList) action.OnFixedUpdate();
            foreach (var transition in TransitionList) transition.Decision.OnFixedUpdate();
        }

        public void OnLateUpdate()
        {
            if (!IsEnabledAndSupported) return;
            OnStateLateUpdate();

            foreach (var action in ActionList) action.OnLateUpdate();
            foreach (var transition in TransitionList) transition.Decision.OnLateUpdate();
        }

        public virtual void Dispose()
        {
            foreach (var action in ActionList)
                (action as IDisposable)?.Dispose();
            foreach (var transition in TransitionList)
                (transition.Decision as IDisposable)?.Dispose();
        }

        protected virtual void OnStateInitialize()
        {
        }

        protected virtual void OnStateEnter()
        {
        }

        protected virtual void OnStateExit()
        {
        }

        protected virtual void OnStateUpdate()
        {
        }

        protected virtual void OnStateFixedUpdate()
        {
        }

        protected virtual void OnStateLateUpdate()
        {
        }
    }

    public abstract class SMStateWithFrequency : SMState, ISMFrequencyInteraction
    {
        public readonly SMFrequencyHandler FrequencyHandler = new SMFrequencyHandler();

        public float Frequency
        {
            get => FrequencyHandler.Value;
            set => FrequencyHandler.Value = value;
        }

        public sealed override void OnUpdate()
        {
            base.OnUpdate();

            if (!IsEnabledAndSupported) return;
            if (!FrequencyHandler.CheckAndUpdate()) return;
            OnStateUpdateWithFrequency();
        }

        protected virtual void OnStateUpdateWithFrequency()
        {
        }
    }
}