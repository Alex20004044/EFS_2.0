using CorD.SparrowInterfaceField;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD.DebugTool
{
    public class DebugLog : MonoBehaviour
    {
        [SerializeField]
        string message;
        [ShowIf("@messageSource == MessageSource.fieldGetter")]
        [SerializeField]
        InterfaceField<IFieldGetter<string>> fieldGetter;
        [ShowIf("@messageSource == MessageSource.stream")]
        [SerializeField]
        InterfaceField<IObservable<string>> stream;
        ReactiveProperty<string> messageProperty = new ReactiveProperty<string>();
        
        [SerializeField]
        MessageSource messageSource = MessageSource.staticValue;
        [SerializeField]
        DebugLogType logType;

        private void Start()
        {
            if (messageSource == MessageSource.stream)
            {
                messageProperty.Value = $"Init debug log stream at {gameObject.name}";
                messageProperty.Subscribe(LogMessage).AddTo(this);
                stream.i.Subscribe((x) => messageProperty.Value = x).AddTo(this);
            }
        }
        [Button]
        public void LogMessage()
        {
            LogMessage(GetMessage());
        }
        public void LogMessage(string message)
        {
            switch (logType)
            {
                case DebugLogType.debug:
                    {
                        Debug.Log(message);
                        break;
                    }
                case DebugLogType.warning:
                    {
                        Debug.LogWarning(message);
                        break;
                    }
                case DebugLogType.error:
                    {
                        Debug.LogError(message);
                        break;
                    }
            }
        }
        public void LogCurrentTimeMessage(string message)
        {
            LogMessage(message + " " + System.DateTime.Now.TimeOfDay);
        }

        string GetMessage()
        {
            switch (messageSource)
            {
                case MessageSource.staticValue:
                    return message;
                case MessageSource.fieldGetter:
                    return message + fieldGetter.i.GetValue();
                case MessageSource.stream:
                    return message + messageProperty.Value;
                default:
                    return "ERROR";
            }
        }

        enum DebugLogType { debug, warning, error };
        enum MessageSource { staticValue, fieldGetter, stream };

    }
}
