using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQFramework;
using UnityEngine.EventSystems;

namespace TcpVideo
{
    public class SliderEvent : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        public ToPlayVideo toPlayVideo;        // ��Ƶ���ŵĽű�

        private void Start()
        {

        }

        /// <summary>
        /// �� Slider ��ӿ�ʼ��ק�¼�
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData eventData)
        {
            toPlayVideo.videoPlayer.Pause();
            SetVideoTimeValueChange();
        }

        /// <summary>
        /// ��ǰ�� Slider ����ֵת��Ϊ��ǰ����Ƶ����ʱ��
        /// </summary>
        private void SetVideoTimeValueChange()
        {
            toPlayVideo.videoPlayer.time = toPlayVideo.sliderVideoTime.value * toPlayVideo.videoPlayer.clip.length;
        }

        /// <summary>
        /// �� Slider ��ӽ�����ק�¼�
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            toPlayVideo.videoPlayer.Play();

            //��ͻ��˷����¼� 

        }

    }
}