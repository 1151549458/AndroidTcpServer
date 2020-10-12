using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQFramework;
using System;
namespace TcpVideo
{
    public class TcpOrder 
    {
        public const string orderVideoInfo = "VideoMsg|";
        public const string orderPlay = "VpPlay:";
        public const string orderPause = "VpPause:";
        public const string orderStop = "VpStop:";
        public const string orderDrag = "VpSlider:";
        public const string orderToggle = "Toggle:";

        public static float fDragValue =0;
        public static bool bIsToggle = false;
        public static string GetPlayMsg()
        {
            return orderVideoInfo + orderPlay;
        }
        public static string GetPauseMsg()
        {
            return orderVideoInfo + orderPause; 
        }

        public static string GetStopMsg()
        {
            return orderVideoInfo + orderStop; 
        }
        public static string GetDragMsg(float f)
        {
            return orderVideoInfo + orderDrag + f.ToString(); 
        }
        public static string GetToggleMsg(bool b)
        {
            return orderVideoInfo + orderToggle + b.ToString();
        }


        public static void ParseHead(string strMsg)
        {
            string[] arrayMsg = strMsg.Split('|');
            //判断自己的名字是否正确
            switch (arrayMsg[0])
            {
                case "name1":
                    ParesMsg(arrayMsg[1]);
                    break;
                case "name2":
                    ParesMsg(arrayMsg[1]); 
                    break;
            }

        }

        public static void ParesMsg(string head)
        {
            string[] array = head.Split(':');
            switch (array[0])
            {
                case orderPlay:
                    //播放视频
                    break;
                case orderPause:
                    //暂停视频
                    break;
                case orderStop:
                    //停止播放视频
                    break;
                case orderDrag:
                    fDragValue = GetDragValue(array[1]);
                    break;
                case orderToggle:
                    bIsToggle = GetToggleValue(array[1]);
                    break;
                default:
                    break;
            }
 
        }

        public static float GetDragValue(string s)
        {
            return Convert.ToSingle(s);
        }
        public static bool GetToggleValue(string s)
        {
            return Convert.ToBoolean(s);
        }

    }
}