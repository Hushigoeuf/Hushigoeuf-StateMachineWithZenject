using UnityEngine;

namespace Hushigoeuf
{
    [AddComponentMenu(HGEditor.COMPONENT_MENU_PATH + nameof(WorldMarker))]
    public class WorldMarker : MonoBehaviour
    {
        public Color MarkerColor = Color.white;

        private Transform _markerPoint;

        public Transform MarkerPoint
        {
            get
            {
                _markerPoint ??= transform;
                return _markerPoint;
            }
        }

        public Vector3 MarkerPosition => MarkerPoint?.position ?? Vector3.zero;
    }
}