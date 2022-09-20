using UnityEngine;

namespace Hushigoeuf
{
    [AddComponentMenu(HGEditor.COMPONENT_MENU_PATH + nameof(JumpTarget))]
    public class JumpTarget : Facade
    {
        public Shape CurrentShape;
    }
}