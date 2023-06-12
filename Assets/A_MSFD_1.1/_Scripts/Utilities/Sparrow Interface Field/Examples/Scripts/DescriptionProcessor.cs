using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CorD.SparrowInterfaceField.Test
{
    /// <summary>
    /// Basic usage example
    /// </summary>
    public class DescriptionProcessor : MonoBehaviour
    {
        /// <summary>
        /// InterfaceField gives ability to use Drag and Drop in Inspector
        /// </summary>
        [SerializeField]
        InterfaceField<IDescriptor> firstDescription;
        /// <summary>
        /// Also it is possible to use InterfaceFieldAttribute with Unity.Object to achieve same effect
        /// </summary>
        [InterfaceField(typeof(IDescriptor))]
        [SerializeField]
        Object secondDescription;
        /// <summary>
        /// //You can add private property for auto cast Unity.Object to target interface
        /// </summary>
        IDescriptor SecondDescription => secondDescription as IDescriptor;

        private void Start()
        {
            if (firstDescription.i != null)
                Debug.Log(gameObject.name + " First Description: " + firstDescription.i.GetDescription());
            if (SecondDescription != null)
                Debug.Log(gameObject.name + "Second Description: " + SecondDescription.GetDescription());

        }

    }
}