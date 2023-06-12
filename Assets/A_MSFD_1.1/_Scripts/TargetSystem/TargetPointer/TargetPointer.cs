using System;
using UnityEngine;
using UniRx;
using Sirenix.OdinInspector;
using CorD.SparrowInterfaceField;

namespace MSFD
{
    public class TargetPointer : MonoBehaviour, IObserver<Transform>
    {
        [SerializeField]
        InterfaceField<IObservable<Transform>> transformSource;

        [Header("Snap must be in left corner! Place this script on arrow parent or somewhere else")]
        [SerializeField]
        GameObject pointerGO;

        [SerializeField]
        bool isShowPointerWhenTargetInScreen = true;

        [HideIf("$" + nameof(isShowPointerWhenTargetInScreen))]
        [SerializeField]
        [Header("ArrowImage is an object which should be disabled when target in screen")]
        GameObject pointerImage;

        [SerializeField]
        float displacementFromEdge = 0f;

        [SerializeField]
        PointerAnimationType pointerAnimationType = PointerAnimationType.none;
        [ShowIf("@pointerAnimationType == PointerAnimationType.moveTowards")]
        [SerializeField]
        float animationMoveSpeed = 700f;

        Transform target;

        float angle;
        new Camera camera;

        Vector3 targetInScreen;
        RectTransform rectTransform;
        Vector3 desiredPosition = Vector3.zero;

        IDisposable disposable;
        private void Awake()
        {
            camera = Camera.main;
            rectTransform = pointerGO.GetComponentInChildren<RectTransform>();

            disposable = transformSource.i.Subscribe(this).AddTo(this);
        }

        private void LateUpdate()
        {
            if (target == null)
            {
                if (pointerImage.activeSelf)
                {
                    pointerImage.SetActive(false);
                }
                return;
            }
            else
            {
                DirectArrow();
                MoveToDesiredPosition();
            }
        }

        void DirectArrow()
        {
            targetInScreen = camera.WorldToScreenPoint(target.position);
            targetInScreen.z = 0;
            if (!IsNeedDisplayPointer())
            {
                if (pointerImage.activeSelf)
                    pointerImage.SetActive(false);       
            }
            else
            {
                if (!pointerImage.activeSelf)
                {
                    pointerImage.SetActive(true);
                    rectTransform.position = targetInScreen;
                }
                Vector3 direction = (target.position - camera.transform.position).normalized;
                angle = AS.Coordinates.AngleFromDirection(new Vector2(direction.x, direction.z));
                rectTransform.localEulerAngles = new Vector3(0, 0, angle);

                float xDisp = displacementFromEdge + rectTransform.sizeDelta.x / 2f;
                targetInScreen.x = Mathf.Clamp(targetInScreen.x, xDisp,
                    Screen.width - xDisp);
                float yDisp = displacementFromEdge + rectTransform.sizeDelta.y / 2f;
                targetInScreen.y = Mathf.Clamp(targetInScreen.y, yDisp,
                    Screen.height - yDisp);

                desiredPosition = targetInScreen;              
            }
        }
        bool IsNeedDisplayPointer()
        {
            return target != null && (isShowPointerWhenTargetInScreen || !IsTargetInScreen());
        }
        bool IsTargetInScreen()
        {
            return ((targetInScreen.x > 0 && targetInScreen.x < Screen.width) &&
            (targetInScreen.y > 0 && targetInScreen.y < Screen.height));
        }

        void MoveToDesiredPosition()
        {
            if (pointerAnimationType == PointerAnimationType.none)
            {
                rectTransform.position = desiredPosition;
            }
            else
            {
                rectTransform.position = Vector3.MoveTowards(rectTransform.position, desiredPosition, animationMoveSpeed * UnityEngine.Time.deltaTime);
            }
        }
        public void SetTarget(Transform _target)
        {
            target = _target;
        }

        public void OnCompleted()
        {
            disposable.Dispose();
        }

        public void OnError(Exception error)
        {
            throw error;
        }

        public void OnNext(Transform value)
        {
            SetTarget(value);
        }
        enum PointerAnimationType { none, moveTowards};
    }
}