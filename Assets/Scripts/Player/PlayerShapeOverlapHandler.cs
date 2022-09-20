using System;
using UnityEngine;
using Zenject;

namespace Hushigoeuf
{
    public class PlayerShapeOverlapHandler : IInitializable, IDisposable, ITickable
    {
        [Serializable]
        public class Settings
        {
            [Min(0)] public float MaxDistance;
            [Min(0)] public float MaxAngle;
        }

        [Inject] private readonly Player _player;
        [Inject] private readonly PlayerHitMemory _hitMemory;
        [Inject] private readonly Settings _settings;

        private JumpTarget _jumpTarget;

        public void Initialize()
        {
            SubscribeToOverlapState();
        }

        public void Dispose()
        {
            UnsubscribeToOverlapState();
        }

        public void Tick()
        {
            HandleOverlapShapes();
        }

        private void HandleOverlapShapes()
        {
            if (_jumpTarget == null) return;

            if (CompareDistance() && CompareQuaternions())
                _player.Overlap(1);
            else
                _player.UnOverlap();
        }

        private bool CompareDistance()
        {
            if (!(_settings.MaxDistance > 0)) return true;

            float distance = Vector2.Distance(_player.Position, _jumpTarget.Position);
            return distance <= _settings.MaxDistance;
        }

        private bool CompareQuaternions()
        {
            if (!(_settings.MaxAngle > 0)) return true;

            float angle = Quaternion.Angle(_player.Transform.rotation, _jumpTarget.Transform.rotation);
            float halfMaxAngle = _settings.MaxAngle / 2f;

            switch (_player.CurrentShape.OverlapQuality)
            {
                case Shape.OverlapQualities.Strict:
                    return angle <= halfMaxAngle;

                case Shape.OverlapQualities.Rotate90:
                    return angle <= halfMaxAngle ||
                           !(angle - 90 - 10 > _settings.MaxAngle) ||
                           angle >= 180 - halfMaxAngle;

                case Shape.OverlapQualities.Rotate180:
                    return angle <= halfMaxAngle || angle >= 180 - halfMaxAngle;
            }

            return true;
        }

        private void SubscribeToOverlapState()
        {
            _hitMemory.OnHitSubscribed += OnHitSubscribed;
            _hitMemory.OnHitUnsubscribed += OnHitUnsubscribed;
        }

        private void UnsubscribeToOverlapState()
        {
            _hitMemory.OnHitSubscribed -= OnHitSubscribed;
            _hitMemory.OnHitUnsubscribed -= OnHitUnsubscribed;
        }

        private void OnHitSubscribed(GameObject collider)
        {
            if (_jumpTarget != null) return;

            var jumpTargetFacade = collider.GetComponent<JumpTarget>();
            if (jumpTargetFacade == null) return;

            _jumpTarget = jumpTargetFacade;

            OnJumpTargetSubscribed();
        }

        private void OnHitUnsubscribed(GameObject collider)
        {
            if (_jumpTarget == null) return;

            var jumpTargetFacade = collider.GetComponent<JumpTarget>();
            if (jumpTargetFacade == null) return;
            if (_jumpTarget != jumpTargetFacade) return;

            OnJumpTargetUnsubscribed();

            _jumpTarget = null;
        }

        private void OnJumpTargetSubscribed()
        {
            _player.Select();
        }

        private void OnJumpTargetUnsubscribed()
        {
            _player.UnSelect();
        }
    }
}