using MSFD.AS;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MSFD
{
    [System.Serializable]
    public class WeightedRandomList<T>
    {
        [TableList(ShowIndexLabels = true)]
        [SerializeField]
        List<ItemWeight<T>> probabilities = new List<ItemWeight<T>>();

        public void AddItem(T item, int chanceWeight = 1)
        {
            probabilities.Add(new ItemWeight<T>(item, chanceWeight));
        }
        public void RemoveAllItems(T item)
        {
            probabilities.RemoveAll(x => x.Equals(item));
        }
        public T GetItem()
        {
            return probabilities[probabilities.Select(x => x.weight).GetRandomWeightedIndex()].item;
        }
#if UNITY_EDITOR
        [Button]
        float[] _CalculateProbabilities()
        {
            int totalWeight = probabilities.Sum((x) => x.weight);
            return probabilities.Select((x) => (float)x.weight / totalWeight).ToArray();
        }
#endif

        [System.Serializable]
        struct ItemWeight<T>
        {
            public T item;
            [Min(0)]
            public int weight;

            public ItemWeight(T item, int chanceWeight)
            {
                this.weight = chanceWeight;
                this.item = item;
            }
        }
    }
}
