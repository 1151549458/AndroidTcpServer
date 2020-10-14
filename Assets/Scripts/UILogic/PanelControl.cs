using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQFramework;
using UniRx;
using System;

namespace TcpVideo
{
    public class PanelControl : MonoBehaviour
    {
        public GameObject goTcpInfoPanel;
        public GameObject goVideoPanel;
        
        // Start is called before the first frame update
        void Start()
        {
            goVideoPanel.Hide();
            this.Delay(1.0f,()=> {
                if (!MainControl.Instance().xmlInfo.IsOpenTcpPanel())
                {
                    goTcpInfoPanel.Show();
                    goTcpInfoPanel.GetComponent<TcpSectionUI>().toggle.isOn = false;
                }
                else
                {
                    goTcpInfoPanel.Hide();
                    goTcpInfoPanel.GetComponent<TcpSectionUI>().toggle.isOn = true;
                    Info qInfo = MainControl.Instance().xmlInfo.GetIpPortInfo();

                    MainControl.Instance().StartSever(qInfo.sIp, Convert.ToInt32(qInfo.sPort));
                }
            });

            MainControl.Instance().tcpServer.IsContent.Subscribe(_=> {
                if (_)
                {
                    goVideoPanel.Show();
                }
            });


        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}