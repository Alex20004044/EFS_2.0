using CorD.SparrowInterfaceField;
using UnityEngine;

namespace MSFD
{
    /// <summary>
    /// Single damage on targetEnter
    /// </summary>
    public class DamageInZoneOnTargetEnter : MonoBehaviour
    {
        [SerializeField]
        InterfaceField<IZoneObserver> zoneObserver;

        [SerializeField]
        float momentalDamage = 1;

        private void Awake()
        {
            zoneObserver.i.onTriggerEnterTransform += OnTargetEnter;
        }
        private void OnDestroy()
        {
            zoneObserver.i.onTriggerEnterTransform -= OnTargetEnter;
        }
        private void OnTargetEnter(Transform target)
        {
            target.GetComponent<IHP>().GetHP().Decrease(momentalDamage);
        }
    }
}