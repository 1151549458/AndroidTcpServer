using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQFramework;
using UnityEngine.EventSystems;

namespace TcpVideo
{
    public class SliderEvent : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        public ToPlayVideo toPlayVideo;        // 视频播放的脚本

        private void Start()
        {

        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            toPlayVideo.videoPlayer.Pause();
            toPlayVideo.isPlay.Value = true;
        }


        /// <summary>
        /// 给 Slider 添加开始拖拽事件
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData eventData)
        {
            SetVideoTimeValueChange();

        }

        /// <summary>
        /// 当前的 Slider 比例值转换为当前的视频播放时间
        /// </summary>
        private void SetVideoTimeValueChange()
        {
            toPlayVideo.videoPlayer.time = toPlayVideo.sliderVideoTime.value * toPlayVideo.videoPlayer.clip.length;
        }

        /// <summary>
        /// 给 Slider 添加结束拖拽事件
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
         
            toPlayVideo.isPlay.Value = false;
            toPlayVideo.videoPlayer.Play();
            MainControl.Instance().SendMsg(toPlayVideo.Name + TcpOrder.orderDrag + toPlayVideo.sliderVideoTime.value);
            //向客户端发送事件 

        }

     
    }
}