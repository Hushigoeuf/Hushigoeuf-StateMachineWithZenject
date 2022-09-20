namespace Hushigoeuf.StateMachine
{
    public class SMTransitionList : SMSubscriptionList<ISMTransition>
    {
        public void Subscribe(SMDecision decision, SMState targetState)
        {
            Subscribe(new SMTransition(decision, targetState));
        }
    }
}