using Sirenix.OdinInspector;
using UnityEngine;

namespace MSFD
{
    public class Destroyer : MonoBehaviour, IActivatable
    {
        [SerializeField]
        float despawnTime = 0;
        [Button]
        public void Activate()
        {
            InstantiateCore.Despawn(gameObject, despawnTime);
        }
    }
}