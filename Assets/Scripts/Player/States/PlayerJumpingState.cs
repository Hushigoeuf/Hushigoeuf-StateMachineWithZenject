using System;
using Hushigoeuf.StateMachine;
using Zenject;

namespace Hushigoeuf
{
    public class PlayerJumpingState : SMState
    {
        [Serializable]
        public class StatData
        {
            public int JumpCount;
            public int OverlappedJumpCount;
        }

        [Inject] private readonly PlayerFallingState _fallingState;
        [Inject] private readonly PlayerFallingAction _fallingAction;
        [Inject] private readonly PlayerJumpAction _jumpAction;
        [Inject] private readonly PlayerJumpStartedDecision _startJumpingDecision;
        [Inject] private readonly PlayerJumpCompletedDecision _jumpingDecision;
        [Inject] private readonly PlayerRotation _rotation;
        [Inject] private readonly Player _player;
        [Inject] private readonly ShapeGenerator _shapeGenerator;
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly StatData _statData;

        protected override void SetActions(SMActionList actionList)
        {
            actionList.Subscribe(_fallingAction);
            actionList.Subscribe(_jumpAction);
        }

        protected override void SetTransitions(SMTransitionList transitionList)
        {
            transitionList.Subscribe(_startJumpingDecision, this);
            transitionList.Subscribe(_jumpingDecision, _fallingState);
        }

        protected override void OnStateEnter()
        {
            StartNewRotation();
            if (_player.IsOverlapped)
                GenerateNewShape();
            UpdateJumpStats();
            SendJumpSignal();
        }

        private void StartNewRotation()
        {
            _rotation.StartNewRotation();
        }

        private void GenerateNewShape()
        {
            _shapeGenerator.GenerateNewMemory();
        }

        private void SendJumpSignal()
        {
            _signalBus.Fire(new PlayerJumpSignal()
            {
                IsOverlapped = _player.IsOverlapped
            });
        }

        private void UpdateJumpStats()
        {
            _statData.JumpCount++;
            if (_player.IsOverlapped)
                _statData.OverlappedJumpCount++;
        }
    }

    public struct PlayerJumpSignal
    {
        public bool IsOverlapped;
    }
}