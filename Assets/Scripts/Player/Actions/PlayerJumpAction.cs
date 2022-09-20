using System;
using Hushigoeuf.StateMachine;
using UnityEngine;
using Zenject;

namespace Hushigoeuf
{
    public class PlayerJumpAction : SMActionWithFrequency
    {
        [Serializable]
        public class Settings
        {
            [Min(0)] public float StartSpeed;
            [Min(0)] public float StartOverlapSpeed;
            [Min(0)] public float SpeedDeceleration;
        }

        private readonly Player _player;
        private readonly Transform _transform;
        private readonly Settings _settings;
        private readonly PlayerRotation _rotation;

        private Vector3 _playerPosition;

        public float CurrentSpeed { get; private set; }

        [Inject]
        public PlayerJumpAction(Player player,
            Settings settings,
            PlayerRotation rotation)
        {
            _player = player;
            _transform = player.transform;
            _settings = settings;
            _rotation = rotation;
        }

        protected override void OnActionEnter()
        {
            StartNewJump();
        }

        protected override void OnActionUpdateWithFrequency()
        {
            HandleJumping();
        }

        private void StartNewJump()
        {
            if (!_player.IsOverlapped)
                CurrentSpeed = _settings.StartSpeed;
            else CurrentSpeed = _settings.StartOverlapSpeed;
        }

        private void HandleJumping()
        {
            if (CurrentSpeed <= 0) return;

            _playerPosition = _transform.position;
            _playerPosition.y += CurrentSpeed * Time.deltaTime;
            _transform.position = _playerPosition;

            CurrentSpeed -= _settings.SpeedDeceleration * Time.deltaTime;
        }
    }
}