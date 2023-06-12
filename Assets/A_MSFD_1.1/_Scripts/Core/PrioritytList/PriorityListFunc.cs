using Sirenix.OdinInspector;
using System;
#if UNITY_EDITOR
using System.Reflection;
#endif
namespace MSFD
{
    [System.Serializable]
    public class PriorityListFunc<T> : PriorityList<Func<T,T>>, ICalculator<T>
    {
        [FoldoutGroup(EditorConstants.debugGroup)]
        [Button]
        public T Calculate(T sourceValue)
        {        
            foreach (var x in this)
                sourceValue = x.Invoke(sourceValue);
            return sourceValue;
        }
#if UNITY_EDITOR
        [FoldoutGroup(EditorConstants.debugGroup)]
        [Obsolete(EditorConstants.editorOnly)]
        [Button]
        void ShowInstalledModifiers()
        {
            string log = "PriorityListFunc<" + typeof(T) + ">\n";
            log += "Priority    |   MethodName\n";
            foreach (var x in GetPriorityGroupsEnumerator())
            {
                log += $"{x.Key,7}       ";
                foreach (var y in x.Value)
                {
                    log += y.GetMethodInfo().Name + ", ";
                }
                log += "\n";
            }
            UnityEngine.Debug.Log(log);
        }
#endif
    }
}