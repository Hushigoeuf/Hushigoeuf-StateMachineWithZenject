using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Hushigoeuf
{
    [AddComponentMenu(HGEditor.COMPONENT_MENU_PATH + nameof(JumpCounterUI))]
    [RequireComponent(typeof(TMP_Text))]
    public class JumpCounterUI : MonoBehaviour, IInitializable, IDisposable
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly PlayerJumpingState.StatData _statData;

        private TMP_Text _text;

        public void Initialize()
        {
            _text = GetComponent<TMP_Text>();

            SubscribeToPlayerJump();
        }

        public void Dispose()
        {
            UnsubscribeToPlayerJump();
        }

        private void SubscribeToPlayerJump()
        {
            _signalBus.Subscribe<PlayerJumpSignal>(OnPlayerJump);
        }

        private void UnsubscribeToPlayerJump()
        {
            _signalBus.Unsubscribe<PlayerJumpSignal>(OnPlayerJump);
        }

        private void OnPlayerJump(PlayerJumpSignal signal)
        {
            _text.SetText(_statData.OverlappedJumpCount.ToString());
        }
    }
}