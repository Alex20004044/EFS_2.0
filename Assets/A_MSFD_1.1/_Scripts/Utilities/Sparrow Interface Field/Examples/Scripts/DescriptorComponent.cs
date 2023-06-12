using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CorD.SparrowInterfaceField.Test
{
    /// <summary>
    /// Simple example MonoBehaivior realisation of IDescription interface
    /// </summary>
    public class DescriptorComponent : MonoBehaviour, IDescriptor
    {
        [TextArea]
        [SerializeField]
        string description = "Description from DescriptorComponent";
        public string GetDescription()
        {
            return description;
        }
    }
}