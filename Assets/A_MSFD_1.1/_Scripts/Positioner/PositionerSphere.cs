using MSFD.AS;
using UnityEngine;

namespace MSFD
{
    public class PositionerSphere : PositionerBase
    {
        [Sirenix.OdinInspector.MinValue(0)]
        [SerializeField]
        float radius = 5f;
        [SerializeField]
        WorkMode workMode = WorkMode.allAxisEnabled;

        public override Pose GetValue()
        {
            Pose pose = new Pose(Rand.RandomPointInSphere(transform.position, radius), DefineRotation());
            if (workMode == WorkMode.yAxisDisabled)
                pose.position.y = 0;            
            else if (workMode == WorkMode.zAxisDisabled)
                pose.position.z = 0;
            return pose;
        }
        protected override void OnDrawGizmosSelected()
        {
            Gizmos.color = positionerColor;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
        enum WorkMode { allAxisEnabled, yAxisDisabled, zAxisDisabled};
    }
}