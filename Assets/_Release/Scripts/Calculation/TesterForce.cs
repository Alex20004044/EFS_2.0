using System.Collections;
using System.Collections.Generic;
using CorD.SparrowInterfaceField;
using MSFD;
using MSFD.AS;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EFS
{
    public class TesterForce : MonoBehaviour
    {
        ChargePoint[] chargePoints;

        bool isActivated;
        [SerializeField]
        Server server;
        [SerializeField]
        bool isSendMessagesToServer = false;

        Vector3 electricForce;

        private void Start()
        {
            chargePoints = FindObjectsOfType<ChargePoint>();
            isActivated = true;

            Messenger.Subscribe(GameEvents.I_CHARGE_UPDATED, OnChargeDestroyed);
        }

        private void OnDestroy()
        {
            server.CloseServer();
        }
        void OnChargeDestroyed()
        {
            chargePoints = FindObjectsOfType<ChargePoint>();
        }

        void Update()
        {
            if (!isActivated) return;

            //int index = MapUtilities.ConvertMeasurePointPositionToIndex(new Vector2(transform.position.x, transform.position.z),
            //     electricFieldCalculator.GetMapSize(), electricFieldCalculator.GetMeasuresPerUnit());
            electricForce = MapUtilities.CalculateIntesityV3(transform.position, chargePoints);
            //float angle = Coordinates.AngleFromDirection(electricForce);
            Quaternion rotation = Quaternion.LookRotation(electricForce, Vector3.up);

            transform.rotation = rotation;
            if(isSendMessagesToServer)
                server.SendMessageToClient(JsonUtility.ToJson(new ElectricData(electricForce, transform.position)));
        }

        
    }
    struct ElectricData
    {
        public Vector3 force;
        public Vector3 position;

        public ElectricData(Vector3 force, Vector3 position)
        {
            this.force = force;
            this.position = position;
        }
    }

}
