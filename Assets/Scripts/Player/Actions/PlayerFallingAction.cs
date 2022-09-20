using System;
using Hushigoeuf.StateMachine;
using UnityEngine;
using Zenject;

namespace Hushigoeuf
{
    public class PlayerFallingAction : SMActionWithFrequency
    {
        [Serializable]
        public class Settings
        {
            [Min(0)] public float Speed;
        }

        [Inject] private readonly Player _player;
        [Inject] private readonly Settings _settings;

        private Vector3 _playerPosition;

        protected override void OnActionUpdateWithFrequency()
        {
            HandleFalling();
        }

        private void HandleFalling()
        {
            _playerPosition = _player.Position;
            _playerPosition.y -= _settings.Speed * Time.deltaTime;

            _player.Position = _playerPosition;
        }
    }
}