namespace Hushigoeuf
{
    public abstract class InputButton : IInputComponent
    {
        public enum ButtonStates
        {
            Disabled,
            ButtonDown,
            ButtonPressed,
            ButtonUp
        }

        public delegate void ButtonDownDelegate();

        public delegate void ButtonPressedDelegate();

        public delegate void ButtonUpDelegate();

        public event ButtonDownDelegate OnButtonDown;
        public event ButtonPressedDelegate OnButtonPressed;
        public event ButtonUpDelegate OnButtonUp;

        public ButtonStates CurrentState { get; private set; }

        public void InputUpdate()
        {
            CurrentState = ButtonStates.Disabled;

            if (GetButtonDown())
            {
                CurrentState = ButtonStates.ButtonDown;
                OnButtonDown?.Invoke();
            }
            else if (GetButtonUp())
            {
                CurrentState = ButtonStates.ButtonUp;
                OnButtonUp?.Invoke();
            }
            else if (GetButton())
            {
                CurrentState = ButtonStates.ButtonPressed;
                OnButtonPressed?.Invoke();
            }
        }

        protected abstract bool GetButtonDown();
        protected abstract bool GetButton();
        protected abstract bool GetButtonUp();
    }
}