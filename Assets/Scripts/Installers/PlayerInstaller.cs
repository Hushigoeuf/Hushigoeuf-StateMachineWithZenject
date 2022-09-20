using UnityEngine;
using Zenject;

namespace Hushigoeuf
{
    [AddComponentMenu(HGEditor.COMPONENT_MENU_PATH + nameof(PlayerInstaller))]
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindOtherSystems();
            BindStateMachine();
            BindStates();
            BindActions();
            BindDecisions();
        }

        private void BindOtherSystems()
        {
            Container.BindInterfacesAndSelfTo<PlayerShapeFactory>().AsSingle();
            Container.Bind<PlayerHitMemory>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerRotation>().AsSingle();
            Container.BindInterfacesTo<PlayerShapeOverlapHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerDeathHandler>().AsSingle();
        }

        private void BindStateMachine()
        {
            Container.BindInterfacesAndSelfTo<PlayerStateMachine>().AsSingle();
        }

        private void BindStates()
        {
            Container.Bind<PlayerStartingState>().AsSingle();
            Container.Bind<PlayerFallingState>().AsSingle();
            Container.Bind<PlayerJumpingState>().AsSingle();
        }

        private void BindActions()
        {
            Container.Bind<PlayerFallingAction>().AsSingle();
            Container.Bind<PlayerJumpAction>().AsSingle();
        }

        private void BindDecisions()
        {
            Container.Bind<PlayerJumpStartedDecision>().AsSingle();
            Container.Bind<PlayerJumpCompletedDecision>().AsSingle();
        }
    }
}