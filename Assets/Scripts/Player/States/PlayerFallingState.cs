using Hushigoeuf.StateMachine;
using Zenject;

namespace Hushigoeuf
{
    public class PlayerFallingState : SMState
    {
        [Inject] private readonly PlayerJumpingState _jumpingState;
        [Inject] private readonly PlayerFallingAction _fallingAction;
        [Inject] private readonly PlayerJumpStartedDecision _jumpingDecision;

        protected override void SetActions(SMActionList actionList)
        {
            actionList.Subscribe(_fallingAction);
        }

        protected override void SetTransitions(SMTransitionList transitionList)
        {
            transitionList.Subscribe(_jumpingDecision, _jumpingState);
        }
    }
}