using Zenject;

namespace Hushigoeuf
{
    public class EnemyDeathHandler : ITickable
    {
        [Inject] private readonly Enemy _enemy;
        [Inject] private readonly GameInstaller.MarkerSettings _markerSettings;

        public void Tick()
        {
            HandleDeath();
        }

        private void HandleDeath()
        {
            switch (_enemy.SpawnDirection)
            {
                case EnemySpawner.Directions.Left:
                    if (_enemy.Position.x > _markerSettings.EnemyRightSpawnMarker.MarkerPosition.x) Death();
                    break;

                case EnemySpawner.Directions.Right:
                    if (_enemy.Position.x < _markerSettings.EnemyLeftSpawnMarker.MarkerPosition.x) Death();
                    break;
            }
        }

        private void Death() => _enemy.Dispose();
    }
}