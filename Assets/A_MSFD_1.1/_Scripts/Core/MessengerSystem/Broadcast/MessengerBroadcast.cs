using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class MessengerBroadcast : MessengerBroadcastBase
    {
        protected override void Broadcast()
        {
            Messenger.Broadcast(eventName, messengerMode);
        }
    }
}
