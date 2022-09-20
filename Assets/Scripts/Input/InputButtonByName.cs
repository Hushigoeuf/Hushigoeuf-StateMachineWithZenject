using UnityEngine;

namespace Hushigoeuf
{
    public class InputButtonByName : InputButton
    {
        public readonly string ButtonName;

        public InputButtonByName(string buttonName)
        {
            ButtonName = buttonName;
        }

        protected override bool GetButtonDown() => Input.GetButtonDown(ButtonName);
        protected override bool GetButton() => Input.GetButton(ButtonName);
        protected override bool GetButtonUp() => Input.GetButtonUp(ButtonName);
    }
}