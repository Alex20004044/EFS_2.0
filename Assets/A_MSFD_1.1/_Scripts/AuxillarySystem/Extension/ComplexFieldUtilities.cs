using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.AS
{
    public static class ComplexFieldUtilities
    {
        public static float Map(IDeltaRange<float> value, Vector2 outputRange)
        {
            return Calculation.Map(value.Value, value.MinBorder, value.MaxBorder, outputRange.x, outputRange.y);
        }

        public static float RandomPoint(this IDeltaRange<float> range)
        {
            return RandomPointInRange(range);
        }
        public static float RandomPointInRange(IDeltaRange<float> range)
        {
            return Random.Range(range.MinBorder, range.MaxBorder);
        }
        public static int RandomPoint(this IDeltaRange<int> range)
        {
            return RandomPointInRange(range);
        }
        public static int RandomPointInRange(IDeltaRange<int> range)
        {
            return Random.Range(range.MinBorder, range.MaxBorder);
        }
    }
}