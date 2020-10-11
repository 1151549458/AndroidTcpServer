/**
 *Copyright(C) 2018 by DefaultCompany
 *All rights reserved.
 *FileName:     GameObjectSimplify.cs
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
using UnityEngine.UI;

namespace SQFramework
{
    public static partial class GameObjectSimplify
    {
        #region SetActive 


        public static void Show(this GameObject gameObj)//this代表扩展
        {
            gameObj.SetActive(true);
        }
        public static void Hide(this GameObject gameObj)//this代表扩展
        {
            gameObj.SetActive(false);
        }
        public static void SetActive(this GameObject[] gos, bool b) //组显隐
        {
            for (int i = 0; i < gos.Length; i++)
            {
                gos[i].SetActive(b);
            }
        }
        public static void SetActive(this GameObject[] gos, int index, bool isActive) //组的ID显隐
        {
            for (int i = 0; i < gos.Length; i++)
            {
                if (i == index)
                {
                    gos[i].SetActive(isActive);
                }
                else
                {
                    gos[i].SetActive(!isActive);
                }
            }
        } 
        public static void Show(this Transform transform)
        {
            transform.gameObject.SetActive(true);
        }
        public static void Hide(this Transform transform)
        {
            transform.gameObject.SetActive(false);
        }
        public static void SetActive(this Transform[] transforms, bool isActive) //组显隐
        {
            for (int i = 0; i < transforms.Length; i++)
            {
                transforms[i].gameObject.SetActive(isActive);
            }
        }
        public static void SetActive(this Transform[] transforms, int index, bool isActive) //组的ID显隐
        {
            for (int i = 0; i < transforms.Length; i++)
            {
                if (i == index)
                {
                    transforms[i].gameObject.SetActive(isActive);
                }
                else
                {
                    transforms[i].gameObject.SetActive(!isActive);
                }
            }
        }
        public static void SetActive(this List<GameObject> listgos, bool isActive) //组显隐
        {
            for (int i = 0; i < listgos.Count; i++)
            {
                listgos[i].gameObject.SetActive(isActive);
            }
        }
        public static void SetActive(this List<GameObject> listgos, int index, bool isActive) //组的ID显隐
        {
            for (int i = 0; i < listgos.Count; i++)
            {
                if (i == index)
                {
                    listgos[i].SetActive(isActive);
                }
                else
                {
                    listgos[i].SetActive(!isActive);
                }
            }
        }

        public static void SetActive(this List<Transform> listTrans, bool isActive) //组显隐
        {
            for (int i = 0; i < listTrans.Count; i++)
            {
                listTrans[i].gameObject.SetActive(isActive);
            }
        }
        public static void SetActive(this List<Transform> listTrans, int index, bool isActive) //组的ID显隐
        {
            for (int i = 0; i < listTrans.Count; i++)
            {
                if (i == index)
                {
                    listTrans[i].gameObject.SetActive(isActive);
                }
                else
                {
                    listTrans[i].gameObject.SetActive(!isActive);
                }
            }
        }




        #endregion

        #region Delay
        public static void Delay(this MonoBehaviour mono, float seconds, Action onFinished)
        {
            mono.StartCoroutine(DelayCoroutine(seconds, onFinished));
        }

        private static IEnumerator DelayCoroutine(float seconds, Action onFinished)
        {
            yield return new WaitForSeconds(seconds);
            onFinished?.Invoke();
        }

        #endregion

        #region 判断物体是否相等
        public static bool GetGameObjectSame(this GameObject go, GameObject[] gos)
        {
            for (int i = 0; i < gos.Length; i++)
                if (gos[i] == go)
                    return true;
            return false;
        }
        #endregion

        #region Collider碰撞器

        public static void SetCollideEnable(this GameObject collider, bool isActive, bool isHasChild = false)
        {
            if (collider && collider.GetComponent<Collider>())
                collider.GetComponent<Collider>().enabled = isActive;

            if (isHasChild)
            {
                Collider[] child = collider.GetComponentsInChildren<Collider>();
                for (int i = 0; i < child.Length; i++)
                    child[i].enabled = isActive;
            }
        }
        /// <summary>
        /// 碰撞起是否开启
        /// </summary>
        /// <param name="collider"></param>
        /// <param name="isActive"></param>
        /// <param name="isHasChild"></param>
        public static void SetCollideEnable(GameObject[] collider, bool isActive, bool isHasChild = false)
        {
            for (int i = 0; i < collider.Length; i++)
                SetCollideEnable(collider[i], isActive, isHasChild);
        }
        /// <summary>
        /// 只开启这个数组中某个碰撞起
        /// </summary>
        /// <param name="collider"></param>
        /// <param name="index"></param>
        /// <param name="isHasChild"></param>
        public static void SetCollideEnable(this GameObject[] collider, int index, bool isHasChild = false)
        {
            for (int i = 0; i < collider.Length; i++)
                SetCollideEnable(collider[i], i == index, isHasChild);
        }
        #endregion
    }

    public static partial class GameObjectSimplify
    {
        #region 销毁对象
        /// <summary>
        /// 销毁对象
        /// </summary>
        /// <param name="go"></param>
        /// <param name="b">是否立即销毁</param>
        public static void SafeDestroy(this GameObject go,bool b)
        { 
            if (go != null)
            {
                if (b)
                {
                    UnityEngine.Object.DestroyImmediate(go);
                }
                else
                {
                    UnityEngine.Object.Destroy(go);
                }
            }
        } 

        /// <summary>
        /// 销毁对象 Transform版本
        /// </summary>
        /// <param name="go"></param>
        public static void SafeDestroy(this Transform go,bool b)
        {
            if (go != null)
            {
                SafeDestroy(go.gameObject,b);
            }
            else
            {
                ThrowError("将要销毁的 Transform 为空！");
            }
        }
      
        /// <summary>
        /// 抛出异常
        /// </summary>
        /// <param name="message"></param>
        public static void ThrowError(string message)
        {
            throw new ArgumentException(message);
        }
        #endregion

        #region 查找同级对象
        public static GameObject Peer(this GameObject go, string subnode)
        {
            return Peer(go.transform, subnode);
        }

        public static GameObject Peer(Transform go, string subnode)
        {
            Transform transform = go.parent.Find(subnode);
            if (transform == null)
            {
                return null;
            }
            return transform.gameObject;
        }
        #endregion

        #region 查找某个对象的子物体
        public static GameObject Child(this GameObject go, string subnode)
        {
            return Child(go.transform, subnode);
        }

        public static GameObject Child(Transform go, string subnode)
        {
            Transform transform = go.Find(subnode);
            if (transform == null)
            {
                return null;
            }
            return transform.gameObject;
        }
        #endregion


        /// <summary>
        /// 外部加载的图片适应屏幕尺寸
        /// </summary>
        /// <param name="go_Pic"></param> GameObject UI
        /// <param name="valueSize"></param>  比例 一般 1.0f
        public static void GetPixelSize(RawImage raw_Pic, float valueSize)   // gopic   让图片适应屏幕大小
        {
            float heightRate = raw_Pic.rectTransform.sizeDelta.y / (float)Display.displays[0].renderingHeight;
            float weightRate = raw_Pic.rectTransform.sizeDelta.x / (float)Display.displays[0].renderingWidth;
            raw_Pic.transform.localScale = Vector3.one / (heightRate >= weightRate ? heightRate : weightRate) * valueSize;

        }
    }
}