using System;
using UnityEngine;
using Zenject;

namespace Hushigoeuf
{
    [CreateAssetMenu(menuName = HGEditor.CREATE_ASSET_MENU_PATH + nameof(GameSettingsInstaller))]
    public class GameSettingsInstaller : ScriptableObjectInstaller
    {
        [Serializable]
        public class PlayerSettings
        {
            public Player PlayerPrefab;
            public JumpTarget JumpTargetPrefab;
            public PlayerFallingAction.Settings FallingSettings;
            public PlayerJumpAction.Settings JumpingSetting;
            public PlayerRotation.Settings RotationSettings;
            public PlayerShapeOverlapHandler.Settings ShapeOverlapSettings;
        }

        [Serializable]
        public class ShapeSettings
        {
            public ShapeFactory.Settings FactorySettings;
        }

        [Serializable]
        public class EnemySettings
        {
            public Enemy EnemyPrefab;
            public EnemySpawner.Settings SpawnSettings;
            public EnemyMovement.Settings MovementSettings;
            public EnemyRotation.Settings RotationSettings;
        }

        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private InputManager.Settings _inputSettings;
        [SerializeField] private ShapeSettings _shapeSettings;
        [SerializeField] private EnemySettings _enemySettings;

        public override void InstallBindings()
        {
            BindPlayerSettings();
            BindInputSettings();
            BindShapeSettings();
            BindEnemySettings();
        }

        private void BindPlayerSettings()
        {
            Container.BindInstance(_playerSettings);
            Container.BindInstance(_playerSettings.FallingSettings);
            Container.BindInstance(_playerSettings.JumpingSetting);
            Container.BindInstance(_playerSettings.RotationSettings);
            Container.BindInstance(_playerSettings.ShapeOverlapSettings);
        }

        private void BindInputSettings()
        {
            Container.BindInstance(_inputSettings);
        }

        private void BindShapeSettings()
        {
            Container.BindInstance(_shapeSettings);
            Container.BindInstance(_shapeSettings.FactorySettings);
        }

        private void BindEnemySettings()
        {
            Container.BindInstance(_enemySettings);
            Container.BindInstance(_enemySettings.SpawnSettings);
            Container.BindInstance(_enemySettings.MovementSettings);
            Container.BindInstance(_enemySettings.RotationSettings);
        }
    }
}