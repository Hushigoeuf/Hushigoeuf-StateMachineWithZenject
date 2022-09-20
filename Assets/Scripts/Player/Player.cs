using UnityEngine;

namespace Hushigoeuf
{
    [AddComponentMenu(HGEditor.COMPONENT_MENU_PATH + nameof(Player))]
    public class Player : Facade
    {
        public Shape CurrentShape;

        public bool IsSelected { get; private set; }
        public float OverlapStrength01 { get; private set; }
        public bool IsOverlapped => OverlapStrength01 > 0;

        public void Select()
        {
            if (IsSelected) return;
            IsSelected = true;

            CurrentShape.SetColor(Color.cyan);
        }

        public void UnSelect()
        {
            if (!IsSelected) return;
            IsSelected = false;

            CurrentShape.SetColor(Color.white);
        }

        public void Overlap(float strength01)
        {
            if (!IsSelected) return;
            OverlapStrength01 = Mathf.Clamp01(strength01);

            if (IsOverlapped) CurrentShape.SetColor(Color.magenta);
        }

        public void UnOverlap()
        {
            if (!IsSelected) return;
            OverlapStrength01 = 0;

            CurrentShape.SetColor(Color.cyan);
        }
    }
}