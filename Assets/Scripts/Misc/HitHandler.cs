using UnityEngine;
using Zenject;

namespace Hushigoeuf
{
    public abstract class HitHandler : MonoBehaviour
    {
        [SerializeField] protected LayerMask _targetLayerMask = Physics2D.AllLayers;

        protected bool LayerInLayerMask(int layer) => ((1 << layer) & _targetLayerMask) != 0;

        protected virtual void OnTriggerEnter2D(Collider2D collider) =>
            HitEnter(collider.gameObject);

        protected virtual void OnTriggerExit2D(Collider2D collider) =>
            HitExit(collider.gameObject);

        protected virtual void OnCollisionEnter2D(Collision2D collision) =>
            HitEnter(collision.gameObject);

        protected virtual void OnCollisionExit2D(Collision2D collision) =>
            HitExit(collision.gameObject);

        protected virtual void HitEnter(GameObject hitObject)
        {
            if (!LayerInLayerMask(hitObject.layer)) return;
            OnHitEnter(hitObject);
        }

        protected virtual void HitExit(GameObject hitObject)
        {
            if (!LayerInLayerMask(hitObject.layer)) return;
            OnHitExit(hitObject);
        }

        protected abstract void OnHitEnter(GameObject hitObject);
        protected abstract void OnHitExit(GameObject hitObject);
    }

    public abstract class HitHandler<THitMemory> : HitHandler where THitMemory : HitMemory
    {
        [Inject] protected readonly THitMemory _hitMemory;

        protected override void OnHitEnter(GameObject hitObject) => _hitMemory.Subscribe(hitObject);
        protected override void OnHitExit(GameObject hitObject) => _hitMemory.Unsubscribe(hitObject);
    }
}