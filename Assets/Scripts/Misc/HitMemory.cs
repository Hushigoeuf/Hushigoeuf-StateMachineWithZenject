using System.Collections.Generic;
using UnityEngine;

namespace Hushigoeuf
{
    public class HitMemory
    {
        public delegate void HitSubscribedDelegate(GameObject hitObject);

        public delegate void HitUnsubscribedDelegate(GameObject hitObject);

        public event HitSubscribedDelegate OnHitSubscribed;
        public event HitUnsubscribedDelegate OnHitUnsubscribed;

        private readonly List<GameObject> _hitObjects = new List<GameObject>();

        public GameObject[] HitObjects => _hitObjects.ToArray();

        public void Subscribe(GameObject hitObject)
        {
            if (_hitObjects.Contains(hitObject))
            {
                HGEditor.LogWarning("Hit-object '" + hitObject.name + "' does exists.");
                return;
            }

            _hitObjects.Add(hitObject);
            OnHitSubscribed?.Invoke(hitObject);
        }

        public void Unsubscribe(GameObject hitObject)
        {
            if (!_hitObjects.Contains(hitObject))
            {
                HGEditor.LogWarning("Hit-object '" + hitObject.name + "' not exists.");
                return;
            }

            _hitObjects.Remove(hitObject);
            OnHitUnsubscribed?.Invoke(hitObject);
        }
    }
}