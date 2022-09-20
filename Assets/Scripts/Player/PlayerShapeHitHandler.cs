using UnityEngine;

namespace Hushigoeuf
{
    [AddComponentMenu(HGEditor.COMPONENT_MENU_PATH + nameof(PlayerShapeHitHandler))]
    public class PlayerShapeHitHandler : HitHandler
    {
        protected override void OnHitEnter(GameObject hitObject)
        {
            var enemy = hitObject.GetComponent<Enemy>();
            if (enemy == null) return;

            var health = GetComponentInParent<PlayerHealth>();
            if (health == null) return;

            health.TakeDamage(enemy.DamageCaused);
        }

        protected override void OnHitExit(GameObject hitObject)
        {
        }
    }
}