/**
 *Copyright(C) 2018 by DefaultCompany
 *All rights reserved.
 *FileName:     CommonUtil.cs
 *Author:       Pan
 *Version:      1.0
 *UnityVersion��2018.4.13f1
 *Date:         2020-05-18
 *Description:   
 *History:
*/using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace SQFramework
{   /// <summary>
    /// 通用类
    /// </summary>
    public static partial class CommonUtil
    {
        /// <summary>
        /// 复制文本
        /// </summary>
        /// <param name="strText"></param>
        public static void CopyText(string strText)
        {
            GUIUtility.systemCopyBuffer = strText;
        }

        /////轴心圆
        private static float angel = 0; //
        /// <summary>
        /// 轴心圆
        /// </summary>
        /// <param name="gos"></param>
        /// <param name="radius"></param>
        /// <param name="Center"></param>
        /// <param name="isLook"></param>
        public static void CreatSelectPanel(GameObject[] gos, float radius, Transform Center, bool isLook)
        {
            for (int i = 0; i < gos.Length; i++)
            {
                //一弧度 等于 pai/180 2pi/360 
                angel += 360 / gos.Length;
                float radian = angel * (Mathf.PI / 180.0f);

                Vector3 pos = Center.localPosition;
                pos.z += radius * Mathf.Cos(radian);
                pos.x += radius * Mathf.Sin(radian);

                gos[i].GetComponent<Transform>().localPosition = Vector3.zero;

                if (isLook)
                {
                    gos[i].GetComponent<Transform>().eulerAngles = new Vector3(0, angel - 180, 0);
                }
                gos[i].GetComponent<Transform>().localPosition = pos;

            }

        }
        /// <summary>
        /// 轴心圆UI
        /// </summary>
        /// <param name="center"></param>
        /// <param name="gos"></param>
        /// <param name="radius"></param>
        public static void CreatSelectPanel(RectTransform center, GameObject[] gos, float radius,bool isEul = false)
        {
         
            int angle = 0;
            for (int i = 0; i < gos.Length; i++)
            {
                angle += (360 / gos.Length) * (i + 1); //算出角度 
                float vx = center.anchoredPosition.x + radius * Mathf.Cos(angle * Mathf.PI / 180);
                float vy = center.anchoredPosition.x + radius * Mathf.Sin(angle * Mathf.PI / 180);
                gos[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(vx, vy);
                if (isEul)
                {
                    gos[i].GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, angle - 90);
                }
                angle = 0;
            }
           
        }



        /// <summary>
        /// 设置鼠标隐藏还是显示
        /// </summary>
        /// <param name="b"></param>
        public static void SetCursor(bool b)
        {
            Cursor.visible = b;
        }

        /// <summary>
        /// 用 ASCII 码范围判断字符是不是汉字
        /// </summary>
        /// <param name="text">待判断字符或字符串</param>
        /// <returns>真：是汉字；假：不是</returns>
        public static bool CheckStringChinese(string text)
        {
            bool res = false;
            foreach (char t in text)
            {
                if ((int)t > 127)
                    res = true;
            }
            return res;
        }

        #region 平台路径选择
        public static void Select_Platform(out string str)
        {
            //if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            //{//这样也行   //}
#if UNITY_ANDROID
      Debug.Log("这里是安卓设备");
      str = "jar:file://" + Application.dataPath + "!/assets/" ; 
#endif
#if UNITY_IPHONE
      Debug.Log("这里是苹果设备");  
      str =  Application.dataPath + "/Raw/";
#endif
#if UNITY_STANDALONE_WIN
            Debug.Log("我是从Windows的电脑上运行的");
            str = Application.streamingAssetsPath + "/";
#endif

#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
        str = Application.persistentDataPath + "/";
#endif
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
            str = Application.streamingAssetsPath + "/";
#endif
        }
        #endregion

    }
}