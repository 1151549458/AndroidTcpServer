/**
 *Copyright(C) 2018 by DefaultCompany
 *All rights reserved.
 *FileName:     Device.cs
 *Author:       Pan
 *Version:      1.0
 *UnityVersion��2018.4.13f1
 *Date:         2020-05-18
 *Description:   
 *History:
*/using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace SQFramework
{
    public partial class Device : MonoBehaviour
    {
#if UNITY_EDITOR
        [MenuItem("SQFramework/屏幕分辨率")]
        private static void ScreenMenuClicked()
        {
            Debug.Log(IsPhoneResolution() ? "是 Phone 分辨率" : "不是 Phone 分辨率");
        }
        /// <summary>
        /// 获取屏幕宽高比
        /// </summary>
        /// <returns></returns>
        public static float GetAspectRatio()
        {
            return Screen.width > Screen.height ? (float)Screen.width / Screen.height : (float)Screen.height / Screen.width;
        }
        public static bool IsPhoneResolution()
        {
            var aspect = GetAspectRatio();
            return aspect > 16.0f / 9 - 0.05 && aspect < 16.0f / 9 + 0.05;
        }
#endif
    }
}