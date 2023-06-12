using MSFD.AS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class PositionerBounds : PositionerBase
    {
        [SerializeField]
        Vector3 boundsSize = new Vector3(1, 0, 1);

        public override Pose GetValue()
        {
            return new Pose(Rand.RandomPointInBounds(transform.position, boundsSize), DefineRotation());
        }

        protected override void OnDrawGizmosSelected()
        {
            Gizmos.color = positionerColor;
            Gizmos.DrawWireCube(transform.position, boundsSize);
        }
    }
}