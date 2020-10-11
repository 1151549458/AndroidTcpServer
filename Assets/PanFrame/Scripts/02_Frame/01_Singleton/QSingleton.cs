/**
 *Copyright(C) 2018 by DefaultCompany
 *All rights reserved.
 *FileName:     QSingleton.cs
 *Author:       Pan
 *Version:      1.0
 *UnityVersion��2018.4.13f1
 *Date:         2020-05-18
 *Description:   
 *History:
*/
 
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace SQFramework
{
    public interface ISingleton
    {
        void OnSingletonInit();
    }
    public class Singleton<T> : ISingleton where T : Singleton<T>
    {
        protected static T mInstance = null;
        static object mLock = new object(); 
        public static T Instance
        {
            get
            {
                lock (mLock)
                {
                    if (mInstance == null)
                    {
                        mInstance = SingletonCreator.CreateSingleton<T>();
                    }
                } 
                return mInstance;
            }
        }
        public virtual void Dispose()
        {
            mInstance = null;
        }
        public void OnSingletonInit()
        { 
        }
    }
    public static class SingletonCreator
    {
        public static T CreateSingleton<T>() where T : class, ISingleton
        {
            // 获取私有构造函数
            var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

            // 获取无参构造函数
            var ctor = System.Array.Find(ctors, c => c.GetParameters().Length == 0);

            if (ctor == null)
            {
                throw new System.Exception("Non-Public Constructor() not found! in " + typeof(T));
            }

            // 通过构造函数，常见实例
            var retInstance = ctor.Invoke(null) as T;
            retInstance.OnSingletonInit();

            return retInstance;
        }
    }  
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        protected static T instance = null;

        public static T Instance()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (FindObjectsOfType<T>().Length > 1)
                {
                    Debug.LogError("More than 1!");
                    return instance;
                }

                if (instance == null)
                {
                    string instanceName = typeof(T).Name;
                    Debug.Log("Instance Name: " + instanceName);
                    GameObject instanceGO = GameObject.Find(instanceName);

                    if (instanceGO == null)
                        instanceGO = new GameObject(instanceName);
                    instance = instanceGO.AddComponent<T>();
                    DontDestroyOnLoad(instanceGO);  //保证实例不会被释放
                    Debug.Log("Add New Singleton " + instance.name + " in Game!");
                }
                else
                {
                    Debug.Log("Already exist: " + instance.name);
                }
            } 
            return instance;
        }
         
        protected virtual void OnDestroy()
        {
            instance = null;
        }
    }
 
}
