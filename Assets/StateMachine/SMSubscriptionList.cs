using System.Collections;
using System.Collections.Generic;

namespace Hushigoeuf.StateMachine
{
    public abstract class SMSubscriptionList<T> : IEnumerable<T>
    {
        protected readonly List<T> Items = new List<T>();

        public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => Items.Count;

        public void Subscribe(T item)
        {
            Items.Add(item);
        }

        public void Unsubscribe(T item)
        {
            Items.Remove(item);
        }
    }
}