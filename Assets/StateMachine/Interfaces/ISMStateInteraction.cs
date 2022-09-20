namespace Hushigoeuf.StateMachine
{
    public interface ISMStateInteraction
    {
        public bool StateInProgress { get; }
        public void Enter();
        public void Exit();
    }
}