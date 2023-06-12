using UnityEngine;
using Sirenix.OdinInspector;
using UniRx;
using System;
using CorD.SparrowInterfaceField;

namespace MSFD
{
    public class CameraController : MonoBehaviour, IObserver<Transform[]>
    {
        [SerializeField]
        InterfaceField<IObservable<Transform[]>> transformsSource;

        [SerializeField]
        float dampTime = 0.3f;                 // Approximate time for the camera to refocus.
        [SerializeField]
        float screenEdgeBuffer = 4f;           // Space between the top/bottom most target and the screen edge.
        [SerializeField]
        float minHeight = 40f;                  // The smallest orthographic size the camera can be.
        [SerializeField]
        float maxHeight = 60f;

        [SerializeField]
        bool isNeedToClampTargets = false;

        [Space]
        [SerializeField]
        float heightMultiplyer = 2f;
        [SerializeField]
        float clampTargetBuffer = 4f;

        bool isCameraActivated = false;

        [HorizontalGroup("XClamp")]
        [SerializeField]
        float minXCoord = -50f;
        [HorizontalGroup("XClamp")]
        [SerializeField]
        float maxXCoord = 50f;

        [HorizontalGroup("ZClamp")]
        [SerializeField]
        float minZCoord = -50f;
        [HorizontalGroup("ZClamp")]
        [SerializeField]
        float maxZCoord = 50f;

        float xLength;
        float yLength;

        float xCoord;
        float zCoord;

        Transform[] targets;                   // All the targets the camera needs to encompass.

        float distanceFromCenter;
        float height;


        Camera _camera;
        Vector3 moveVelocity;
        Vector3 desiredPosition;
        Vector3 averagePos;

        void Awake()
        {
            _camera = GetComponentInChildren<Camera>();
        }
        void Start()
        {
            transformsSource.i.Subscribe(this).AddTo(this);
        }
        private void FixedUpdate()
        {
            if (isCameraActivated)
            {
                FindAveragePosition();
                Zoom();
                LimitPosition();

                if (isNeedToClampTargets)
                {
                    ClampTargetPos();
                }
                Move();
            }
        }
        private void Move()
        {
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref moveVelocity, dampTime);
        }

        void Zoom()
        {
            distanceFromCenter = 0;
            float length;

            foreach (Transform target in targets)
            {
                if (!target.gameObject.activeSelf)
                    continue;

                length = Vector3.Distance(target.position, averagePos);

                if (length >= distanceFromCenter)
                {
                    distanceFromCenter = length;
                }
            }
            // ATTENTION!!!      
            height = distanceFromCenter * heightMultiplyer + screenEdgeBuffer;
            height = Mathf.Clamp(height, minHeight, maxHeight);
            desiredPosition = new Vector3(desiredPosition.x, height, desiredPosition.z);
        }

        public void SetTargetsAndActivate(Transform[] _targets)
        {
            targets = _targets;
            isCameraActivated = true;
        }

        private void FindAveragePosition()
        {
            averagePos = new Vector3();
            int numTargets = 0;

            for (int i = 0; i < targets.Length; i++)
            {
                if (!targets[i].gameObject.activeSelf)
                {
                    continue;
                }
                averagePos += targets[i].position;
                numTargets++;
            }

            if (numTargets > 0)
            {
                averagePos /= numTargets;

            }
            else
            {
                averagePos = transform.position;
            }
            averagePos.y = 0;

            desiredPosition = averagePos;
        }

        void ClampTargetPos()
        {
            foreach (Transform target in targets)
            {
                if (!target.gameObject.activeSelf)
                {
                    continue;
                }

                Vector3 vector00 = _camera.ScreenToWorldPoint(new Vector3(0, 0, transform.position.y));
                Vector3 vector11 = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.y));


                Vector3 pos = target.position;

                pos.x = Mathf.Clamp(pos.x, vector00.x + clampTargetBuffer, vector11.x - clampTargetBuffer);
                pos.z = Mathf.Clamp(pos.z, vector00.z + clampTargetBuffer, vector11.z - clampTargetBuffer);

                target.position = pos;
            }
        }

        public void SetStartPositionAndSize()
        {
            FindAveragePosition();
            transform.position = desiredPosition;
        }
        void LimitPosition()
        {
            yLength = transform.position.y * Mathf.Tan(Mathf.Deg2Rad * _camera.fieldOfView / 2);
            xLength = yLength * _camera.aspect;
            xCoord = Mathf.Clamp(desiredPosition.x, minXCoord + xLength, maxXCoord - xLength);
            zCoord = Mathf.Clamp(desiredPosition.z, minZCoord + yLength, maxZCoord - yLength);
            desiredPosition = new Vector3(xCoord, desiredPosition.y, zCoord);
        }

        public void OnCompleted()
        {
            
        }

        public void OnError(Exception error)
        {
            throw error;
        }

        public void OnNext(Transform[] value)
        {
            SetTargetsAndActivate(value);
        }
    }
}