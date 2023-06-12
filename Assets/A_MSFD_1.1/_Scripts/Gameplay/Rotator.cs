using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    [RequireComponent(typeof(Rigidbody))]
    public class Rotator : MonoBehaviour
    {
        [SerializeField]
        Vector3 axis = Vector3.up;
        [SuffixLabel("Deg/s")]
        [SerializeField]
        float rotationSpeed = 90;

        Rigidbody rb;
        Quaternion rotation;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            CalculateRotationQuaternion();
        }
        [Button]
        void CalculateRotationQuaternion()
        {
            rotation = Quaternion.AngleAxis(rotationSpeed * UnityEngine.Time.fixedDeltaTime, axis);
        }
        private void FixedUpdate()
        {
            rb.MoveRotation(rb.rotation * rotation);
        }
        public void SetAxis(Vector3 rotateDir)
        {
            axis = rotateDir;
        }
        public void SetRotationSpeed(float speed)
        {
            rotationSpeed = speed;
        }
    }
}