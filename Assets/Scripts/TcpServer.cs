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
        //�洢�Ϳͻ���ͨѶ���׽���
        private List<Socket> socConnections = new List<Socket>();
        private List<Thread> dictThread = new List<Thread>();
        private Thread _thread;


        public void StartServer(string _ip, int _port)
        {
            try
            {
                //�����ʼ����ʱ �ڷ���˴���һ���������IP�Ͷ˿ںŵ�Socket
                Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(_ip);
                //��������˿�
                IPEndPoint point = new IPEndPoint(ip, _port);
                socketWatch.Bind(point);//�󶨶˿ں�
                Debug.Log("�����ɹ�!");
                socketWatch.Listen(5);//���ü��������ͬʱ����5̨
                                      //���������߳�
                Thread thread = new Thread(Listen);
                thread.IsBackground = true;
                thread.Start(socketWatch);
                _thread = thread;
                IsContent.Value = true;
            }
            catch (Exception)
            {

                Debug.Log("IP���߶˿ںŴ���...Ip+" + _ip + "  port+" + _port);
            }
        }
        // �ȴ��ͻ��˵����� ���Ҵ�����֮ͨ�ŵ�Socket
        void Listen(object o)
        {
            try
            {
                Socket socketWatch = o as Socket;
                while (true)
                {
                    Socket socketSend = socketWatch.Accept();//�ȴ����տͻ�������
                    Debug.Log(socketSend.RemoteEndPoint.ToString() + ":" + "���ӳɹ�!");
                    //����һ�����̣߳�ִ�н�����Ϣ����
                    Thread r_thread = new Thread(Received);
                    r_thread.IsBackground = true;
                    r_thread.Start(socketSend); //����
                    socConnections.Add(socketSend); //����������� ˵����һ����������
                    dictThread.Add(r_thread); //����������� ˵����һ������̡߳�
                }
            }
            catch { }
        }
        // �������˲�ͣ�Ľ��տͻ��˷�������Ϣ
        void Received(object o)
        {
            Socket socketSend = o as Socket;
            while(true)
            {
                Thread.Sleep(1);//ÿ���߳��ڲ�����ѭ�����涼Ҫ�Ӹ�����ʱ�䡱˯�ߣ�ʹ���߳�ռ����Դ�õ���ʱ�ͷ� 
                Debug.Log("ˢ��");
                //�ͻ������ӷ������ɹ��󣬷��������տͻ��˷��͵���Ϣ
                byte[] buffer = new byte[1024];
                //ʵ�ʽ��յ�����Ч�ֽ���
                int len = socketSend.Receive(buffer);
                if (len == 0)
                {
                    Debug.Log("���ȵ���0 �ҷ���");
                    break;
                }
                //////////������Ϳ��Խ��յ��ַ�����//////////////////////////
                string str = Encoding.Default.GetString(buffer, 0, len);

                ParseHead(str);

                Debug.Log("��������ӡ��" + socketSend.RemoteEndPoint + ":" + str);
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
            //�ж��Լ��������Ƿ���ȷ
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
                Debug.Log("��������ʧ��" + " bytes��" + bytes);
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
       
        // �˳�ʱ�ر�һЩ�߳� ������
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