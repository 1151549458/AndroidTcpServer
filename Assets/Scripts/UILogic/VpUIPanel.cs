using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQFramework;
using UnityEngine.UI;

namespace TcpVideo
{
    using UniRx;
    public class VpUIPanel : MonoBehaviour
    {
        public ToPlayVideo[] toPlayVideos;
        public Text text;
        void Start()
        {
            text.text = "�̵�����δ����";
            MainControl.Instance().tcpServer.CallTcpSuccess03 += () => {
                ThreadHelperTool.QueueOnMainThread(() => {
                    text.text = "�̵������ӳɹ�";

                });
            };
        }


        
    }
}