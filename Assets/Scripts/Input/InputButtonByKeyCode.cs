using UnityEngine;

namespace Hushigoeuf
{
    public class InputButtonByKeyCode : InputButton
    {
        public readonly KeyCode TargetKeyCode;
        public readonly KeyCode TargetAltKeyCode;

        public InputButtonByKeyCode(KeyCode targetKeyCode, KeyCode targetAltKeyCode = KeyCode.None)
        {
            TargetKeyCode = targetKeyCode;
            TargetAltKeyCode = targetAltKeyCode;
        }

        protected override bool GetButtonDown() =>
            Input.GetKeyDown(TargetKeyCode) || Input.GetKeyDown(TargetAltKeyCode);

        protected override bool GetButton() =>
            Input.GetKey(TargetKeyCode) || Input.GetKey(TargetAltKeyCode);

        protected override bool GetButtonUp() =>
            Input.GetKeyUp(TargetKeyCode) || Input.GetKeyUp(TargetAltKeyCode);
    }
}