using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;

namespace MSFD
{
    [System.Serializable]
    public class PriorityList<T>: IPriorityList<T>
    {   
        SortedDictionary<int, List<T>> itemsDictionary = new SortedDictionary<int, List<T>>();
        CompositeDisposable disposables = new CompositeDisposable();
        Subject<Unit> onItemsUpdated = new Subject<Unit>();

        /// <summary>
        /// Lower priority values will be processed before higher values.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="priority"></param>
        public IDisposable Add(T item, int priority = 0)
        {
            if (!this.itemsDictionary.TryGetValue(priority, out List<T> mods))
            {
                mods = new List<T>();
                this.itemsDictionary.Add(priority, mods);
            }
            mods.Add(item);
            RaiseItemsUpdatedEvent();

            IDisposable disposable = Disposable.Create(() => {
                mods.Remove(item);
                if (mods.Count == 0) //is it necessary?
                    itemsDictionary.Remove(priority);
                RaiseItemsUpdatedEvent();
            });
            disposables.Add(disposable);
            return disposable;
        }
        public IObservable<Unit> GetObsOnItemsUpdated()
        {
            return onItemsUpdated.ThrottleFrame(0, FrameCountType.EndOfFrame);
        }
        public void RaiseItemsUpdatedEvent()
        {
            onItemsUpdated.OnNext(Unit.Default);
        }

        [FoldoutGroup(EditorConstants.debugGroup)]
        [Button]
        public void Clear()
        {
            disposables.Clear();
            onItemsUpdated.OnNext(Unit.Default);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var x in itemsDictionary)
                foreach (var t in x.Value)
                    yield return t;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        /// <summary>
        /// Key is priority, Value is list of items
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<int, IEnumerable<T>>> GetPriorityGroupsEnumerator()
        {               
            foreach (var x in itemsDictionary)
                yield return new KeyValuePair<int, IEnumerable<T>>(x.Key, x.Value);
        }
    }
}