using System;
using Hushigoeuf.StateMachine;
using Zenject;

namespace Hushigoeuf
{
    public abstract class SMStateMachineWithZenject : SMStateMachine,
        IInitializable,
        ITickable,
        IFixedTickable,
        ILateTickable,
        IDisposable
    {
        protected SMStateMachineWithZenject(SMState initialState, object owner) : base(initialState, owner)
        {
        }

        public void Initialize() => base.Initialize();
        public void Tick() => Update();
        public void FixedTick() => FixedUpdate();
        public void LateTick() => LateUpdate();
        public void Dispose() => base.Dispose();
    }
}