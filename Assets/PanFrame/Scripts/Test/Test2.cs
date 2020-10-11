/**
 *Copyright(C) 2018 by DefaultCompany
 *All rights reserved.
 *FileName:     Test2.cs
 *Author:       Pan
 *Version:      1.0
 *UnityVersion��2018.4.13f1
 *Date:         2020-05-28
 *Description:   
 *History:
*/using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQFramework;
public class Test2 : MonoBehaviourSimplify
{
    protected override void OnBeforeDestroy()
    {
        
    }
    public GameObject gos;
    // Start is called before the first frame update
    void Start()
    {
        this.Delay(2.0f,()=> {
            SendMsg("ok","触发了");
        });
    } 
    // Update is called once per frame
    void Update()
    {
        
    }
}
