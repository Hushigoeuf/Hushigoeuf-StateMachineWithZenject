using System;
using UnityEngine;
using Zenject;

namespace Hushigoeuf
{
    public class EnemySpawner : ITickable
    {
        [Serializable]
        public class Settings
        {
            [Min(1)] public float DelayBeforeStartSpawns;
            [Min(1)] public float MinDelayBetweenSpawns;
        }

        public enum Directions
        {
            Left,
            Right
        }

        [Inject] private readonly EnemyFactory _factory;
        [Inject] private readonly Settings _settings;
        [Inject] private readonly EnemyMovement.Settings _movementSettings;
        [Inject] private readonly IRandomService _randomService;
        [Inject] private readonly GameInstaller.MarkerSettings _markerSettings;

        private float _lastSpawnTime;

        public void Tick()
        {
            if (Time.realtimeSinceStartup < _settings.DelayBeforeStartSpawns) return;

            TrySpawnNewEnemy();
        }

        private void TrySpawnNewEnemy()
        {
            if (Time.realtimeSinceStartup - _lastSpawnTime > _settings.MinDelayBetweenSpawns) SpawnNewEnemy();
        }

        private Enemy SpawnNewEnemy()
        {
            Enemy enemy = _factory.Create(GetRandomDirection(), GetRandomSpeed());
            enemy.Position = GetRandomPosition(enemy.Position, enemy.SpawnDirection);

            _lastSpawnTime = Time.realtimeSinceStartup;

            return enemy;
        }

        private Directions GetRandomDirection() => _randomService.IsChance(.5f) ? Directions.Left : Directions.Right;

        private float GetRandomSpeed() =>
            _randomService.Range(_movementSettings.MinRandomSpeed, _movementSettings.MaxRandomSpeed);

        private Vector3 GetRandomPosition(Vector3 origin, Directions direction)
        {
            switch (direction)
            {
                case Directions.Left:
                    origin = GetRandomPositionByMarker(origin, _markerSettings.EnemyLeftSpawnMarker);
                    break;

                case Directions.Right:
                    origin = GetRandomPositionByMarker(origin, _markerSettings.EnemyRightSpawnMarker);
                    break;
            }

            return origin;
        }

        private Vector3 GetRandomPositionByMarker(Vector3 origin, ScreenMarker marker)
        {
            float heightFrom = marker.MarkerPosition.y - marker.Size.y / 2f;

            origin.x = marker.MarkerPosition.x;
            origin.y = _randomService.Range(heightFrom, heightFrom + marker.Size.y);

            return origin;
        }
    }
}