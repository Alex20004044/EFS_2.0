using UnityEngine;
namespace CorD.SparrowInterfaceField.Test
{
    /// <summary>
    /// Example of InterfaceField usage
    /// </summary>
    public class CodeUsage : MonoBehaviour
    {
        [SerializeField]
        InterfaceField<IDescriptor> descriptor;

        //You can add private property for auto cast
        IDescriptor Descriptor => descriptor.Value;
        //Equivalent syntax
        //IDescriptor Descriptor => descriptor.i;
        //IDescriptor Descriptor => descriptor.Get();

        private void Start()
        {
            //Shortcut for initialization with GetComponent() and GetComponentInChildren()
            if (descriptor.TryInitInterface(this))
                Debug.Log("Descriptor is initialized");

            Debug.Log(Descriptor.GetDescription());

            //You can set interface value at runtime
            IDescriptor nonMonoBehaiviourDescriptor = new DescriptorNonMonoBehaviour("NonMonoBehaiviour descriptor");
            descriptor.Set(nonMonoBehaiviourDescriptor);
            Debug.Log(descriptor.i.GetDescription());
        }

    }
}