using Zenject;

namespace Hushigoeuf
{
    public class EnemyPool : MonoPoolableMemoryPool<EnemySpawner.Directions, float, IMemoryPool, Enemy>
    {
    }
}