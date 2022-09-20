using Zenject;

namespace Hushigoeuf
{
    public class PlayerStateMachine : SMStateMachineWithZenject
    {
        [Inject]
        public PlayerStateMachine(PlayerStartingState startingState, Player owner) : base(startingState, owner)
        {
        }
    }
}