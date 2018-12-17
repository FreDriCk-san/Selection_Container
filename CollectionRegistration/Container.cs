using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace CollectionRegistration
{
    public class SelectionContainer
    {
        private ConcurrentDictionary<Guid, IEnumerable> container;

        public int Count => container.Count;

        public SelectionContainer()
        {
            container = new ConcurrentDictionary<Guid, IEnumerable>();
        }

        public Guid Register(IEnumerable selection)
        {
            if (null == selection)
            {
                throw new NullReferenceException();
            }

            var id = Guid.NewGuid();

            if (null != container.FirstOrDefault(c => c.Key == id).Key)
            {
                throw new Exception("The current id is already existing!");
            }

            container.GetOrAdd(id, selection);
            return id;
        }


        public IEnumerable<T> Take<T>(Guid key, int take, int skip = 0)
        {
            IEnumerable toTake;

            if (!container.TryGetValue(key, out toTake))
            {
                throw new Exception("Collection wasn't found!");
            }

            return toTake.Cast<T>().Skip(skip).Take(take);
        }


        public void Remove(Guid key)
        {
            IEnumerable removedItem;

            if (!container.TryRemove(key, out removedItem))
            {
                throw new Exception("Can't remove item!");
            }
        }

        public void Clear()
        {
            container.Clear();
        }

    }
}
