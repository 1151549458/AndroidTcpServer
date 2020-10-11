using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SQFramework;
using System;
namespace TcpVideo
{
    public class TcpSectionUI : MonoBehaviour
    {
        public Button btnContent;
        public InputField inputFieldIp;
        public InputField InputFieldPort;
        public Button btnNext;
        private string strIP = string.Empty;
        private int port;
        void Start()
        {
            btnContent.onClick.AddListener(() =>
            {
                InitSever();
          
            });
            btnNext.onClick.AddListener(()=> {
                InitSever();
            });
            SetNormal();
        }
        void SetNormal()
        {
            inputFieldIp.text = "192.168.1.1";
            InputFieldPort.text = "8888";
        }
        void InitSever()
        {
            string ip = inputFieldIp.text;
            string port = InputFieldPort.text;
            MainControl.Instance().StartSever(ip, Convert.ToInt32(port));
            gameObject.Hide();
        }

 
    }
}