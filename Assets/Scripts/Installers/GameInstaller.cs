using System;
using UnityEngine;
using Zenject;

namespace Hushigoeuf
{
    [AddComponentMenu(HGEditor.COMPONENT_MENU_PATH + nameof(GameInstaller))]
    public class GameInstaller : MonoInstaller
    {
        [Serializable]
        public class MarkerSettings
        {
            public WorldMarker PlayerFallingDeathMarker;
            public ScreenMarker EnemyLeftSpawnMarker;
            public ScreenMarker EnemyRightSpawnMarker;
        }

        [Inject] private readonly GameSettingsInstaller.PlayerSettings _playerSettings;
        [Inject] private readonly GameSettingsInstaller.EnemySettings _enemySettings;

        [SerializeField] private MarkerSettings _markerSettings;

        public override void InstallBindings()
        {
            BindSettings();
            BindStatInstances();
            BindSignals();
            BindCoreInstances();
            BindGenerator();
            BindPlayer();
            BindEnemy();
        }

        private void BindSettings()
        {
            Container.BindInstance(_markerSettings).AsSingle();

            Container.BindInterfacesTo<ScreenMarker>().FromInstance(_markerSettings.PlayerFallingDeathMarker);
            Container.BindInterfacesTo<ScreenMarker>().FromInstance(_markerSettings.EnemyLeftSpawnMarker);
            Container.BindInterfacesTo<ScreenMarker>().FromInstance(_markerSettings.EnemyRightSpawnMarker);
        }

        private void BindStatInstances()
        {
            Container.Bind<PlayerJumpingState.StatData>().AsSingle();
        }

        private void BindSignals()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<PlayerJumpSignal>();
            Container.DeclareSignal<GenerateNewMemorySignal>();
        }

        private void BindCoreInstances()
        {
            Container.BindInstance(Camera.main).AsSingle();
            Container.BindInstance(new SceneLoader(gameObject.scene.buildIndex)).AsSingle();
        }

        private void BindPlayer()
        {
            BindPlayerInput();
            BindPlayerPrefab();
            BindJumpTarget();
        }

        private void BindPlayerInput()
        {
            Container.Bind<InputMemory>().AsSingle();
            Container.BindInterfacesTo<InputManager>().AsSingle();
        }

        private void BindPlayerPrefab()
        {
            var instance = Container.InstantiatePrefabForComponent<Player>(_playerSettings.PlayerPrefab);
            Container.BindInstance(instance).AsSingle();
        }

        private void BindJumpTarget()
        {
            var instance = Container.InstantiatePrefabForComponent<JumpTarget>(_playerSettings.JumpTargetPrefab);
            Container.BindInstance(instance).AsSingle();
        }

        private void BindGenerator()
        {
            Container.Bind<ShapeMemory>().AsSingle();
            Container.Bind<ShapeRandom>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShapeGenerator>().AsSingle();
        }

        private void BindEnemy()
        {
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            Container.Bind<EnemyRegistry>().AsSingle();
            Container.BindInterfacesTo<EnemyDespawnWhenJumped>().AsSingle();

            Container.BindFactory<EnemySpawner.Directions, float, Enemy, EnemyFactory>()
                .FromPoolableMemoryPool<EnemySpawner.Directions, float, Enemy, EnemyPool>(pool => pool
                    .WithInitialSize(4)
                    .FromSubContainerResolve()
                    .ByNewPrefabInstaller<EnemyInstaller>(_enemySettings.EnemyPrefab)
                    .UnderTransformGroup("Enemies"));
        }
    }
}