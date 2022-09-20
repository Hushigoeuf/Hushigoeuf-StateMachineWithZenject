using System;
using UnityEngine;

namespace Hushigoeuf
{
    [AddComponentMenu(HGEditor.COMPONENT_MENU_PATH + nameof(ShapeRenderer))]
    public class ShapeRenderer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] _renderers = Array.Empty<SpriteRenderer>();

        public void SetColor(Color color)
        {
            for (var i = 0; i < _renderers.Length; i++)
            {
                color.a = _renderers[i].color.a;
                _renderers[i].color = color;
            }
        }
    }
}