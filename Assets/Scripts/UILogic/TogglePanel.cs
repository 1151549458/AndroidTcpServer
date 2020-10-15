using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SQFramework;

namespace TcpVideo
{
    using UniRx;
    public class TogglePanel : MonoBehaviour
    {
        public bool isToggleConnect;
        public Button btn01;
        public Button btn02;
        public string toggleName;

        public string strOn1 = "on1";
        public string strOff1 = "off1";
        public string strOn2 = "on2";
        public string strOff2 = "off2";

        private string str01 = "";
        private string str02 = "";
        private BoolReactiveProperty isTog01 = new BoolReactiveProperty(false);
        private BoolReactiveProperty isTog02 = new BoolReactiveProperty(false);
        void Start()
        {
            isToggleConnect = false;

            btn01.transform.GetChild(1).Hide();
            btn02.transform.GetChild(1).Hide();

            isTog01.Subscribe(_=> {
                if (_)
                {
                    btn01.transform.GetChild(1).Show();
                    btn01.transform.GetChild(0).Hide();
                    str01 = strOn1;
                }
                else
                {
                    btn01.transform.GetChild(0).Show();
                    btn01.transform.GetChild(1).Hide();  
                    str01 = strOff1;
                }
                MainControl.Instance().SendMsg(str01);

            });
            isTog02.Subscribe(_=> {
                if (_)
                {
                    btn01.transform.GetChild(1).Show();
                    btn01.transform.GetChild(0).Hide();
                    str02 = strOn2;
                }
                else
                {
                    btn01.transform.GetChild(0).Show();
                    btn01.transform.GetChild(1).Hide();
                    str02 = strOff2;
                }
                MainControl.Instance().SendMsg(str02);
            });

            btn01.onClick.AddListener(()=> {
                isTog01.Value = !isTog01.Value;  
            });
            btn02.onClick.AddListener(()=> {
                isTog02.Value = !isTog02.Value; 
            });

        }

       
    }
}