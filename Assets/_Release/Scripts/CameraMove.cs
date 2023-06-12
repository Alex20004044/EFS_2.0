using System;
using System.Collections;
using System.Collections.Generic;
using CorD.SparrowInterfaceField;
using MSFD.AS;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

namespace EFS
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField]
        InterfaceField<IObservable<Vector2>> inputSource;

        [SerializeField]
        float moveSpeed = 10;

        Vector2 desiredDirection;

        private void Awake()
        {
            inputSource.i.Subscribe(x => desiredDirection = x).AddTo(this);
        }

        void LateUpdate()
        {
            if (desiredDirection == Vector2.zero)
                return;
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(transform.position.x + desiredDirection.x,
                transform.position.y, 
                transform.position.z + desiredDirection.y),
                moveSpeed * Time.deltaTime);
        }
    }
}
