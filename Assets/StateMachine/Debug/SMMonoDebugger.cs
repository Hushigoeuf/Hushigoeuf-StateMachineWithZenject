using System.Collections.Generic;
using UnityEngine;

namespace Hushigoeuf.StateMachine
{
    public class SMMonoDebugger : MonoBehaviour, ISMDebugInteraction
    {
        private const int MAX_COUNT_OF_FIRST_MESSAGES = 10;
        private const int MAX_COUNT_OF_LAST_MESSAGES = 10;

        public readonly List<SMDebugMessage> FirstMessages = new List<SMDebugMessage>();
        public readonly List<SMDebugMessage> LastMessages = new List<SMDebugMessage>();

        public SMStateMachine TargetStateMachine { get; private set; }
        public void Construct(SMStateMachine target) => TargetStateMachine = target;

        public void CreateMessage(string messageText, SMDebugMessageType messageType)
        {
#if !UNITY_EDITOR
            return;
#endif

            var message = new SMDebugMessage()
            {
                Index = GetLastIndexOfAllMessages(),
                MessageText = messageText,
                MessageType = messageType,
                MessageColor = messageType.GetColorByMessageType()
            };

            if (FirstMessages.Count < MAX_COUNT_OF_FIRST_MESSAGES)
                FirstMessages.Add(message);
            else LastMessages.Add(message);
            if (LastMessages.Count > MAX_COUNT_OF_LAST_MESSAGES)
                LastMessages.RemoveAt(0);
        }

        private int GetLastIndexOfAllMessages()
        {
            var result = 0;
            if (LastMessages.Count != 0)
                result = LastMessages[^1].Index + 1;
            else if (FirstMessages.Count != 0)
                result = FirstMessages.Count;
            return result;
        }
    }
}