using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CorD.SparrowInterfaceField.Test
{
    /// <summary>
    /// Simple example ScriptableObject realisation of IDescription interface
    /// </summary>
    [System.Serializable]
    [CreateAssetMenu(fileName = "DescriptorScriptableObject", menuName = "Sparrow Interface Field Example/Descriptor Scriptable Object")]
    public class DescriptorScriptableObject : ScriptableObject, IDescriptor
    {
        [TextArea]
        [SerializeField]
        string description = "Description from DescriptorScriptableObject";
        public string GetDescription()
        {
            return description;
        }
    }
}