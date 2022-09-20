using UnityEngine;
using Zenject;

namespace Hushigoeuf
{
    [AddComponentMenu(HGEditor.COMPONENT_MENU_PATH + nameof(PlayerHealth))]
    public class PlayerHealth : MonoBehaviour
    {
        [Inject] private readonly PlayerDeathHandler _deathHandler;

        [Min(0)] [SerializeField] private int _initialHealth;
        [Min(0)] [SerializeField] private int _maxHealth;

        public int InitialHealth => _initialHealth;
        public int MaxHealth => _maxHealth;

        public int CurrentHealth { get; private set; }

        private void Awake()
        {
            CurrentHealth = InitialHealth;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
            if (CurrentHealth <= 0) _deathHandler.Death();
        }
    }
}