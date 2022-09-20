using System;
using UnityEngine;
using Zenject;

namespace Hushigoeuf
{
    public class EnemyRotation : ITickable
    {
        [Serializable]
        public class Settings
        {
            [Min(0)] public float Speed;
        }

        [Inject] private readonly Enemy _enemy;
        [Inject] private readonly Settings _settings;

        public void Tick()
        {
            HandleRotation();
        }

        private void HandleRotation()
        {
            float speed = _settings.Speed * Time.deltaTime;
            if (_enemy.SpawnDirection == EnemySpawner.Directions.Left) speed *= -1;
            _enemy.Transform.Rotate(0, 0, speed);
        }
    }
}