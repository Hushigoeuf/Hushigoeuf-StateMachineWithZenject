using System;
using UnityEngine;
using Zenject;

namespace Hushigoeuf
{
    public class InputManager : IInitializable, ILateTickable
    {
        [Serializable]
        public class Settings
        {
            public KeyCode JumpKey = KeyCode.Mouse0;
            public KeyCode JumpAltKey = KeyCode.Space;
        }

        private readonly InputMemory _inputMemory;
        private readonly Settings _settings;

        [Inject]
        public InputManager(InputMemory inputMemory, Settings settings)
        {
            _inputMemory = inputMemory;
            _settings = settings;
        }

        public void Initialize()
        {
            InitializeInputButtons();
        }

        public void LateTick()
        {
            UpdateInputButtons();
        }

        private void InitializeInputButtons()
        {
            _inputMemory.JumpButton = new InputButtonByKeyCode(_settings.JumpKey, _settings.JumpAltKey);
        }

        private void UpdateInputButtons()
        {
            _inputMemory.JumpButton.InputUpdate();
        }
    }
}