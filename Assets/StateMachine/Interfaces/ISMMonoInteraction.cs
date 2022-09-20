using System;

namespace Hushigoeuf.StateMachine
{
    public interface ISMMonoInteraction : IDisposable
    {
        void Initialize();
        void Update();
        void FixedUpdate();
        void LateUpdate();
    }
}