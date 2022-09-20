using System;
using System.Linq;
using Zenject;

namespace Hushigoeuf
{
    public class EnemyDespawnWhenJumped : IInitializable, IDisposable
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly EnemyRegistry _enemyRegistry;

        private bool _jumpingPrepared;

        public void Initialize()
        {
            SubscribeToPlayerJumped();
        }

        public void Dispose()
        {
            UnsubscribeToPlayerJumped();
        }

        private void SubscribeToPlayerJumped()
        {
            _signalBus.Subscribe<PlayerJumpSignal>(OnPlayerJumped);
        }

        private void UnsubscribeToPlayerJumped()
        {
            _signalBus.Unsubscribe<PlayerJumpSignal>(OnPlayerJumped);
        }

        private void OnPlayerJumped(PlayerJumpSignal signal)
        {
            if (!_jumpingPrepared)
            {
                _jumpingPrepared = signal.IsOverlapped;
                return;
            }

            if (!signal.IsOverlapped)
            {
                _jumpingPrepared = false;
                return;
            }

            foreach (Enemy enemy in _enemyRegistry.Enemies.ToArray()) enemy.Dispose();
        }
    }
}