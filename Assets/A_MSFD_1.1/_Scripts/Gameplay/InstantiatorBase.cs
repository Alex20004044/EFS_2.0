using CorD.SparrowInterfaceField;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class InstantiatorBase : MonoBehaviour, IActivatable, IFieldSetter<GameObject>
    {
        [SerializeField]
        StaticGetterField<GameObject> prefab;
        [SerializeField]
        InterfaceField<IFieldGetter<Pose>> pose;
        [SerializeField]
        bool isSetParentToThis = true;
        [Button]
        public virtual void Activate()
        {
            InstantiateCore.Spawn(prefab.GetValue(), pose.i.GetValue(), isSetParentToThis ? transform : null);
        }

        public void SetValue(GameObject value)
        {
            prefab.SetValue(value);
        }
    }
}