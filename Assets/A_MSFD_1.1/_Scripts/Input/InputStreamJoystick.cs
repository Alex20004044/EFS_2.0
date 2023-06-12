using Sirenix.OdinInspector;
using System;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public class InputStreamJoystick : MonoBehaviour, IObservable<Vector2>, IObservable<Vector3>
    {
        [SerializeField]
        string vertAxisName = "Vertical";
        [SerializeField]
        string horAxisName = "Horizontal";
        [SerializeField]
        bool isNormalizeInput = true;
        [SerializeField]
        AS.Coordinates.ConvertV2ToV3Mode convertV2ToV3Mode = AS.Coordinates.ConvertV2ToV3Mode.y_to_z;

        float vert;
        float hor;

        [ReadOnly]
        [ShowInInspector]
        ReactiveProperty<Vector2> input = new ReactiveProperty<Vector2>();
        Subject<Vector3> vector3Subject = new Subject<Vector3>();

        void Awake()
        {
            input.Subscribe((x) => vector3Subject.OnNext(AS.Coordinates.ConvertVector2ToVector3(x, 0, convertV2ToV3Mode)));
        }
        private void Update()
        {
            vert = CnControls.CnInputManager.GetAxis(vertAxisName);
            hor = CnControls.CnInputManager.GetAxis(horAxisName);

            Vector2 rawInput = new Vector3(hor, vert);
            if (isNormalizeInput)
                rawInput.Normalize();
            input.Value = rawInput;
        }
        public IDisposable Subscribe(IObserver<Vector2> observer)
        {
            return input.Subscribe(observer);
        }

        public IDisposable Subscribe(IObserver<Vector3> observer)
        {
            vector3Subject.OnNext(AS.Coordinates.ConvertVector2ToVector3(input.Value, 0, convertV2ToV3Mode));
            return vector3Subject.Subscribe(observer);
        }
    }
}