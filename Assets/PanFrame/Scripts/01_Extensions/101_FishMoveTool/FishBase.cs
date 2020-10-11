/**
 *Copyright(C) 2018 by DefaultCompany
 *All rights reserved.
 *FileName:     FishBase.cs
 *Author:       Pan
 *Version:      1.0
 *UnityVersion：2017.2.0f3
 *Date:         2019-03-12
 *Description:   
 *History:
*/
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
namespace SQFramework
{
    public class FishBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Vector3 rangeR;
        public Vector3 rangeE;
        public string fishTag;
        public float speed = 1;
        public float rotaSpeed = 30;
        public enum FISH_STATE
        {
            FISH_NONE,    //无敌状态
            FISH_NORMAL,  //正常
            FISH_SCARE,   //惊吓
            FISH_DIE      //被吊住
        }
        public FISH_STATE fish_state = FISH_STATE.FISH_NORMAL;
        private float level = 0;
        public float Level
        {
            get { return level; }
            set
            {
                level = value;
            }
        }
        void Awake()
        {
        }
        public virtual void OnPointerEnter(PointerEventData eventData)
        {

        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {

        }


    }
}
