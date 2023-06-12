using Sirenix.OdinInspector;
using System;
using UniRx;
using UnityEngine;
namespace MSFD
{
    public class HPBase : MonoBehaviour, IHP, IDeltaRange<float>
    {
        [SerializeField]
        protected DeltaRangeE hpDeltaRange = new DeltaRangeE();

        [SerializeField]
        protected bool isReviveOnEnable = true;

        //[FoldoutGroup(EditorConstants.debugGroup)]
        [SerializeField]
        protected bool isLogError = true;
        [ReadOnly]
        [ShowInInspector]
        protected bool isDestroyed;

        public float Value => ((IModField<float>)hpDeltaRange).Value;

        public float BaseValue { get => ((IModField<float>)hpDeltaRange).BaseValue; set => ((IModField<float>)hpDeltaRange).BaseValue = value; }

        protected virtual void Awake()
        {
            hpDeltaRange.GetObsOnMinBorder().Subscribe((x) => Destruct()).AddTo(this);
            hpDeltaRange.GetObsOnIncrease().Subscribe((x) =>
            {
                if(x > hpDeltaRange.MinBorder)
                    isDestroyed = false;
            }).AddTo(this);
        }
        protected virtual void OnEnable()
        {
            if (isReviveOnEnable)
            {
                hpDeltaRange.Fill();
            }
        }
        public IDeltaRange<float> GetHP()
        {
            return hpDeltaRange;
        }
        /// <summary>
        /// This method defines GameObject Destroy process
        /// </summary>
        protected virtual void Destruct()
        {
            if (isDestroyed)
            {
                AS.Utilities.LogError("Attempt to destroy already destroyed object: " + name, isLogError);
                return;
            }
            isDestroyed = true;
            InstantiateCore.Despawn(gameObject);
        }

        public void Empty()
        {
            ((IDeltaRange<float>)hpDeltaRange).Empty();
        }

        public void Fill()
        {
            ((IDeltaRange<float>)hpDeltaRange).Fill();
        }

        public bool IsEmpty()
        {
            return ((IDeltaRange<float>)hpDeltaRange).IsEmpty();
        }

        public bool IsFull()
        {
            return ((IDeltaRange<float>)hpDeltaRange).IsFull();
        }

        public IModField<float> GetMinBorderModField()
        {
            return ((IDeltaRange<float>)hpDeltaRange).GetMinBorderModField();
        }

        public IModField<float> GetMaxBorderModField()
        {
            return ((IDeltaRange<float>)hpDeltaRange).GetMaxBorderModField();
        }

        public IObservable<float> GetObsOnMinBorder()
        {
            return ((IDeltaRange<float>)hpDeltaRange).GetObsOnMinBorder();
        }

        public IObservable<float> GetObsOnMaxBorder()
        {
            return ((IDeltaRange<float>)hpDeltaRange).GetObsOnMaxBorder();
        }

        public IObservable<float> GetObsOnRangeReached()
        {
            return ((IDeltaRange<float>)hpDeltaRange).GetObsOnRangeReached();
        }

        public float Increase(float value)
        {
            return ((IDelta<float>)hpDeltaRange).Increase(value);
        }

        public float Decrease(float value)
        {
            return ((IDelta<float>)hpDeltaRange).Decrease(value);
        }

        public IModProcessor<float> GetIncreaseModProcessor()
        {
            return ((IDelta<float>)hpDeltaRange).GetIncreaseModProcessor();
        }

        public IModProcessor<float> GetDecreaseModProcessor()
        {
            return ((IDelta<float>)hpDeltaRange).GetDecreaseModProcessor();
        }

        public IDisposable AddChangeMod(Func<float, float> modifier, int priority = 0)
        {
            return ((IDelta<float>)hpDeltaRange).AddChangeMod(modifier, priority);
        }

        public IObservable<float> GetObsOnIncrease()
        {
            return ((IDelta<float>)hpDeltaRange).GetObsOnIncrease();
        }

        public IObservable<float> GetObsOnDecrease()
        {
            return ((IDelta<float>)hpDeltaRange).GetObsOnDecrease();
        }

        public IObservable<float> GetObsOnChange()
        {
            return ((IDelta<float>)hpDeltaRange).GetObsOnChange();
        }

        public float GetValue()
        {
            return ((IFieldGetter<float>)hpDeltaRange).GetValue();
        }

        public void SetValue(float value)
        {
            ((IFieldSetter<float>)hpDeltaRange).SetValue(value);
        }

        public float CalculateWithMods(float sourceValue)
        {
            return ((IModProcessor<float>)hpDeltaRange).CalculateWithMods(sourceValue);
        }

        public IDisposable AddMod(Func<float, float> modifier, int priority = 0)
        {
            return ((IModifiable<float>)hpDeltaRange).AddMod(modifier, priority);
        }

        public IObservable<Unit> GetObsOnModsUpdated()
        {
            return ((IModifiable<float>)hpDeltaRange).GetObsOnModsUpdated();
        }

        public void RemoveAllMods()
        {
            ((IModifiable<float>)hpDeltaRange).RemoveAllMods();
        }

        public IDisposable Subscribe(IObserver<float> observer)
        {
            return ((IObservable<float>)hpDeltaRange).Subscribe(observer);
        }

        public IDisposable Subscribe(IObserver<IDeltaRange<float>> observer)
        {
            return ((IObservable<IDeltaRange<float>>)hpDeltaRange).Subscribe(observer);
        }

        public float MaxBorder => ((IDeltaRange<float>)hpDeltaRange).MaxBorder;

        public float MinBorder => ((IDeltaRange<float>)hpDeltaRange).MinBorder;

        public void RemoveAllModsFromAllModifiables()
        {
            ((IDelta<float>)hpDeltaRange).RemoveAllModsFromAllModifiables();
        }

        public float GetFillPercent()
        {
            return ((IDeltaRange<float>)hpDeltaRange).GetFillPercent();
        }

        public void RaiseModsUpdatedEvent()
        {
            ((IModifiable<float>)hpDeltaRange).RaiseModsUpdatedEvent();
        }
    }
}