using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class PositionerPoint : PositionerBase
    {
        public override Pose GetValue()
        {
            return new Pose(transform.position, DefineRotation());
        }
    }
}