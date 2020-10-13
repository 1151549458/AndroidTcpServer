using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQFramework;
using System;
namespace TcpVideo
{
    public class TcpOrder : MonoSingleton<TcpOrder>
    {
        
        public const string orderVideoInfo = "VideoMsg|";
        public const string orderPlay = "VpPlay:";
        public const string orderPause = "VpPause:";
        public const string orderStop = "VpStop:";
        public const string orderDrag = "VpSlider:";
        public const string orderToggle = "Toggle:";

        public float fDragValue =0;
        public bool bIsToggle = false;
        public string GetPlayMsg()
        {
            return orderVideoInfo + orderPlay;
        }
        public string GetPauseMsg()
        {
            return orderVideoInfo + orderPause; 
        }

        public string GetStopMsg()
        {
            return orderVideoInfo + orderStop; 
        }
        public string GetDragMsg(float f)
        {
            return orderVideoInfo + orderDrag + f.ToString(); 
        }
        public string GetToggleMsg(bool b)
        {
            return orderVideoInfo + orderToggle + b.ToString();
        }


        public void ParseHead(string strMsg)
        {
            string[] arrayMsg = strMsg.Split('|');
            //�ж��Լ��������Ƿ���ȷ
            switch (arrayMsg[0])
            {
                case "window01":

                    ParesMsg(arrayMsg[1]);
                    break;
                case "window02":
                    ParesMsg(arrayMsg[1]);
                    break;
                default:

                    break;
            }

        }

        public void ParesMsg(string head)
        {
            string[] array = head.Split(':');
            switch (array[0])
            {
                case orderPlay:
                    //������Ƶ
                    break;
                case orderPause:
                    //��ͣ��Ƶ
                    break;
                case orderStop:
                    //ֹͣ������Ƶ
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

        public float GetDragValue(string s)
        {
            return Convert.ToSingle(s);
        }
        public bool GetToggleValue(string s)
        {
            return Convert.ToBoolean(s);
        }

    }
}