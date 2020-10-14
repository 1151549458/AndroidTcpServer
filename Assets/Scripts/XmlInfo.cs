using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQFramework;
using System;
namespace TcpVideo
{
    public class XmlInfo : MonoBehaviour
    {
        string path = string.Empty;

        public string TagInfo = "SocketInfo";
        public string IpInfo = "VpIp";
        public string PortInfo = "VpPort";
        public string IsToggle = "VpToggle";
        public Info qInfo;
        void Awake()
        { 
#if UNITY_ANDROID
      path = Application.persistentDataPath;
#endif
#if UNITY_STANDALONE_WIN
            Debug.Log("我是从Windows的电脑上运行的");
            path = Application.streamingAssetsPath ;
#endif

            Debug.Log(path);
            path += "/DataInfo.xml";

            if (!DirFileHelper.IsExistsFile(path))
            {
                XmlHelper.CreateXml(path, "Root", TagInfo);
                SetIpPortInfo("", "", false);

            }

        }


        public void SetIpPortInfo(string sip, string sport, bool ison)
        {
            Debug.Log("sss");
            XmlHelper.SetXmlAttribute(path, TagInfo, IpInfo, sip); 
            XmlHelper.SetXmlAttribute(path, TagInfo, PortInfo, sport);
            XmlHelper.SetXmlAttribute(path, TagInfo, IsToggle, ison.ToString());
        }

        public Info GetIpPortInfo()
        {
            return qInfo = new Info {  sIp = XmlHelper.GetXmlAttribute(path, TagInfo, IpInfo),
                 sPort = XmlHelper.GetXmlAttribute(path, TagInfo, PortInfo),
                 sIson = XmlHelper.GetXmlAttribute(path, TagInfo, IsToggle)
            };
        }
        public bool IsOpenTcpPanel()
        {
            return Convert.ToBoolean(XmlHelper.GetXmlAttribute(path, TagInfo, IsToggle));
        }




    }
    public class Info
    {
        public string sIp;
        public string sPort;
        public string sIson;
    }
}