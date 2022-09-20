namespace Hushigoeuf.StateMachine
{
    public interface ISMTransition
    {
        SMState TargetState { get; }
        SMDecision Decision { get; }
        bool Evaluate(out SMState targetState);
    }
}