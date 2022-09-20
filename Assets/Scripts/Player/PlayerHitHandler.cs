using UnityEngine;

namespace Hushigoeuf
{
    [AddComponentMenu(HGEditor.COMPONENT_MENU_PATH + nameof(PlayerHitHandler))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerHitHandler : HitHandler<PlayerHitMemory>
    {
    }
}