using System;
using UnityEngine;
using Zenject;

namespace Hushigoeuf
{
    public class PlayerRotation : IInitializable, ITickable
    {
        [Serializable]
        public class Settings
        {
            [Min(0)] public float MaxSpeedFromStart;
            [Min(0)] public float MaxOverlapSpeedFromStart;
            [Min(0)] public float SpeedDeceleration;
            [Min(0)] public float MinSpeedToStop;
        }

        [Inject] private readonly Player _player;
        [Inject] private readonly Settings _settings;
        [Inject] private readonly IRandomService _randomService;

        private float _currentSpeed;
        private float _directionMultiplier;

        public void Initialize()
        {
            SetRandomDirection();
        }

        public void Tick()
        {
            HandleRotating();
        }

        public void StartNewRotation()
        {
            SetReverseDirection();

            if (!_player.IsOverlapped)
                _currentSpeed = _settings.MaxSpeedFromStart;
            else _currentSpeed = _settings.MaxOverlapSpeedFromStart;
        }

        private void SetRandomDirection()
        {
            _directionMultiplier = _randomService.IsChance(.5f) ? -1 : 1;
        }

        private void SetReverseDirection()
        {
            _directionMultiplier *= -1;
        }

        private void HandleRotating()
        {
            if (_currentSpeed <= 0) return;

            _player.Transform.Rotate(0, 0, _currentSpeed * _directionMultiplier * Time.deltaTime);

            _currentSpeed -= _settings.SpeedDeceleration * Time.deltaTime;
            if (_currentSpeed < _settings.MinSpeedToStop)
                _currentSpeed = _settings.MinSpeedToStop;
        }
    }
}