using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CorD.SparrowInterfaceField.Test
{    /// <summary>
     /// Simple example NonMonoBehaivior realisation of IDescription interface
     /// </summary>
    public class DescriptorNonMonoBehaviour : IDescriptor 
    {
        string description;
        public DescriptorNonMonoBehaviour(string description)
        {
            this.description = description;
        }

        public string GetDescription()
        {
            return description;
        }
    }
}