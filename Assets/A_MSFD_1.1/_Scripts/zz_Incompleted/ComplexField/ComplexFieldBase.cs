#define OPTIMIZATION
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[System.Serializable]
public abstract class ComplexFieldBase<T>
{
    public T V
    {
        get
        {
            return Get();
        }

    }
    [ShowIf("$" + nameof(isShowValueInInspector))]
    [SerializeField]
    protected T value;


    [SerializeField]
    [HideInInspector]
    protected bool isShowValueInInspector = false;

    public virtual T Get()
    {
        if (value.Equals(default(T)) && typeof(T).IsClass)
        {
            Initialize();
        }
        return value;
    }
    public virtual void Set(T _value)
    {
        value = _value;
    }
    public abstract void Initialize();
}
