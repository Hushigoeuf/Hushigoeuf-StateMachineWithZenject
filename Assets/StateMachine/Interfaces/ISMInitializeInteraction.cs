namespace Hushigoeuf.StateMachine
{
    public interface ISMInitializeInteraction
    {
        bool Initialized { get; }
        void Initialize();
    }
}