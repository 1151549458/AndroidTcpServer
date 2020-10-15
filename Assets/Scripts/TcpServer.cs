using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQFramework;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System;
using UniRx;
namespace TcpVideo
{
    public class TcpServer : MonoSingleton<TcpServer>
    {
        public BoolReactiveProperty IsContent = new BoolReactiveProperty(false);

        public Action CallTcpSuccess01 = null;
        public Action CallTcpSuccess02 = null;
        public Action CallTcpSuccess03 = null;
        //存储和客户端通讯的套接字
        private List<Socket> socConnections = new List<Socket>();
        private List<Thread> dictThread = new List<Thread>();
        private Thread _thread;


        public void StartServer(string _ip, int _port)
        {
            try
            {
                //点击开始监听时 在服务端创建一个负责监听IP和端口号的Socket
                Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(_ip);
                //创建对象端口
                IPEndPoint point = new IPEndPoint(ip, _port);
                socketWatch.Bind(point);//绑定端口号
                Debug.Log("监听成功!");
                socketWatch.Listen(5);//设置监听，最大同时连接5台
                                      //创建监听线程
                Thread thread = new Thread(Listen);
                thread.IsBackground = true;
                thread.Start(socketWatch);
                _thread = thread;
                IsContent.Value = true;
            }
            catch (Exception)
            {

                Debug.Log("IP或者端口号错误...Ip+" + _ip + "  port+" + _port);
            }
        }
        // 等待客户端的连接 并且创建与之通信的Socket
        void Listen(object o)
        {
            try
            {
                Socket socketWatch = o as Socket;
                while (true)
                {
                    Socket socketSend = socketWatch.Accept();//等待接收客户端连接
                    Debug.Log(socketSend.RemoteEndPoint.ToString() + ":" + "连接成功!");
                    //开启一个新线程，执行接收消息方法
                    Thread r_thread = new Thread(Received);
                    r_thread.IsBackground = true;
                    r_thread.Start(socketSend); //传参
                    socConnections.Add(socketSend); //把这个存起来 说明有一个链接上了
                    dictThread.Add(r_thread); //把这个存起来 说明有一个这个线程。
                }
            }
            catch { }
        }
        // 服务器端不停的接收客户端发来的消息
        void Received(object o)
        {
            Socket socketSend = o as Socket;
            while(true)
            {
                Thread.Sleep(1);//每个线程内部的死循环里面都要加个“短时间”睡眠，使得线程占用资源得到及时释放 
                Debug.Log("刷新");
                //客户端连接服务器成功后，服务器接收客户端发送的消息
                byte[] buffer = new byte[1024];
                //实际接收到的有效字节数
                int len = socketSend.Receive(buffer);
                if (len == 0)
                {
                    Debug.Log("长度等于0 我返回");
                    break;
                }
                //////////到这里就可以接收到字符串了//////////////////////////
                string str = Encoding.Default.GetString(buffer, 0, len);

                ParseHead(str);

                Debug.Log("服务器打印：" + socketSend.RemoteEndPoint + ":" + str);
            }
        }

        public void SendMsg(string str)
        {
            for (int i = 0; i < socConnections.Count; i++)
            {
                bt_send_Click(socConnections[i],str);
            }
        }
         
        public void ParseHead(string strMsg)
        {
            string[] arrayMsg = strMsg.Split('='); 
            string head = arrayMsg[1].Trim('"');
            //判断自己的名字是否正确
            switch (head)
            {
                case "window01":
                    CallTcpSuccess01?.Invoke();
                    break;
                case "window02":
                    CallTcpSuccess02?.Invoke();
                    break;
                case "HHC-NET2D":
                    CallTcpSuccess03?.Invoke();   
                    break; 
                default:

                    break;
            }

        }
        private void bt_send_Click(Socket socket, string str)
        {
            try
            {
                string msg = str;
                byte[] buffer = new byte[1024];
                buffer = Encoding.UTF8.GetBytes(msg);
                socket.Send(buffer);
            }
            catch { }
        }
        private void bt_send_Click(Socket socket, byte[] bytes)
        {
            try
            {
                socket.Send(bytes);
            }
            catch
            {
                Debug.Log("发送数据失败" + " bytes：" + bytes);
            }
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            for (int i = 0; i < dictThread.Count; i++)
            {
                dictThread[i].Abort();
                dictThread.Clear();
            }
            if (_thread != null)
                _thread.Abort();
        }
       
        // 退出时关闭一些线程 防卡死
        private void OnApplicationQuit()
        {
            for (int i = 0; i < dictThread.Count; i++)
            {
                dictThread[i].Abort();
                dictThread.Clear();
            }
            if (_thread != null)
                _thread.Abort();
        }
    }

}