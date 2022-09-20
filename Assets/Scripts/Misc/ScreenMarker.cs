using UnityEngine;
using Zenject;

namespace Hushigoeuf
{
    [AddComponentMenu(HGEditor.COMPONENT_MENU_PATH + nameof(ScreenMarker))]
    public class ScreenMarker : WorldMarker, IInitializable
    {
        public enum Directions
        {
            Center,
            Left,
            Right,
            Top,
            Bottom
        }

        [Inject] private readonly Camera _camera;
        [Inject] private readonly IScreenService _screenService;

        [SerializeField] private Directions _direction = Directions.Center;
        [SerializeField] private float _offset;

        public Directions Direction => _direction;
        public float Offset => _offset;

        public Vector2 Size
        {
            get
            {
                Vector2 size = ScreenUnitSize;
                if (_direction == Directions.Left || _direction == Directions.Right) size.x = 0;
                else if (_direction == Directions.Top || _direction == Directions.Bottom) size.y = 0;
                return size;
            }
        }

        private Vector2 ScreenUnitSize =>
            _camera.ScreenToWorldPoint(new Vector3(
                _screenService.ScreenWidth, _screenService.ScreenHeight));

        public void Initialize()
        {
            UpdateMarkerPosition();
        }

        private void UpdateMarkerPosition()
        {
            MarkerPoint.position = GetPositionAtScreen();
        }

        private Vector3 GetPositionAtScreen()
        {
            Vector3 position = _camera.transform.position;
            position.z = MarkerPosition.z;
            Vector2 screenSize = ScreenUnitSize;

            switch (_direction)
            {
                case Directions.Left:
                    position.x -= screenSize.x - _offset;
                    break;

                case Directions.Right:
                    position.x += screenSize.x + _offset;
                    break;

                case Directions.Top:
                    position.y += screenSize.y + _offset;
                    break;

                case Directions.Bottom:
                    position.y -= screenSize.y - _offset;
                    break;
            }

            return position;
        }
    }
}