namespace Thrive.Common
{
    using System.Collections;
    using System.Collections.Generic;

    public class ThriveCollection<T> : IEnumerable<T>
    {
        protected List<T> Set = new List<T>();

        public virtual void Add(T entity)
        {
            Set.Add(entity);
        }

        public virtual void Remove(T entity)
        {
            Set.Remove(entity);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Set.GetEnumerator();
        }

        public int Count
        {
            get
            {
                return Set.Count;
            }
        }

        public T this[int index]
        {
            get
            {
                return Set[index];
            }
        }

    }
}
