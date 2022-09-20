using UnityEngine;
using Zenject;

namespace Hushigoeuf
{
    [AddComponentMenu(HGEditor.COMPONENT_MENU_PATH + nameof(BootstrapInstaller))]
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindScreenService();
            BindRandomService();
        }

        private void BindScreenService()
        {
            Container.Bind<IScreenService>().To<UnityScreenService>().AsSingle();
        }

        private void BindRandomService()
        {
            Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
        }
    }
}