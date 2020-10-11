/**
 *Copyright(C) 2018 by DefaultCompany
 *All rights reserved.
 *FileName:     ToolsDownLoad.cs
 *Author:       Pan
 *Version:      1.0
 *UnityVersion��2018.4.13f1
 *Date:         2020-06-10
 *Description:   
 *History:
*/using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

namespace SQFramework
{
    public class DownLoadTools
    {
        public const string youthupPath = "https://psq.youthup.cn/CodePlugin/";
        public const string locaPath = "/PanFrame/Scripts/01_Extensions/";
        public const string explainPath = "explain.txt";

        public const string fishMovePath = "101_FishMoveTool";
        public const string jsonToolPath = "102_JsonTool";
        
        public static void DownTool(string toolPath)
        {
            string url = youthupPath + toolPath + "/" ;  //服务器文件
            string locurl = Application.dataPath + locaPath+ toolPath+"/"; //本地路径

            //SUnityWebRequest.Instance().DownloadFile(url, locurl, (r)=> {

            //    string[] arrayLine = File.ReadAllLines(locurl);
            //    for (int i = 0; i < arrayLine.Length; i++)
            //    {
            //        string currentUrl = youthupPath + toolPath + "/" + arrayLine[i];
            //        string currentlocurl = Application.dataPath + locaPath + toolPath + "/" + arrayLine[i];
            //        SUnityWebRequest.Instance().DownloadFile(currentUrl, currentlocurl,(s)=> {
            //        }); 
            //    } 
            //});
            if (!DirFileHelper.IsExistsFile(locurl + explainPath))
            {
                WebClient wc = new WebClient();
                wc.DownloadFileCompleted += (send, e) =>
                {
                    Debug.Log("下载完成" + send);
                    Debug.Log(locurl + explainPath);
                    string[] arrayLine = File.ReadAllLines(locurl+ explainPath);
                    for (int i = 0; i < arrayLine.Length; i++)
                    {
                        string currentUrl = url + arrayLine[i];
                        string currentlocurl = locurl + arrayLine[i];
                        Debug.Log(currentUrl);
                        Debug.Log(currentlocurl);
                        wc.DownloadFile(new Uri(currentUrl), currentlocurl);
                    } 
                };
                Debug.Log(url);
                Debug.Log(locurl);
                DirFileHelper.CreateDirectory(locurl);

                wc.DownloadFileAsync(new Uri(url+ explainPath), locurl+ explainPath);
            }
            else
            {
                Debug.Log("文件已经存在 "+locurl);
            }
          
        }

      

    }


    [CreateAssetMenu(fileName ="x",menuName = "CodeData")]
    public class CodeData : ScriptableObject
    {
        public string[] strCodePath;
        public bool[] isCodePath;
    }
}