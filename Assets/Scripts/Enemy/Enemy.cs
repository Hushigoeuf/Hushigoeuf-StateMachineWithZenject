using System;
using UnityEngine;
using Zenject;

namespace Hushigoeuf
{
    [AddComponentMenu(HGEditor.COMPONENT_MENU_PATH + nameof(Enemy))]
    public class Enemy : Facade, IPoolable<EnemySpawner.Directions, float, IMemoryPool>, IDisposable
    {
        [Inject] private readonly EnemyRegistry _enemyRegistry;

        [Min(0)] [SerializeField] private int _damageCaused;

        private IMemoryPool _pool;

        public int DamageCaused => _damageCaused;

        public EnemySpawner.Directions SpawnDirection { get; private set; }
        public float Speed { get; private set; }

        public void Dispose()
        {
            _pool.Despawn(this);
        }

        public void OnSpawned(EnemySpawner.Directions spawnDirection, float speed, IMemoryPool pool)
        {
            SpawnDirection = spawnDirection;
            Speed = speed;
            _pool = pool;

            _enemyRegistry.AddEnemy(this);
        }

        public void OnDespawned()
        {
            _pool = null;

            _enemyRegistry.RemoveEnemy(this);
        }
    }
}