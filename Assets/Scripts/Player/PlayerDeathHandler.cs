using Zenject;

namespace Hushigoeuf
{
    public class PlayerDeathHandler : ITickable
    {
        [Inject] private readonly Player _player;
        [Inject] private readonly GameInstaller.MarkerSettings _markerSettings;
        [Inject] private readonly SceneLoader _sceneLoader;

        public void Tick()
        {
            HandleDeathFromLongFalling();
        }

        private void HandleDeathFromLongFalling()
        {
            if (_markerSettings.PlayerFallingDeathMarker == null) return;

            if (_player.Position.y < _markerSettings.PlayerFallingDeathMarker.MarkerPosition.y) Death();
        }

        public void Death() => _sceneLoader.Restart();
    }
}