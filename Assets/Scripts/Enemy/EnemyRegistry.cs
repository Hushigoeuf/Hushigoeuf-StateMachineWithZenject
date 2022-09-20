using System.Collections.Generic;

namespace Hushigoeuf
{
    public class EnemyRegistry
    {
        private readonly List<Enemy> _enemies = new List<Enemy>();
        public IEnumerable<Enemy> Enemies => _enemies;
        public void AddEnemy(Enemy enemy) => _enemies.Add(enemy);
        public void RemoveEnemy(Enemy enemy) => _enemies.Remove(enemy);
    }
}