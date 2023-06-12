using UnityEngine;
using Sirenix.OdinInspector;
namespace MSFD
{
    public class IntStreamMessenger : FieldStreamFromMessengerBase<int>
    {
        [Header("Transform value from one range to another")]
        [SerializeField]
        bool isMapRecievedValue = false;
        [ShowIf("$" + nameof(isMapRecievedValue))]
        [SerializeField]
        Vector2Int inputRange = new Vector2Int(0, 100);
        [ShowIf("$" + nameof(isMapRecievedValue))]
        [SerializeField]
        Vector2Int outputRange = new Vector2Int(0, 1);

        public override void OnNext(int value)
        {
            if (isMapRecievedValue)
                value = AS.Calculation.Map(value, inputRange, outputRange);
            base.OnNext(value);
        }
    }
}