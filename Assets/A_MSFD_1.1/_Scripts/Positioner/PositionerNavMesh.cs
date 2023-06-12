/*using MSFD.AS;
using UnityEngine;
using UnityEngine.AI;

namespace MSFD
{
    public class PositionerNavMesh : PositionerBase
    {
        [SerializeField]
        NavMeshSurface navMeshSurface;

        public override Pose GetPose()
        {
            return new Pose(Rand.RandomPointInBounds(navMeshSurface.navMeshData.sourceBounds), DefineRotation());
        }
    }
}*/