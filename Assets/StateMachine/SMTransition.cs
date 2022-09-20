namespace Hushigoeuf.StateMachine
{
    public class SMTransition : ISMTransition
    {
        public SMDecision Decision { get; }
        public SMState TargetState { get; }

        public SMTransition(SMDecision decision, SMState targetState)
        {
            Decision = decision;
            TargetState = targetState;
        }

        public bool Evaluate(out SMState targetState)
        {
            targetState = null;
            if (Decision.Decided) targetState = TargetState;
            return targetState != null;
        }
    }
}