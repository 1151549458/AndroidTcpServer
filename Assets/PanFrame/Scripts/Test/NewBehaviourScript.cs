/**
 *Copyright(C) 2018 by DefaultCompany
 *All rights reserved.
 *FileName:     NewBehaviourScript.cs
 *Author:       Pan
 *Version:      1.0
 *UnityVersion��2018.4.13f1
 *Date:         2020-05-21
 *Description:   
 *History:
*/using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SQFramework;
public class NewBehaviourScript : MonoBehaviourSimplify 
{
    public Text text;
    protected override void OnBeforeDestroy()
    { 

    }
     
    void Start()
    { 
        Debug.Log(MathUtil.GetRandomValueFrom(1, 2, 3));
        Debug.Log(MathUtil.GetRandomValueFrom("asdasd", "123123"));
        Debug.Log(MathUtil.GetRandomValueFrom(0.2f));
        Debug.Log(MathUtil.Percent(50)); //代表50%

        RegisterMsg("ok",da=> {
            Debug.Log("ok");
        });
        //string st1 = CongfigMgr.Instance().GetConfig().Get(Constant.ConfigField.RES_PATH);
        //Debug.Log(st1);
        //text.text = st1;

        //谨慎调用
       // DownLoadTools.DownTool(DownLoadTools.fishMovePath);
        //SUnityWebRequest.Instance().DownloadFile("https://psq.youthup.cn/Picture/01.png",Application.streamingAssetsPath+"/01.png",(s)=> {

        Debug.Log("https://psq.youthup.cn/CodePlugin/101FishMoveTool/explain.txt");


        //});
    }
     
    void Update()
    {
        
    }
}
