/**
 *Copyright(C) 2018 by DefaultCompany
 *All rights reserved.
 *FileName:     MonoBehaviourSimplify.cs
 *Author:       Pan
 *Version:      1.0
 *UnityVersion��2018.4.13f1
 *Date:         2020-05-18
 *Description:   
 *History:
*/using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SQFramework
{
    public partial class MonoBehaviourSimplify : MonoBehaviour
    {  
        //原本是实现一些方法，在这里进行了this 拓展 

    }

    public abstract partial class MonoBehaviourSimplify 
    {
        List<MsgRecord> mMsgRecorder = new List<MsgRecord>();
        private class MsgRecord
        {
            private static readonly Stack<MsgRecord> mMsgRecordPool = new Stack<MsgRecord>();

            public static MsgRecord Allocate(string msgName, Action<object> onMsgReceived)
            {
                MsgRecord retMsgRecord = null;

                retMsgRecord = mMsgRecordPool.Count > 0 ? mMsgRecordPool.Pop() : new MsgRecord();

                retMsgRecord.Name = msgName;
                retMsgRecord.OnMsgReceived = onMsgReceived; 

                return retMsgRecord;
            }

            public void Recycle()
            {
                Name = null;
                OnMsgReceived = null;

                mMsgRecordPool.Push(this);
            }

            public string Name;

            public Action<object> OnMsgReceived;
        }
        protected void RegisterMsg(string msgName, Action<object> onMsgReceived)
        {
            MsgDispatcher.Register(msgName, onMsgReceived);

            mMsgRecorder.Add(MsgRecord.Allocate(msgName, onMsgReceived));
        }

        protected void UnRegisterMsg(string msgName)
        {
            var selectedRecords = mMsgRecorder.FindAll(recorder => recorder.Name == msgName);

            selectedRecords.ForEach(selectRecord =>
            {
                MsgDispatcher.UnRegister(selectRecord.Name, selectRecord.OnMsgReceived);
                mMsgRecorder.Remove(selectRecord);
                selectRecord.Recycle();
            });

            selectedRecords.Clear();
        } 
        protected void UnRegisterMsg(string msgName, Action<object> onMsgReceived)
        {
            var selectedRecords = mMsgRecorder.FindAll(recorder => recorder.Name == msgName && recorder.OnMsgReceived == onMsgReceived);

            selectedRecords.ForEach(selectRecord =>
            {
                MsgDispatcher.UnRegister(selectRecord.Name, selectRecord.OnMsgReceived);
                mMsgRecorder.Remove(selectRecord);
                selectRecord.Recycle();
            });

            selectedRecords.Clear();
        }

        protected void SendMsg(string msgName, object data)
        {
            MsgDispatcher.SendMsg(msgName, data);
        }

        private void OnDestroy()
        {
            OnBeforeDestroy();

            foreach (var msgRecord in mMsgRecorder)
            {
                MsgDispatcher.UnRegister(msgRecord.Name, msgRecord.OnMsgReceived);
                msgRecord.Recycle();
            }

            mMsgRecorder.Clear();
        }
        protected abstract void OnBeforeDestroy();
    } 
    #region 例子
    public class MonoExample : MonoBehaviourSimplify
    {  
        private void Start()
        {
            gameObject.Show();
            gameObject.Hide();
            transform.Identity();
            this.Delay(2.0f,()=>{

            });
            RegisterMsg("Do", DoSomething);  
            SendMsg("Do","hello");
           
        }
        void DoSomething(object data)
        { 
            Debug.LogFormat("Received Do msg:{0}", data);
            UnRegisterMsg("Do");
        }
        protected override void OnBeforeDestroy()
        {

        }
    }
    #endregion
}
