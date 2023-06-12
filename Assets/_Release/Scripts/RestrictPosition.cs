using System.Collections;
using System.Collections.Generic;
using MSFD.AS;
using UnityEngine;

namespace EFS
{
    public class RestrictPosition : MonoBehaviour
    {
        [SerializeField]
        Vector2 maxPos;
        private void Update()
        {
            if (transform.position.x > maxPos.x)
                transform.position = transform.position.SetXAxis(maxPos.x);            
            if (transform.position.x < -maxPos.x)
                transform.position = transform.position.SetXAxis(-maxPos.x);

            if (transform.position.z > maxPos.y)
                transform.position = transform.position.SetZAxis(maxPos.y);
            if (transform.position.z < -maxPos.x)
                transform.position = transform.position.SetZAxis(-maxPos.y);
        }
    }
}
