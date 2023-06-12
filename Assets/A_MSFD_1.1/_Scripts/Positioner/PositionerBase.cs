using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public abstract class PositionerBase : MonoBehaviour, IFieldGetter<Pose>
    {
        [SerializeField]
        protected PoseRotationMode poseRotationMode = PoseRotationMode.fromThisTransform;
        public abstract Pose GetValue();

        protected virtual Quaternion DefineRotation()
        {
            switch (poseRotationMode)
            {
                case PoseRotationMode.fromThisTransform:
                    return transform.rotation;
                    break;
                case PoseRotationMode.random:
                    return AS.Rand.RandomRotationYAxis();
                    break;
                case PoseRotationMode.identity:
                    return Quaternion.identity;
                    break;
                default:
                    throw new System.Exception("Unknown mode: " + poseRotationMode.ToString());
            }
        }

        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = positionerColor;
            Gizmos.DrawSphere(transform.position, 0.3f);
        }

        public enum PoseRotationMode { fromThisTransform, random, identity };

        public static readonly Color positionerColor = new Color(1f, 0.2f, 1f, 0.8f);
        public static readonly Color positionerSolidColor = new Color(1f, 0.2f, 1f, 1f);
    }
}