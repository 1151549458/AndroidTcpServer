/**
 *Copyright(C) 2018 by DefaultCompany
 *All rights reserved.
 *FileName:     TransformSimplify.cs
 *Author:       Pan
 *Version:      1.0
 *UnityVersion��2018.4.13f1
 *Date:         2020-05-18
 *Description:   
 *History:
*/using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SQFramework
{
    public static partial class TransformSimplify
    {

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="transform"></param>
        public static void Identity(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
        }
        public static void SetLocalPosX(this Transform transform, float x)
        {
            var localPos = transform.localPosition;
            localPos.x = x;
            transform.localPosition = localPos;
        }

        public static void SetLocalPosY(Transform transform, float y)
        {
            var localPos = transform.localPosition;
            localPos.y = y;
            transform.localPosition = localPos;
        }

        public static void SetLocalPosZ(Transform transform, float z)
        {
            var localPos = transform.localPosition;
            localPos.z = z;
            transform.localPosition = localPos;
        }

        public static void SetLocalPosXY(Transform transform, float x, float y)
        {
            var localPos = transform.localPosition;
            localPos.x = x;
            localPos.y = y;
            transform.localPosition = localPos;
        }

        public static void SetLocalPosXZ(Transform transform, float x, float z)
        {
            var localPos = transform.localPosition;
            localPos.x = x;
            localPos.z = z;
            transform.localPosition = localPos;
        }

        public static void SetLocalPosYZ(Transform transform, float y, float z)
        {
            var localPos = transform.localPosition;
            localPos.y = y;
            localPos.z = z;
            transform.localPosition = localPos;
        } 

    }

    public static partial class TransformSimplify
    {
        public static void AddChild( Transform transform, Transform childTrans)
        {
            childTrans.SetParent(transform);
        }
        public static void SetScale(this Transform trans, bool isActive)
        {
            if (trans != null)
            {
                if (isActive)
                {
                    trans.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    trans.localScale = new Vector3(0, 0, 0);
                }
            }
        }
        public static void SetScale(this GameObject go, bool isActive)
        {
            if (go != null)
            {
                if (isActive)
                {
                    go.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    go.transform.localScale = new Vector3(0, 0, 0);
                }
            }
        }
    }
}

