namespace Hushigoeuf.StateMachine
{
    public abstract class SMAction :
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
        public bool IsEnabled { get; set; } = true;
        public bool IsEnabledAndSupported => StateInProgress && IsEnabled;

        public void Construct(SMStateMachine sm) => StateMachine = sm;
        public void Construct(object owner) => Owner = owner;

        public void Initialize()
        {
            if (Initialized) return;
            Initialized = true;
            OnActionInitialize();
        }

        public void Enter()
        {
            StateInProgress = true;
            OnActionEnter();
        }

        public void Exit()
        {
            StateInProgress = false;
            OnActionExit();
        }

        public virtual void OnUpdate()
        {
            if (!IsEnabledAndSupported) return;
            OnActionUpdate();
        }

        public void OnFixedUpdate()
        {
            if (!IsEnabledAndSupported) return;
            OnActionFixedUpdate();
        }

        public void OnLateUpdate()
        {
            if (!IsEnabledAndSupported) return;
            OnActionLateUpdate();
        }

        protected virtual void OnActionInitialize()
        {
        }

        protected virtual void OnActionEnter()
        {
        }

        protected virtual void OnActionExit()
        {
        }

        protected virtual void OnActionUpdate()
        {
        }

        protected virtual void OnActionFixedUpdate()
        {
        }

        protected virtual void OnActionLateUpdate()
        {
        }
    }

    public abstract class SMActionWithFrequency : SMAction, ISMFrequencyInteraction
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
            OnActionUpdateWithFrequency();
        }

        protected virtual void OnActionUpdateWithFrequency()
        {
        }
    }
}