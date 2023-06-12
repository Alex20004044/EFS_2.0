using MSFD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using MSFD.AS;
using System;

public class ClipTest : MonoBehaviour
{
    [SerializeField]
    Clip clip = new Clip();

    Vector3 vector = new Vector3();
    Transform[] transforms;
    List<Transform> transformsList;
    void Start()
    {
        clip.StartRecharge();
        clip.GetObsOnCanShoot().Subscribe((_) => Debug.Log("OnCanShoot"));
        clip.GetObsOnShoot().Subscribe((_) => Debug.Log("Shoot!"));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
