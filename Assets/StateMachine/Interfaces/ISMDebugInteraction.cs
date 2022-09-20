namespace Hushigoeuf.StateMachine
{
    public interface ISMDebugInteraction
    {
        void CreateMessage(string messageText, SMDebugMessageType messageType);
    }
}