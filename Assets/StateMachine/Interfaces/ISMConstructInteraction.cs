namespace Hushigoeuf.StateMachine
{
    public interface ISMConstructInteraction
    {
        SMStateMachine StateMachine { get; }
        object Owner { get; }
        void Construct(SMStateMachine sm);
        void Construct(object owner);
    }
}