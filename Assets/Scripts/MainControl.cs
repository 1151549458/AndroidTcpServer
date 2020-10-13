using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQFramework;
namespace TcpVideo
{
    public class MainControl : MonoSingleton<MainControl>
    {
        public string strIp;

        public int port;

        public TcpServer tcpServer;

       // public ToPlayVideo[] toPlayVideos;
        public void StartSever(string _ip,int _port)
        {
            strIp = _ip;
            port = _port;
            tcpServer.StartServer(_ip, _port);
        }


        public void SendMsg(string str)
        {
            tcpServer.SendMsg(str);

        }

    }
}