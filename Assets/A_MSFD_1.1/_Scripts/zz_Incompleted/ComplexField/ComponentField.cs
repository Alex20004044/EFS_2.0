using UnityEngine;
using Sirenix.OdinInspector;
namespace MSFD
{
    [System.Serializable]
    public class ComponentField<T> : ComplexFieldBase<T> where T : Component
    {
        [OnValueChanged("@" + nameof(OnGetComponentMode) + "()")]
        [SerializeField]
        GetComponentMode getComponentMode = GetComponentMode.manual;

        [ShowIf("@" + nameof(IsShowSourceGo) + "()")]
        [SerializeField]
        GameObject sourceGo;
        [ShowIf("@" + nameof(IsShowSourceGoName) + "()")]
        [SerializeField]
        string sourceGoName;

        public override void Initialize()
        {

            switch (getComponentMode)
            {
                case GetComponentMode.manual:
                    break;
                case GetComponentMode.findInScene:
                    value = GameObject.Find(sourceGoName).GetComponent<T>();
                    break;
                case GetComponentMode.getComponent:
                    value = sourceGo.GetComponent<T>();
                    break;
                case GetComponentMode.getComponentInChildren:
                    value = sourceGo.GetComponentInChildren<T>();
                    break;
                case GetComponentMode.getComponentInParent:
                    value = sourceGo.GetComponentInParent<T>();
                    break;
                default:
                    break;
            }
        }

        void OnGetComponentMode()
        {
            isShowValueInInspector = false;
            switch (getComponentMode)
            {
                case GetComponentMode.manual:
                    isShowValueInInspector = true;
                    break;
                case GetComponentMode.findInScene:
                    break;
                case GetComponentMode.getComponent:
                    break;
                case GetComponentMode.getComponentInChildren:
                    break;
                case GetComponentMode.getComponentInParent:
                    break;
                default:

                    break;
            }
        }
        bool IsShowSourceGoName()
        {
            if (getComponentMode == GetComponentMode.findInScene)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool IsShowSourceGo()
        {
            if (getComponentMode == GetComponentMode.getComponent ||
                getComponentMode == GetComponentMode.getComponentInChildren ||
                getComponentMode == GetComponentMode.getComponentInParent)
            {
                return true;
            }
            else
                return false;
        }

        enum GetComponentMode { manual, findInScene, getComponent, getComponentInChildren, getComponentInParent };
    }
}