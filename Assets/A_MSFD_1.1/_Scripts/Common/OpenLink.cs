using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class OpenLink : MonoBehaviour
    {
        [SerializeField]
        string link;
        public void LoadLink()
        {
            Application.OpenURL(link);
        }
    }
}