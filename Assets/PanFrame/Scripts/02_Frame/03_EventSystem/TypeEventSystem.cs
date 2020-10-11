/**
 *Copyright(C) 2018 by DefaultCompany
 *All rights reserved.
 *FileName:     TypeEventSystem.cs
 *Author:       Pan
 *Version:      1.0
 *UnityVersion��2018.4.13f1
 *Date:         2020-05-19
 *Description:   
 *History:
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SQFramework {
  
    public class TypeEventSystem
    {
        /// <summary>
        /// 接口 只负责存储在字典中
        /// </summary>
        interface IRegisterations
        { 
        } 
        /// <summary>
        /// 多个注册
        /// </summary>
        class Registerations<T> : IRegisterations
        {
            /// <summary>
            /// 不需要 List<Action<T>> 了
            /// 因为委托本身就可以一对多注册
            /// </summary>
            public Action<T> OnReceives = obj => { };
        }

        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<Type, IRegisterations> mTypeEventDict = new Dictionary<Type, IRegisterations>();

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="onReceive"></param>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>(System.Action<T> onReceive)
        {
            var type = typeof(T);

            IRegisterations registerations = null;

            if (mTypeEventDict.TryGetValue(type, out registerations))
            {
                var reg = registerations as Registerations<T>;
                reg.OnReceives += onReceive;
            }
            else
            {
                var reg = new Registerations<T>();
                reg.OnReceives += onReceive;
                mTypeEventDict.Add(type, reg);
            }
        }

        /// <summary>
        /// 注销事件
        /// </summary>
        /// <param name="onReceive"></param>
        /// <typeparam name="T"></typeparam>
        public static void UnRegister<T>(System.Action<T> onReceive)
        {
            var type = typeof(T);

            IRegisterations registerations = null;

            if (mTypeEventDict.TryGetValue(type, out registerations))
            {
                var reg = registerations as Registerations<T>;
                reg.OnReceives -= onReceive;
            }
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="t"></param>
        /// <typeparam name="T"></typeparam>
        public static void Send<T>(T t)
        {
            var type = typeof(T);

            IRegisterations registerations = null;

            if (mTypeEventDict.TryGetValue(type, out registerations))
            {
                var reg = registerations as Registerations<T>;
                reg.OnReceives(t);
            }
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="t"></param>
        /// <typeparam name="T"></typeparam>
        public static void Send<T>() where T : new()
        {
            var type = typeof(T);

            IRegisterations registerations = null;

            if (mTypeEventDict.TryGetValue(type, out registerations))
            {
                var reg = registerations as Registerations<T>;
                reg.OnReceives(new T());
            }
        }
    }

    #region 例子
    public class GameStartEvent
    {
    } 
    public class GameOverEvent
    { // 可以携带参数
        public int Score;
    }
    public class TypeEventSystemExample : MonoBehaviour
    {
        private void Start()
        { 
            TypeEventSystem.Register<GameStartEvent>(OnGameStartEvent);
            TypeEventSystem.Register<GameOverEvent>(OnGameOverEvent);

            TypeEventSystem.Send<GameStartEvent>();
            TypeEventSystem.Send(new GameOverEvent()
            {
                Score = 100
            }); 
        } 
        private void OnGameOverEvent(GameOverEvent obj)
        {
            Debug.Log("分数" + obj.Score);
        } 
        private void OnGameStartEvent(GameStartEvent obj)
        {
            Debug.Log("Start");

        } 
        private void OnDestroy()
        {
            TypeEventSystem.UnRegister<GameStartEvent>(OnGameStartEvent);
            TypeEventSystem.UnRegister<GameOverEvent>(OnGameOverEvent);

        }
    }
    #endregion

}
