using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HS
{
    [System.Serializable]
    public class TypeCountTable<T>: IEnumerable<TypeCountPair<T>>
    {
        [TableList(ShowIndexLabels = true)]
        [SerializeField]
        List<TypeCountPair<T>> table = new List<TypeCountPair<T>>();

        public IEnumerator<TypeCountPair<T>> GetEnumerator()
        {
            return table.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
