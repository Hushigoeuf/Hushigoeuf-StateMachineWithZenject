using UnityEngine;
using Zenject;

namespace Hushigoeuf
{
    [AddComponentMenu(HGEditor.COMPONENT_MENU_PATH + nameof(JumpTargetInstaller))]
    public class JumpTargetInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<JumpTargetShapeFactory>().AsSingle();
        }
    }
}