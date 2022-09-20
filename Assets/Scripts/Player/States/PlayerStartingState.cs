using Hushigoeuf.StateMachine;
using Zenject;

namespace Hushigoeuf
{
    public class PlayerStartingState : SMState
    {
        [Inject] private readonly PlayerFallingState _fallingState;
        [Inject] private readonly PlayerJumpStartedDecision _jumpingDecision;

        protected override void SetTransitions(SMTransitionList transitionList)
        {
            transitionList.Subscribe(_jumpingDecision, _fallingState);
        }
    }
}