using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace MSFD
{
    public class UnityEventOnRandomTime : UnityEventBase
    {
        [SerializeField]
        TimerWorkMode timerWorkMode = TimerWorkMode.single;
        [SerializeField]
        RandomMode randomMode = RandomMode.betweenTwoConstants;
        [HorizontalGroup("MinMax")]
        [ShowIf("@randomMode == RandomMode.betweenTwoConstants")]
        [SerializeField]
        float minDelay = 1;
        [HorizontalGroup("MinMax")]
        [ShowIf("@randomMode == RandomMode.betweenTwoConstants")]
        [SerializeField]
        float maxDelay = 10f;
        [ShowIf("@randomMode == RandomMode.curve")]
        [SerializeField]
        AnimationCurve randomCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 10));

        [SerializeField]
        ActivationModeStandart activationMode = ActivationModeStandart.onEnable;

        [HorizontalGroup]
        [ReadOnly]
        [SerializeField]
        bool isTimerActivated = false;
        [HorizontalGroup]
        [ReadOnly]
        [SerializeField]
        float delay = -1;
        private void OnEnable()
        {
            if (activationMode == ActivationModeStandart.onEnable)
            {
                ActivateTimer();
            }
        }
        private void OnDisable()
        {
            CancelInvoke();
        }
        [Sirenix.OdinInspector.Button]
        public void ActivateTimer()
        {
            if (isTimerActivated)
            {
                Debug.LogError("Attempt to activate already activated timer");
                return;
            }
            isTimerActivated = true;
            if (randomMode == RandomMode.betweenTwoConstants)
            {
                delay = Random.Range(minDelay, maxDelay);
            }
            else
            {
                delay = randomCurve.Evaluate(Random.Range(0f, 1f));
            }
            Invoke("Invoke", delay);
        }

        void Invoke()
        {
            InvokeEvent();
            isTimerActivated = false;
            if(timerWorkMode == TimerWorkMode.cycle)
            {
                ActivateTimer();
            }
        }
        enum TimerWorkMode { single, cycle }
        enum RandomMode { betweenTwoConstants, curve}
    }
}