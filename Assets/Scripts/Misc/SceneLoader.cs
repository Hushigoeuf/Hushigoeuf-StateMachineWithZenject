using UnityEngine.SceneManagement;

namespace Hushigoeuf
{
    public class SceneLoader
    {
        private readonly int _currentBuildIndex;

        public SceneLoader(int currentBuildIndex)
        {
            _currentBuildIndex = currentBuildIndex;
        }

        public void Restart() => SceneManager.LoadScene(_currentBuildIndex);
    }
}