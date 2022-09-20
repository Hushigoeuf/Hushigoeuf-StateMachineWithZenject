using UnityEngine;

namespace Hushigoeuf
{
    [AddComponentMenu(HGEditor.COMPONENT_MENU_PATH + nameof(Shape))]
    public class Shape : Facade
    {
        public enum OverlapQualities
        {
            Strict,
            Rotate90,
            Rotate180
        }

        [SerializeField] private string _shapeID;
        [SerializeField] private OverlapQualities _overlapQuality;

        public string ShapeID => _shapeID;
        public OverlapQualities OverlapQuality => _overlapQuality;

        public void SetColor(Color color) => GetComponent<ShapeRenderer>().SetColor(color);
    }
}