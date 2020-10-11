/**
 *Copyright(C) 2018 by DefaultCompany
 *All rights reserved.
 *FileName:     MsgDispatcher.cs
 *Author:       Pan
 *Version:      1.0
 *UnityVersion��2018.4.13f1
 *Date:         2020-05-15
 *Description:   
 *History:
*/using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
namespace SQFramework
{
    public static class MsgDispatcher
    {
        private static Dictionary<string, Action<object>> RegisteredMsgs = new Dictionary<string, Action<object>>();

        public static void Register( string msgName, Action<object> onMsgReceived)
        {
            if (!RegisteredMsgs.ContainsKey(msgName))
            {
                RegisteredMsgs.Add(msgName, _ => { });
            }
            RegisteredMsgs[msgName] += onMsgReceived; 
        }

        public static void UnRegister(string msgName, Action<object> onMsgRecieved)
        {
            if (RegisteredMsgs.ContainsKey(msgName))
            {
                RegisteredMsgs[msgName] -= onMsgRecieved;
            }
        }
         
        public static void SendMsg( string msgName, object data)
        {
            if (RegisteredMsgs.ContainsKey(msgName))
            {
                RegisteredMsgs[msgName](data);
            } 
        }
     
        private static void MenuClicked()
        {
            //Register("消息1", data => { Debug.LogFormat("消息1:{0}", data); });

            //Send("消息1", "hello world");

            //UnRegister("消息1");

            //Send("消息1", "hello");
        }

    }


 
   

}
