using CorD.SparrowInterfaceField;
using System.Collections;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public class WindZone : MonoBehaviour
    {
        [SerializeField]
        Vector3 windDirection;
        [SerializeField]
        float windPower = 1f;
        [SerializeField]
        Transform windEffect;
        [SerializeField]
        InterfaceField<IZoneObserver> zoneObserver;
        [SerializeField]
        float updateDelay = 0.1f;

        Transform[] targets;
        private void Awake()
        {
            zoneObserver.i.Subscribe((x) => targets = x).AddTo(this);

            if (windEffect != null)
                transform.rotation = Quaternion.LookRotation(windDirection);
            StartCoroutine(Wind());
        }

        IEnumerator Wind()
        {
            while (true)
            {
                foreach (Transform x in targets)
                {
                    Rigidbody rb = x.GetComponent<Rigidbody>();
                    rb.MovePosition(x.transform.position + windDirection * windPower * updateDelay);
                }
                yield return new WaitForSeconds(updateDelay);
            }
        }
    }
}