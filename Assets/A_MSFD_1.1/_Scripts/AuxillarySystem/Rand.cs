using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
namespace MSFD.AS
{
    public static class Rand
    {
        public static int GetRandomWeightedIndex(this IEnumerable<int> weights)
        {
            if (weights.Min() < 0) throw new ArgumentException("All weights must be non negative");

            int randomPoint = weights.Sum().GetRandomIndex();
            int currentPoint = 0;
            int index = -1;
            do
            {
                index++;
                currentPoint += weights.ElementAt(index);        
            } while (currentPoint <= randomPoint);
            return index;
        }


        #region Different

        /// <summary>
        /// Return -1 or 1
        /// </summary>
        /// <returns></returns>
        public static int RandomSign()
        {
            int[] signs = { -1, 1 };
            return signs.GetRandomElement();
        }
        #endregion

        #region Collections
        /// <summary>
        /// Return collection of different elements in [0, elementsCount) with Length = randomIndexesCount.
        /// Different means that elements can't match each other</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements"></param>
        /// <param name="randomIndexesCount"></param>
        /// <returns></returns>
        public static T[] GetRandomDifferentElements<T>(this IEnumerable<T> elements, int randomIndexesCount)
        {
            int[] t;
            return GetRandomDifferentElements(elements, randomIndexesCount, out t);
        }
        /// <summary>
        /// Return collection of different elements in [0, elementsCount) with Length = randomIndexesCount.
        /// Different means that elements can't match each other</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements"></param>
        /// <param name="randomIndexesCount"></param>
        /// <param name="randomIndexes"></param>
        /// <returns></returns>
        public static T[] GetRandomDifferentElements<T>(this IEnumerable<T> elements, int randomIndexesCount, out int[] randomIndexes)
        {
            T[] elementsArray = elements.ToArray();
            T[] randomElements = new T[randomIndexesCount];
            randomIndexes = GetRandomDifferentIndexes(elementsArray.Length, randomIndexesCount);
            for (int i = 0; i < randomIndexesCount; i++)
            {
                randomElements[i] = elementsArray[randomIndexes[i]];
            }
            return randomElements;
        }


        /// <summary>
        /// Return collection of unrelated indexes in [0, elementsCount) with Length = randomIndexesCount.
        /// Unrelated means that indexes can match each other</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements"></param>
        /// <param name="randomIndexesCount"></param>
        /// <returns></returns>
        public static T[] GetRandomUnrelatedElements<T>(this IEnumerable<T> elements, int randomIndexesCount)
        {
            int[] t;
            return GetRandomUnrelatedElements(elements, randomIndexesCount, out t);
        }
        /// <summary>
        /// Return collection of unrelated indexes in [0, elementsCount) with Length = randomIndexesCount.
        /// Unrelated means that indexes can match each other
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements"></param>
        /// <param name="randomIndexesCount"></param>
        /// <param name="randomIndexes"></param>
        /// <returns></returns>
        public static T[] GetRandomUnrelatedElements<T>(this IEnumerable<T> elements, int randomIndexesCount, out int[] randomIndexes)
        {
            T[] elementsArray = elements.ToArray();
            T[] randomElements = new T[randomIndexesCount];
            randomIndexes = GetRandomUnrelatedIndexes(elementsArray.Length, randomIndexesCount);
            for (int i = 0; i < randomIndexesCount; i++)
            {
                randomElements[i] = elementsArray[randomIndexes[i]];
            }
            return randomElements;
        }
        /// <summary>
        /// Return collection of different indexes in [0, elementsCount) with Length = randomIndexesCount.
        /// Different means that indexes can't match each other
        /// </summary>
        /// <param name="elementsCount"></param>
        /// <param name="randomIndexesCount"></param>
        /// <returns></returns>

        public static int[] GetRandomDifferentIndexes(int elementsCount, int randomIndexesCount)
        {
            if (randomIndexesCount <= 0)
                throw new System.IndexOutOfRangeException("Attempt to get 0 or less then 0 randomElements");
            if (randomIndexesCount > elementsCount)
                throw new System.IndexOutOfRangeException("Attempt to get incorrect randomIndexesCount. RandomIndexesCount must be less then sourceElementsCount");

            int[] randomIndexes = new int[randomIndexesCount];

            List<int> availableIndexes = new List<int>(elementsCount);
            for (int i = 0; i < elementsCount; i++)
                availableIndexes.Add(i);

            for (int i = 0; i < randomIndexesCount; i++)
            {
                int ind = availableIndexes.Count.GetRandomIndex();
                randomIndexes[i] = availableIndexes[ind];
                availableIndexes.RemoveAt(ind);
            }
            return randomIndexes;
        }
        /// <summary>
        /// Return collection of unrelated indexes in [0, elementsCount) with Length = randomIndexesCount.
        /// Unrelated means that indexes can match each other
        /// </summary>
        /// <param name="elementsCount"></param>
        /// <param name="randomIndexesCount"></param>
        /// <returns></returns>
        public static int[] GetRandomUnrelatedIndexes(int elementsCount, int randomIndexesCount)
        {
            if (randomIndexesCount <= 0) { throw new System.IndexOutOfRangeException("Attempt to get 0 or less then 0 randomElements"); }
            int[] randomIndexes = new int[randomIndexesCount];
            for (int i = 0; i < randomIndexesCount; i++)
            {
                randomIndexes[i] = GetRandomIndex(elementsCount);
            }
            return randomIndexes;
        }

        public static T GetRandomElement<T>(this IEnumerable<T> elements)
        {
            return elements.ElementAt(elements.GetRandomIndex());
        }
        /// <summary>
        /// Return random index in [0:elementsCount)
        /// </summary>
        /// <param name="elementsCount"></param>
        /// <returns></returns>
        public static int GetRandomIndex<T>(this IEnumerable<T> elements)
        {
            int count = elements.Count();
            if (count < 1)
                throw new System.ArgumentOutOfRangeException("Incorrect elements count");
            return Random.Range(0, count);
        }
        /// <summary>
        /// Return random index in [0:elementsCount)
        /// </summary>
        /// <param name="elementsCount"></param>
        /// <returns></returns>
        public static int GetRandomIndex(this int elementsCount)
        {
            if (elementsCount < 1)
                throw new System.ArgumentOutOfRangeException("Incorrect elements count");
            return Random.Range(0, elementsCount);
        }

        #endregion

        #region Range
        /// <summary>
        /// range.x - inclusive, range.y - exclusive
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static int RandomPointInRange(this Vector2Int range)
        {
            return Random.Range(range.x, range.y);
        }
        /// <summary>
        /// range.x - inclusive, range.y - inclusive
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static float RandomPointInRange(this Vector2 range)
        {
            return Random.Range(range.x, range.y);
        }
        #endregion

        #region Direction/Point
        public static Vector3 RandomPointInBounds(Bounds bounds)
        {
            return RandomPointInBounds(bounds.center, bounds.size);
        }
        public static Vector3 RandomPointInBounds(Vector3 center, Vector3 size)
        {
            Vector3 randomPoint = Vector3.zero;

            randomPoint.x = UnityEngine.Random.Range(-size.x / 2, size.x / 2);
            randomPoint.y = UnityEngine.Random.Range(-size.y / 2, size.y / 2);
            randomPoint.z = UnityEngine.Random.Range(-size.z / 2, size.z / 2);
            return randomPoint + center;
        }
        public static Vector3 RandomPointInSphere(Vector3 center, float radius)
        {
            return center + UnityEngine.Random.insideUnitSphere * radius;
        }
        public static Quaternion RandomRotationXAxis()
        {
            return Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), Vector3.right);
        }
        public static Quaternion RandomRotationYAxis()
        {
            return Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), Vector3.up);
        }
        public static Quaternion RandomRotationZAxis()
        {
            return Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), Vector3.forward);
        }
        #endregion

    }
}