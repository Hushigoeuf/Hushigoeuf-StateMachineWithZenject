using UnityEngine;

namespace Hushigoeuf.StateMachine
{
    public static class SMDebug
    {
        public const string MESSAGE_SM_IS_ALREADY_STATE = "State machine is already state '{0}'.";
        public const string MESSAGE_CHANGE_STATE_TO_FROM = "Change state to '{0}' from '{1}'.";
        public const string MESSAGE_SM_IS_NOT_CAN_RESTORED = "State machine is not can restored.";
        public const string MESSAGE_DECISION_IS_DECIDED = "Decision '{0}' is decided to '{1}'.";

        public static void Log(object message)
        {
#if UNITY_EDITOR
            Debug.Log(message);
#endif
        }

        public static void LogError(object message)
        {
#if UNITY_EDITOR
            Debug.LogError(message);
#endif
        }

        public static void LogWarning(object message)
        {
#if UNITY_EDITOR
            Debug.LogWarning(message);
#endif
        }

        public static Color GetColorByMessageType(this SMDebugMessageType messageType)
        {
            var result = Color.white;
            switch (messageType)
            {
                case SMDebugMessageType.None:
                    result = Color.gray;
                    break;

                case SMDebugMessageType.Warning:
                    result = Color.yellow;
                    break;

                case SMDebugMessageType.Error:
                    result = Color.red;
                    break;
            }

            return result;
        }

        public static void CreateMessageWhenStateIsAlreadyActivated(
            this ISMDebugInteraction debugger, SMState targetState)
        {
            string message = string.Format(MESSAGE_SM_IS_ALREADY_STATE, targetState.GetType().Name);
            debugger.CreateMessage(message, SMDebugMessageType.Warning);
            LogWarning(message);
        }

        public static void CreateMessageWhenStateChanged(
            this ISMDebugInteraction debugger, SMState newState, SMState previousState)
        {
            string message = string.Format(MESSAGE_CHANGE_STATE_TO_FROM,
                newState?.GetType().Name ?? "null", previousState?.GetType().Name ?? "null");
            debugger.CreateMessage(message, SMDebugMessageType.Info);
        }

        public static void CreateMessageWhenStateIsNotCanRestored(this ISMDebugInteraction debugger)
        {
            string message = MESSAGE_SM_IS_NOT_CAN_RESTORED;
            debugger.CreateMessage(message, SMDebugMessageType.Warning);
            LogWarning(message);
        }

        public static void CreateMessageWhenDecisionIsDecided(
            this ISMDebugInteraction debugger, SMDecision decision, SMState targetState)
        {
            string message = string.Format(MESSAGE_DECISION_IS_DECIDED,
                decision.GetType().Name, targetState.GetType().Name);
            debugger.CreateMessage(message, SMDebugMessageType.None);
        }
    }
}