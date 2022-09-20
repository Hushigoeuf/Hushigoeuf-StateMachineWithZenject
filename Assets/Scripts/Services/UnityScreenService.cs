using UnityEngine;

namespace Hushigoeuf
{
    public class UnityScreenService : IScreenService
    {
        public float ScreenWidth => Screen.width;
        public float ScreenHeight => Screen.height;
    }
}