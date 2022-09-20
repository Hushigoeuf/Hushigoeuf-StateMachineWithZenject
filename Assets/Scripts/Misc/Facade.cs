using UnityEngine;

namespace Hushigoeuf
{
    public abstract class Facade : MonoBehaviour
    {
        private Transform _transform;

        public Transform Transform
        {
            get
            {
                _transform ??= transform;
                return _transform;
            }
        }

        public Vector3 Position
        {
            get => Transform.position;
            set => Transform.position = value;
        }
    }
}