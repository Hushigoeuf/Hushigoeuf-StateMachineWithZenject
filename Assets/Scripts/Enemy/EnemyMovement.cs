using System;
using UnityEngine;
using Zenject;

namespace Hushigoeuf
{
    public class EnemyMovement : ITickable
    {
        [Serializable]
        public class Settings
        {
            [Min(0)] public float MinRandomSpeed;
            [Min(0)] public float MaxRandomSpeed;
        }

        [Inject] private readonly Enemy _enemy;
        [Inject] private readonly Settings _settings;

        public void Tick()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            if (!(_enemy.Speed > 0)) return;

            float speed = _enemy.Speed * Time.deltaTime;

            Vector3 position = _enemy.Position;
            switch (_enemy.SpawnDirection)
            {
                case EnemySpawner.Directions.Left:
                    position.x += speed;
                    break;

                case EnemySpawner.Directions.Right:
                    position.x -= speed;
                    break;
            }

            _enemy.Position = position;
        }
    }
}