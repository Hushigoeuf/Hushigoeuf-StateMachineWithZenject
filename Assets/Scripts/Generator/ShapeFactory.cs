using System;
using Zenject;

namespace Hushigoeuf
{
    public abstract class ShapeFactory : IInitializable, IDisposable
    {
        [Serializable]
        public class Settings
        {
            public ShapePrefab[] Prefabs = Array.Empty<ShapePrefab>();
        }

        [Serializable]
        public class ShapePrefab
        {
            public Shape PlayerShape;
            public Shape JumpTargetShape;
        }

        [Inject] protected readonly DiContainer _diContainer;
        [Inject] protected readonly Settings _settings;
        [Inject] protected readonly SignalBus _signalBus;

        protected Shape[] _instances;

        public Shape CurrentInstance { get; protected set; }

        public void Initialize()
        {
            CreateInstances();

            _signalBus.Subscribe<GenerateNewMemorySignal>(OnGenerationMemoryChanged);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GenerateNewMemorySignal>(OnGenerationMemoryChanged);
        }

        private void CreateInstances()
        {
            _instances = new Shape[_settings.Prefabs.Length];
            for (var i = 0; i < _instances.Length; i++)
                _instances[i] = CreateInstance(_settings.Prefabs[i]);
        }

        private Shape CreateInstance(ShapePrefab prefab)
        {
            var instance = _diContainer.InstantiatePrefabForComponent<Shape>(GetTargetPrefab(prefab));
            instance.gameObject.SetActive(false);
            return instance;
        }

        protected abstract Shape GetTargetPrefab(ShapePrefab prefab);

        public void Spawn(string targetShapeID)
        {
            CurrentInstance?.gameObject.SetActive(false);

            for (var i = 0; i < _instances.Length; i++)
                if (_instances[i].ShapeID == targetShapeID)
                {
                    CurrentInstance = _instances[i];
                    break;
                }

            CurrentInstance?.gameObject.SetActive(true);

            OnSpawn();
        }

        protected virtual void OnSpawn()
        {
        }

        private void OnGenerationMemoryChanged(GenerateNewMemorySignal signal)
        {
            Spawn(signal.ShapeID);
        }
    }

    public class PlayerShapeFactory : ShapeFactory
    {
        [Inject] private readonly Player _player;

        protected override Shape GetTargetPrefab(ShapePrefab prefab) => prefab.PlayerShape;

        protected override void OnSpawn()
        {
            _player.CurrentShape = CurrentInstance;
        }
    }

    public class JumpTargetShapeFactory : ShapeFactory
    {
        [Inject] private readonly JumpTarget _jumpTarget;

        protected override Shape GetTargetPrefab(ShapePrefab prefab) => prefab.JumpTargetShape;

        protected override void OnSpawn()
        {
            _jumpTarget.CurrentShape = CurrentInstance;
        }
    }
}