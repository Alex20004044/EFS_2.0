using CorD.SparrowInterfaceField;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    [Serializable]
    public class StaticGetterField<T> : IField<T>
    {
        [SerializeField]
        Mode mode = Mode.dynamic;
        [ShowIf("@" + nameof(IsDynamicMode) + "()")]
        [SerializeField]
        InterfaceField<IFieldGetter<T>> externalVariable;
        [HideIf("@" + nameof(IsDynamicMode) + "()")]
        [SerializeField]
        T staticVariable;
        public T GetValue()
        {
            if (IsDynamicMode())
                return externalVariable.i.GetValue();
            else
                return staticVariable;
        }

        public void SetValue(T value)
        {
            staticVariable = value;
            mode = Mode.@static;
        }
        public void SetFieldGetter(IFieldGetter<T> fieldGetter)
        {
            externalVariable.Set(fieldGetter);
            mode = Mode.dynamic;
        }
        bool IsDynamicMode()
        {
            return mode == Mode.dynamic;
        }
        enum Mode { dynamic, @static };
    }
}