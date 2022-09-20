using Zenject;

namespace Hushigoeuf
{
    public class PlayerFactory : PlaceholderFactory<Player>, IInitializable
    {
        public void Initialize() => Create();
    }
}