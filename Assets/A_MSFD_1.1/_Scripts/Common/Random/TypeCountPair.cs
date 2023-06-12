using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HS
{
    [System.Serializable]
    public struct TypeCountPair<T>
    {
        public T type;
        public int count;

        public TypeCountPair(T type, int count)
        {
            this.type = type;
            this.count = count;
        }
    }
}
