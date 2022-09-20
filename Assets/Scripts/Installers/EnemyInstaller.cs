using Zenject;

namespace Hushigoeuf
{
    public class EnemyInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EnemyMovement>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyRotation>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyDeathHandler>().AsSingle();
        }
    }
}