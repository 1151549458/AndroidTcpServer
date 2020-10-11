/**
 *Copyright(C) 2018 by DefaultCompany
 *All rights reserved.
 *FileName:     Fsm.cs
 *Author:       Pan
 *Version:      1.0
 *UnityVersion��2018.4.13f1
 *Date:         2020-05-20
 *Description:   
 *History:
*/using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQFramework;
namespace ZMFramework
{
    #region 状态机接口
    public interface OnStateEnter
    {
        void OnEnter();
    }
    public interface OnStateUpdate
    {
        void OnUpdate();
    }
    public interface OnStateRepeat
    {
        void OnRepeat();
    }
    public interface OnStateExit
    {
        void OnExit();
    }
    #endregion
    #region 实现状态机抽象
    /// <summary>
    /// 基础状态机抽象类
    /// </summary>
    /// <typeparam name="T">其状态衍生类的状态类型枚举</typeparam>
    public abstract class BaseState<T> : OnStateEnter, OnStateUpdate, OnStateRepeat, OnStateExit
        where T : struct
    {
        public BaseState(UnityEngine.Object owner) { m_owner = owner; }
        protected T m_stateType;
        public T StateType { get { return m_stateType; } }
        protected UnityEngine.Object m_owner;
        public UnityEngine.Object Owner { get { return m_owner; } }
        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnRepeat() { }
        public virtual void OnExit() { }
    }
    #endregion

    /// <summary>
    /// 状态机父类
    /// </summary>
    /// <typeparam name="T">状态机中状态的直接父类</typeparam>
    /// <typeparam name="S">状态机中状态的类型枚举</typeparam>
    public class FSM<T, S> where T : BaseState<S>  where S : struct
    {
        /// <summary>
        /// 储存状态的字典
        /// </summary>
        private Dictionary<S, T> m_dic = new Dictionary<S, T>();
        /// <summary>
        /// 当前状态的类型
        /// </summary>
        private S m_currentTYPE;

        /// <summary>
        /// 根据状态类型，返回新建状态对象
        /// </summary>
        /// <param name="type">状态类型</param>
        /// <returns>状态对象</returns>
        protected virtual T GetState(S type)
        {
            return null;
        }

        /// <summary>
        /// 状态机中增加状态
        /// </summary>
        /// <param name="type">添加的状态</param>
        /// <returns>添加成功为true，已存在则添加失败为false</returns>
        public bool AddState(S type)
        {
            if (!m_dic.ContainsKey(type))
            {
                T state = GetState(type);
                if (state == null) return false;
                m_dic[type] = state;
                return true;
            }
            return false;
        } 
        /// <summary>
        /// 改变状态
        /// </summary>
        /// <param name="statetype">改变后的状态类型</param>
        /// <returns>转换状态成功返回true，状态不存在或已在该状态则转换状态失败返回false</returns>
        public bool ChangeState(S statetype)
        {
            if (m_dic.ContainsKey(statetype))
            {
                if (m_dic.ContainsKey(m_currentTYPE))
                {
                    if (m_dic[m_currentTYPE] == m_dic[statetype])
                    {
                        m_dic[m_currentTYPE].OnRepeat();
                        return false;
                    }
                    m_dic[m_currentTYPE].OnExit();
                }
                m_currentTYPE = statetype;
                m_dic[m_currentTYPE].OnEnter();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 状态Update
        /// </summary>
        public void UpdateState()
        {
            m_dic[m_currentTYPE].OnUpdate();
        }
    }
     
    /// <summary>
    /// 应用状态的类型
    /// </summary>
    public enum AppStateType
    {
        None,
        LoginAndCreate,
        RunAndRun,
        WaitAndWait
    } 
    /// <summary>
    /// 应用的状态父类
    /// </summary>
    public abstract class AppBaseState : BaseState<AppStateType>
    {
        public AppBaseState(MonoBehaviour owner, AppStateType type) : base(owner)
        {
            m_stateType = type;
            m_owner = owner;
            m_ownerobject = owner.gameObject;
        }
        protected new MonoBehaviour m_owner;
        protected GameObject m_ownerobject;
        public override void OnEnter()
        {
            base.OnEnter();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
        }
        public override void OnRepeat()
        {
            base.OnRepeat();
        }
        public override void OnExit()
        {
            base.OnExit();
        }
    }

    #region 子类
    public class App_LoginAndCreateState : AppBaseState
    {
        public App_LoginAndCreateState(MonoBehaviour owner) : base(owner, AppStateType.LoginAndCreate)
        {

        }
        public override void OnEnter()
        {
            base.OnEnter();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
        }
        public override void OnRepeat()
        {
            base.OnRepeat();
        }
        public override void OnExit()
        {
            base.OnExit();
        }
    } 

    public class App_RunAndRunState : AppBaseState
    {
        public App_RunAndRunState(MonoBehaviour owner) : base(owner, AppStateType.LoginAndCreate)
        {

        }
        public override void OnEnter()
        {
            base.OnEnter();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
        }
        public override void OnRepeat()
        {
            base.OnRepeat();
        }
        public override void OnExit()
        {
            base.OnExit();
        }
    }
    public class App_WaitAndWaitState : AppBaseState
    {
        public App_WaitAndWaitState(MonoBehaviour owner) : base(owner, AppStateType.LoginAndCreate)
        {

        }
        public override void OnEnter()
        {
            base.OnEnter();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
        }
        public override void OnRepeat()
        {
            base.OnRepeat();
        }
        public override void OnExit()
        {
            base.OnExit();
        }
    }
    #endregion

    /// <summary>
    /// 状态改变事件参数
    /// </summary>
    /// 

    //public class AppStateChangeEvent
    //{ // 可以携带参数
    //    public AppStateType type;
    //    public AppStateChangeEvent(AppStateType type)
    //    {
    //        this.type = type;
    //    }
    //}
    public class AppStateChangeEvent : EventArgs
    {
        public AppStateType type;
        public AppStateChangeEvent(AppStateType type)
        {
            this.type = type;
        }
    }
    /// <summary>
    /// 应用的状态机
    /// </summary>
    public class AppFSM : FSM<AppBaseState, AppStateType>
    {
        protected MonoBehaviour m_owner;

        public AppFSM(MonoBehaviour owner)
        {
            m_owner = owner;
            EventManager.Instance.AddEvent<AppStateChangeEvent>
                (owner.gameObject,
                (object sender, EventArgs e) =>
                {
                    AppStateChangeEvent sce = (AppStateChangeEvent)e;
                    ChangeState(sce.type);
                });
            UpdateManager.Instance.AddCallBack(UpdateState);
        }

        protected override AppBaseState GetState(AppStateType type)
        {
            switch (type)
            {
                case AppStateType.LoginAndCreate:
                    return new App_LoginAndCreateState(m_owner);
            }
            return base.GetState(type);
        }

        public static AppFSM Create(MonoBehaviour mono)
        {
            AppFSM fsm = new AppFSM(mono);
            fsm.AddState(AppStateType.LoginAndCreate);
            return fsm;
        }
    }




    /// <summary>
    /// 空返回值，无参数的委托
    /// </summary>
    public delegate void CallBack();
    /// <summary>
    /// 布尔返回值，无参数的委托
    /// </summary>
    public delegate bool CallBackForBool();
    /// <summary>
    /// 空返回值，float参数的委托
    /// </summary>
    public delegate void FloatCallBack(float number);

    /// <summary>
    /// 管理者父类
    /// </summary>
    public class BaseManager<T> : Singleton<T> where T : BaseManager<T>
    {
        protected BaseManager() { }
    } 
     
    public class EventManager : BaseManager<EventManager>
    {
        Dictionary<GameObject, Dictionary<Type, EventHandler>> m_dic = new Dictionary<GameObject, Dictionary<Type, EventHandler>>();

        /// <summary>
        /// 增加事件
        /// </summary>
        /// <typeparam name="T">事件的参数类</typeparam>
        /// <param name="owner">拥有对象</param>
        /// <param name="eh">事件</param>
        public void AddEvent<T>(GameObject owner, EventHandler eh) where T : EventArgs
        {
            if (!m_dic.ContainsKey(owner))
            {
                m_dic[owner] = new Dictionary<Type, EventHandler>();
            }
            Type type = typeof(T);
            if (m_dic[owner].ContainsKey(type))
            {
                m_dic[owner][type] += eh;
            }
            else
            {
                m_dic[owner][type] = eh;
            }
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <typeparam name="T">事件的参数类</typeparam>
        /// <param name="owner">拥有对象</param>
        /// <param name="sender">事件的参数</param>
        /// <param name="t">事件的参数</param>
        public void TriggerEvent<T>(GameObject owner, object sender, T t) where T : EventArgs
        {
            if (m_dic.ContainsKey(owner))
            {
                Type type = typeof(T);
                if (m_dic[owner].ContainsKey(type))
                {
                    m_dic[owner][type].Invoke(sender, t);
                }
            }
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <typeparam name="T">事件的参数类</typeparam>
        /// <param name="owner">拥有对象</param>
        /// <param name="eh">事件</param>
        public void RemoveEvent<T>(GameObject owner, EventHandler eh) where T : EventArgs
        {
            if (m_dic.ContainsKey(owner))
            {
                Type type = typeof(T);
                if (m_dic[owner].ContainsKey(type))
                {
                    m_dic[owner][type] -= eh;
                }
            }
        } 
    }

    public class UpdateManager : BaseManager<UpdateManager>
    {
        protected event CallBack update = null;

        /// <summary>
        /// 添加回调
        /// </summary>
        /// <param name="callback">添加的回调</param>
        public void AddCallBack(CallBack callback)
        {
            update += callback;
        }

        /// <summary>
        /// 运行回调
        /// </summary>
        public void RunCallBack()
        {
            if (null != update) update.Invoke();
        }

        /// <summary>
        /// 移除回调
        /// </summary>
        /// <param name="callback">移除的回调</param>
        public void RemoveCallBack(CallBack callback)
        {
            update -= callback;
        }

        /// <summary>
        /// 添加计时即结束事件
        /// </summary>
        /// <param name="timingtime">计时的时间，即多长时间后触发事件</param>
        /// <param name="events">事件委托</param>
        public void AddTimerEndEvents(float timingtime, CallBack events)
        {
            timingtime += Time.time;
            CallBack judge = () => { if (timingtime <= Time.time) events(); };
            events += () => { update -= judge; };//触发后及删除
            update += judge;
        }

        /// <summary>
        /// 添加计时循环事件
        /// </summary>
        /// <param name="timingtime">计时的时间，即多长时间后触发事件</param>
        /// <param name="events">事件委托</param>
        public void AddTimerUpdateEvents(float timingtime, CallBack events)
        {
            float temptime = Time.time + timingtime;
            CallBack judge = () => { if (temptime <= Time.time) events(); };
            events += () => { temptime = Time.time + timingtime; };//触发后再次进入计时
            update += judge;
        } 
    }



}
 
namespace SQFramework
{
    public enum EventQ
    {
        Start = 0,
        One = 1,
        Two =2,
        End = 3,
    }
    public class EnmuEventExample : MonoBehaviour
    {
        private void Start()
        {
            EventManager.Instance.Register((int)EventQ.One, ddd);
            EventManager.Instance.SendEvent((int)EventQ.One,"123",12); 
        }

        private void ddd(object[] param)
        {
            Debug.Log(param.Length);
        }
         
    }


    public delegate void EventMgr(params object[] param);


    public interface IEventMgr
    {
        void Register(int key, EventMgr eventMgr);//注册事件 
        void UnRegister(int key);//解绑事件 
        void ClearAll();//解绑所有事件 
        bool IsRegisterName(int key);//key值是否被注册 
        bool IsRegisterFunc(EventMgr eventMgr);//eventMgr是否被注册 
        void SendEvent(int key, params object[] param);//调用
    }
    public class EventManager :Singleton<EventManager>, IEventMgr
    {
        /// <summary>
        /// 存储注册好的事件
        /// </summary>
        protected readonly Dictionary<int, EventMgr> EventListerDict = new Dictionary<int, EventMgr>();

        /// <summary>
        /// 是否暂停所有的事件
        /// </summary>
        public bool IsPause = false;

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="eventMgr"></param>
        public void Register(int key, EventMgr eventMgr)
        {
            if (EventListerDict.ContainsKey(key))
            {
                Debug.LogError("Key:" + key + "已存在！");
            }
            else
            {
                EventListerDict.Add(key, eventMgr);
            }
        }

        /// <summary>
        /// 取消事件绑定
        /// </summary>
        /// <param name="key"></param>
        public void UnRegister(int key)
        {
            if (EventListerDict != null && EventListerDict.ContainsKey(key))
            {
                EventListerDict.Remove(key);
                Debug.Log("移除事件：" + key);
            }
            else
            {
                Debug.LogError("Key:" + key + "不存在！");
            }
        }

        /// <summary>
        /// 取消所有事件绑定
        /// </summary>
        public void ClearAll()
        {
            if (EventListerDict != null)
            {
                EventListerDict.Clear();
                Debug.Log("清空注册事件！");
            }
        }

        /// <summary>
        /// ID是否注册过
        /// </summary>
        /// <param name="key"></param>
        public bool IsRegisterName(int key)
        {
            if (EventListerDict != null && EventListerDict.ContainsKey(key))
            {
                EventListerDict.Remove(key);
                Debug.Log("事件：" + key + "已注册！");
                return true;
            }
            Debug.Log("事件：" + key + "未注册！");
            return false;
        }

        /// <summary>
        /// 方法是否注册过
        /// </summary>
        /// <param name="eventMgr"></param>
        public bool IsRegisterFunc(EventMgr eventMgr)
        {
            if (EventListerDict != null && EventListerDict.ContainsValue(eventMgr))
            {
                Debug.Log("事件已注册！");
                return true;
            }
            Debug.Log("事件未注册！");
            return false;
        }

        /// <summary>
        /// 调用事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="param"></param>
        public void SendEvent(int key, params object[] param)
        {
            if (!IsPause)
            {
                if (EventListerDict.ContainsKey(key))
                {
                    EventListerDict[key].Invoke(param);
                }
                else
                {
                    Debug.LogError("事件：" + key + "未注册！");
                }
            }
            else
            {
                Debug.LogError("所有事件已暂停！");
            }

        }
    }



}