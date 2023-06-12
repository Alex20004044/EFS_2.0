using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MSFD.Data
{
    public static class DataSystemUtilities
    {

        public static string PathCombine(params string[] pathParts)
        {
            return string.Join("/", pathParts);
        }
        public static string PathCombineIgnoreEmptyParts(params string[] pathParts)
        {
            return string.Join("/", pathParts.Where(x=>!x.IsNullOrWhitespace()));
        }
        public static void ThrowInvalidCastExeption(string path, Type realType, Type requestedType)
        {
            throw new InvalidCastException(FormInvalidCastExeptionMessage(path, realType, requestedType));
        }
        static string FormInvalidCastExeptionMessage(string path, Type realType, Type requestedType)
        {
            return $"Data with path: \"{path}\" has type {realType.Name} but you try to cast it to {requestedType.Name}";
        }

    }
}
