using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using System;
using MSFD.Service;

namespace MSFD.DebugTool
{
    public class MessengerEditorWindow : OdinEditorWindow
    {
        [HorizontalGroup("System")]
        [SerializeField]
        bool isRecieveMessages = true;
        /*[HorizontalGroup("System")]
        [SerializeField]
        bool isDisableRecieverOnWindowClose = true;*/
        [Searchable()]
        [TableList(HideToolbar = false)]
        [ListDrawerSettings(ShowIndexLabels = false,/*, OnBeginListElementGUI = "BeginDrawListElement", OnEndListElementGUI = "EndDrawListElement",*/
            DraggableItems = false, HideAddButton = true)]
        [SerializeField]
        List<DisplayedMessage> messengerEvents = new List<DisplayedMessage>();

        static MessengerEditorWindow instance;

        const string savePath = ServiceConstants.editorDataPath + "MessengerEditor/";

        [MenuItem("MSFD/Messenger View")]
        private static void OpenWindow()
        {
            GetWindow<MessengerEditorWindow>().Show();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            MI.AddMessengerEventsListener(OnMessengerEvent);
            Load();
        }
        private void OnDisable()
        {
            /*if (isDisableRecieverOnWindowClose)
            {*/
            MI.RemoveMessengerEventsListener(OnMessengerEvent);

            Clear();
            //}
            Save();
        }

        void Save()
        {
            SaveCore.Save(savePath + nameof(isRecieveMessages), isRecieveMessages);
            //SaveCore.Save(savePath + nameof(isDisableRecieverOnWindowClose), isDisableRecieverOnWindowClose);
        }
        void Load()
        {
            isRecieveMessages = SaveCore.Load(savePath + nameof(isRecieveMessages), isRecieveMessages);
            //isDisableRecieverOnWindowClose = SaveCore.Load(savePath + nameof(isDisableRecieverOnWindowClose), isDisableRecieverOnWindowClose);
        }
        public void OnMessengerEvent(string broadcastedEvent, MessengerMode messengerMode, int listenersCount, string[] args)
        {
            if (!isRecieveMessages)
                return;
            messengerEvents.Insert(0, new DisplayedMessage(messengerEvents.Count, broadcastedEvent, messengerMode, listenersCount, args));
            OnGUI();
        }
        string EventFormat(string messengerEvent)
        {
            return messengerEvent + " " + System.DateTime.Now;
        }
        public int ListElementIndex(int index)
        {
            return (messengerEvents.Count - index);
        }
        [HorizontalGroup("System")]
        [Button()]
        void Clear()
        {
            messengerEvents = null;
        }


        [Serializable]
        public struct DisplayedMessage
        {
            [DisplayAsString]
            [TableColumnWidth(20)]
            public int index;
            [DisplayAsString]
            [TableColumnWidth(650)]
            public string broadcastedEvent;
            [DisplayAsString]
            [TableColumnWidth(40)]
            public int listeners;
            [TableColumnWidth(10)]
            [DisplayAsString]
            public string mMode;

            [DisplayAsString]
            [TableColumnWidth(100)]
            public string arg_0;
            [DisplayAsString]
            [TableColumnWidth(10)]
            public string arg_1;
            [DisplayAsString]
            [TableColumnWidth(10)]
            public string arg_2;

            [DisplayAsString]
            [TableColumnWidth(50)]
            public string time;
            public DisplayedMessage(int index, string broadcastedEvent, MessengerMode messengerMode, int listeners, string[] args)
            {
                this.index = index;
                this.broadcastedEvent = broadcastedEvent;
                this.listeners = listeners;
                if (messengerMode == MessengerMode.DONT_REQUIRE_LISTENER)
                {
                    this.mMode = "D";
                }
                else
                {
                    this.mMode = "R";
                }

                time = DateTime.Now.TimeOfDay.ToString("");
                this.arg_0 = "-";
                this.arg_1 = "-";
                this.arg_2 = "-";
                if (args != null)
                {
                    if (args.Length > 0)
                        this.arg_0 = args[0];
                    if (args.Length > 1)
                        this.arg_1 = args[1];
                    if (args.Length > 2)
                        this.arg_2 = args[2];
                }
            }
        }
    }
}