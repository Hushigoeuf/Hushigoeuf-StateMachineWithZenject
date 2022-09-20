using Hushigoeuf.StateMachine;
using Zenject;

namespace Hushigoeuf
{
    public class PlayerJumpStartedDecision : SMDecisionWithFrequency
    {
        [Inject] private readonly InputMemory _inputMemory;

        protected override void OnDecisionUpdate() =>
            Decided = _inputMemory.JumpButton.CurrentState == InputButton.ButtonStates.ButtonDown;
    }
}