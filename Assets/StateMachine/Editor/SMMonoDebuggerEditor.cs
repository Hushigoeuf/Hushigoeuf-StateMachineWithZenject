#if ODIN_INSPECTOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Hushigoeuf.StateMachine
{
    [CustomEditor(typeof(SMMonoDebugger))]
    public sealed class SMMonoDebuggerEditor : OdinEditor
    {
        private const string TITLE_STATE_MACHINE = "State machine";
        private const string TITLE_CURRENT_STATE = "Current state";
        private const string TITLE_ACTION_LIST = "Action list";
        private const string TITLE_DECISION_LIST = "Transition list with decisions";
        private const string TITLE_HISTORY_LIST = "History messages";
        private const string MESSAGE_STATE_IS_NOT_DEFINED = "State is not defined.";
        private const float MIN_FREQUENCY = 0;
        private const float MAX_FREQUENCY = 1;
        private const float SPACE_WIDTH_PER_CONTAINERS = 16;

        private SMMonoDebugger TargetDebugger => (SMMonoDebugger) target;
        private SMStateMachine TargetStateMachine => TargetDebugger.TargetStateMachine;
        private SMState TargetState => TargetStateMachine.CurrentState;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SirenixEditorGUI.BeginBox();
            Space();
            {
                DrawStateMachineContainer();
                DrawCurrentStateContainer();
                DrawActionListContainer();
                DrawDecisionListContainer();
                DrawHistoryContainer();
            }
            SirenixEditorGUI.EndBox();
        }

        private void Title(string title)
        {
            SirenixEditorGUI.Title(title.ToUpper(), "", TextAlignment.Left, true, true);
        }

        private void Space()
        {
            EditorGUILayout.Space(SPACE_WIDTH_PER_CONTAINERS);
        }

        private void DrawStateMachineContainer()
        {
            if (TargetStateMachine == null) return;

            Title(TITLE_STATE_MACHINE);
            DrawItemComponent(TargetStateMachine);
            Space();
        }

        private void DrawCurrentStateContainer()
        {
            Title(TITLE_CURRENT_STATE);

            if (TargetState != null)
                DrawItemComponent(TargetState);
            else SirenixEditorGUI.InfoMessageBox(MESSAGE_STATE_IS_NOT_DEFINED);

            if (TargetStateMachine.PreviousState != null)
                SirenixEditorGUI.MessageBox(nameof(TargetStateMachine.PreviousState) + ": " +
                                            TargetStateMachine.PreviousState.GetType().Name, MessageType.None);

            Space();
        }

        private void DrawActionListContainer()
        {
            if (!TargetStateMachine.StateIsDefined) return;
            if (TargetState.ActionList.Count == 0) return;

            Title(TITLE_ACTION_LIST);
            foreach (var action in TargetState.ActionList)
                DrawItemComponent(action);
            Space();
        }

        private void DrawDecisionListContainer()
        {
            if (!TargetStateMachine.StateIsDefined) return;
            if (TargetState.TransitionList.Count == 0) return;

            Title(TITLE_DECISION_LIST);
            foreach (var transition in TargetState.TransitionList)
                DrawItemComponent(transition.Decision,
                    transition.Decision.GetType().Name + " => " + transition.TargetState.GetType().Name);
            Space();
        }

        private void DrawHistoryContainer()
        {
            if (TargetDebugger.FirstMessages.Count + TargetDebugger.LastMessages.Count == 0) return;

            void drawMessage(SMDebugMessage message)
            {
                drawMessageByData(message.Index + ": " + message.MessageText, message.MessageColor);
            }

            void drawMessageByData(string messageText, Color messageColor)
            {
                var oldGUIColor = GUI.color;
                GUI.color = messageColor;
                SirenixEditorGUI.MessageBox(messageText, MessageType.None);
                GUI.color = oldGUIColor;
            }

            Title(TITLE_HISTORY_LIST);
            SirenixEditorGUI.BeginBox();
            {
                foreach (var message in TargetDebugger.FirstMessages) drawMessage(message);
                if (TargetDebugger.LastMessages.Count != 0)
                    drawMessageByData("...", SMDebugMessageType.None.GetColorByMessageType());
                foreach (var message in TargetDebugger.LastMessages) drawMessage(message);
            }
            SirenixEditorGUI.EndBox();

            Space();
        }

        private void DrawItemComponent(object item, string label = null)
        {
            label ??= item.GetType().Name;
            var switchInteraction = item as ISMSwitchInteraction;
            var frequencyInteraction = item as ISMFrequencyInteraction;

            if (switchInteraction != null)
                StartItemList(switchInteraction.IsEnabled);
            else StartItemList();
            {
                if (switchInteraction != null)
                    switchInteraction.IsEnabled = ItemToggle(label, switchInteraction.IsEnabled);
                else
                    ItemLabel(label);
                if (frequencyInteraction != null)
                    frequencyInteraction.Frequency = ItemFrequency(frequencyInteraction.Frequency);
            }
            StopItemList();
        }

        private void StartItemList(bool toggle)
        {
            GUI.color = toggle ? Color.green : Color.red;

            StartItemList();
        }

        private void StartItemList()
        {
            SirenixEditorGUI.BeginBox();
        }

        private void ItemLabel(string label)
        {
            GUILayout.Label(label);
        }

        private bool ItemToggle(string label, bool toggle)
        {
            toggle = EditorGUILayout.BeginToggleGroup(label, toggle);
            EditorGUILayout.EndToggleGroup();

            return toggle;
        }

        private float ItemFrequency(float frequency)
        {
            var oldGUIColor = GUI.color;
            GUI.color = Color.green;
            if (frequency > 0)
                GUI.color = Color.yellow;

            SirenixEditorGUI.BeginBox();
            {
                frequency = EditorGUILayout.Slider(nameof(ISMFrequencyInteraction.Frequency),
                    frequency, MIN_FREQUENCY, MAX_FREQUENCY);
            }
            SirenixEditorGUI.EndBox();

            GUI.color = oldGUIColor;

            return frequency;
        }

        private void StopItemList()
        {
            SirenixEditorGUI.EndBox();
            GUI.color = Color.white;
        }
    }
}
#endif