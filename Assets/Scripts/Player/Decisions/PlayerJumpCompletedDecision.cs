using Hushigoeuf.StateMachine;
using Zenject;

namespace Hushigoeuf
{
    public class PlayerJumpCompletedDecision : SMDecisionWithFrequency
    {
        [Inject] private readonly PlayerJumpAction _jumpAction;

        protected override void OnDecisionUpdateWithFrequency() => Decided = _jumpAction.CurrentSpeed <= 0;
    }
}