using System.Collections.Generic;
using UnityEngine;

namespace CorD.SparrowInterfaceField.Test
{
    /// <summary>
    /// Inspector view example demo
    /// </summary>
    public class InspectorViewExample : MonoBehaviour
    {
        [SerializeField]
        InterfaceField<System.IObservable<float>> someObservable;
        [Header("You can use a class instead of an interface")]
        [SerializeField]
        InterfaceField<DescriptorComponent> descriptorComponent;
        [Header("List of Objects with InterfaceFiedlAttribute")]
        [SerializeField, InterfaceField(typeof(IDescriptor))]
        List<Object> objects;
        [Header("Interface field in container")]
        [SerializeField]
        InterfaceFieldInContainerExample containerExample;

        [Header("Incorrect type of interface example")]
        [SerializeField]
        InterfaceField<Object> ifObject;

        private void Start()
        {
            //Debug.Log((objects[0] as IDescriptor).GetDescription());
        }
    }

    [System.Serializable]
    class InterfaceFieldInContainerExample
    {
        [SerializeField]
        List<InterfaceField<IDescriptor>> interfaceFields;
    }

}