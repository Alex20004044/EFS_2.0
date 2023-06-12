using CorD.SparrowInterfaceField;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD.AS;
namespace MSFD
{
    public class PoseCombiner : MonoBehaviour, IFieldGetter<Pose>
    {
        [SerializeField]
        InterfaceField<IFieldGetter<Pose>>[] positioners;

        public Pose GetValue()
        {
            return positioners.GetRandomElement().i.GetValue();
        }
    }
}