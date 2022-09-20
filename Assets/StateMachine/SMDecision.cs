namespace Hushigoeuf.StateMachine
{
    public abstract class SMDecision :
        ISMConstructInteraction,
        ISMSwitchInteraction,
        ISMInitializeInteraction,
        ISMStateInteraction,
        ISMUpdateListener,
        ISMFixedUpdateListener,
        ISMLateUpdateListener
    {
        public SMStateMachine StateMachine { get; private set; }
        public object Owner { get; private set; }
        public bool Initialized { get; private set; }
        public bool StateInProgress { get; private set; }
        public bool Decided { get; protected set; }
        public bool IsEnabled { get; set; } = true;
        public bool IsEnabledAndSupported => StateInProgress && IsEnabled;

        public void Construct(SMStateMachine sm) => StateMachine = sm;
        public void Construct(object owner) => Owner = owner;

        public void Initialize()
        {
            if (Initialized) return;
            Initialized = true;
            OnDecisionInitialize();
        }

        public void Enter()
        {
            StateInProgress = true;
            OnDecisionEnter();
        }

        public void Exit()
        {
            StateInProgress = false;
            OnDecisionExit();
        }

        public virtual void OnUpdate()
        {
            Decided = false;
            if (!IsEnabledAndSupported) return;
            OnDecisionUpdate();
        }

        public void OnFixedUpdate()
        {
            Decided = false;
            if (!IsEnabledAndSupported) return;
            OnDecisionFixedUpdate();
        }

        public void OnLateUpdate()
        {
            Decided = false;
            if (!IsEnabledAndSupported) return;
            OnDecisionLateUpdate();
        }

        protected virtual void OnDecisionInitialize()
        {
        }

        protected virtual void OnDecisionEnter()
        {
        }

        protected virtual void OnDecisionExit()
        {
        }

        protected virtual void OnDecisionUpdate()
        {
        }

        protected virtual void OnDecisionFixedUpdate()
        {
        }

        protected virtual void OnDecisionLateUpdate()
        {
        }
    }

    public abstract class SMDecisionWithFrequency : SMDecision, ISMFrequencyInteraction
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
            OnDecisionUpdateWithFrequency();
        }

        protected virtual void OnDecisionUpdateWithFrequency()
        {
        }
    }
}