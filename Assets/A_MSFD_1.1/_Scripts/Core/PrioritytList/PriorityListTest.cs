using MSFD;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PriorityListTest : MonoBehaviour
{

    [SerializeField]
    PriorityListAction<float> priorityList;
    [SerializeField]
    PriorityListFunc<float> priorityList2 = new PriorityListFunc<float>();
    [Button]
    void Start()
    {
        priorityList.Add(x => Debug.Log(x*3), 10);
        priorityList.Add(x=>Debug.Log(x), -10);
        priorityList.Add(x=>Debug.Log(x+1), -10);
        priorityList.Add(x => Debug.Log(x*2));

        foreach (var x in priorityList)
            x.Invoke(10);

    }
    [Button]
    void Func()
    {
        priorityList2.Add(x => x * 3, 10);
        priorityList2.Add(x => x, -10);
        priorityList2.Add(x => x * 2);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
