using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQFramework;
using UnityEngine.UI; 
using UnityEngine.Video;
namespace TcpVideo
{
    public class ToPlayVideo : MonoBehaviour
    {
        public VideoPlayer videoPlayer;
        public Text textVideoTime;          // ��Ƶ�ĵ�ǰʱ�� Text
        public Text textVideoName;          // ��Ƶ����ʱ�� Text
        public Slider sliderVideoTime;      // ��Ƶ��ʱ�� Slider
        public Button btnPlayPause;       //������ͣ��ť
        public Button btnStop;            //ֹͣ���Ű�ť
        public Sprite[] spritePlayOrPause; // 0�ǲ��� 1����ͣ

        private bool isPlay = false;
        private int currentHour;
        private int currentMinute;
        private int currentSecond;
        private int clipHour;
        private int clipMinute;
        private int clipSecond;
          
        private void Start()
        {
            btnPlayPause.onClick.AddListener(PlayOrPause);
            btnStop.onClick.AddListener(StopVideo);
            btnPlayPause.image.sprite = spritePlayOrPause[0];

        }

        private void Update()
        {
            if (!isPlay) return; 
            ShowVideoTime();
        }

        /// <summary>
        /// ��ʾ��ǰ��Ƶ��ʱ��
        /// </summary>
        private void ShowVideoTime()
        {
            // ��ǰ����Ƶ����ʱ��
            currentHour = (int)videoPlayer.time / 3600;
            currentMinute = (int)(videoPlayer.time - currentHour * 3600) / 60;
            currentSecond = (int)(videoPlayer.time - currentHour * 3600 - currentMinute * 60);
            // �ѵ�ǰ��Ƶ���ŵ�ʱ����ʾ�� Text ��
            textVideoTime.text = string.Format("{0:D2}:{1:D2}:{2:D2}",
                currentHour, currentMinute, currentSecond);
            // �ѵ�ǰ��Ƶ���ŵ�ʱ�������ֵ�� Slider ��
            sliderVideoTime.value = (float)(videoPlayer.time / videoPlayer.clip.length);
        }
        /// <summary>
        /// ��Ƶ������ͣ
        /// </summary>
        void PlayOrPause()
        {
            //�����ƵƬ�β�Ϊ�գ�������Ƶ����û�в������
            if (videoPlayer.clip != null && (ulong)videoPlayer.frame < videoPlayer.frameCount)
            {

                sliderVideoTime.gameObject.SetActive(true);
                //�����Ƶ���ڲ���
                if (isPlay)
                {
                    btnPlayPause.image.sprite = spritePlayOrPause[1];
                    videoPlayer.Pause();
                    isPlay = false;
                }
                else
                {
                    btnPlayPause.image.sprite = spritePlayOrPause[0];
                    videoPlayer.Play();
                    isPlay = true;
                }
            }
        }
        void StopVideo()
        {
            videoPlayer.Stop();
            btnPlayPause.image.sprite = spritePlayOrPause[0]; 
            sliderVideoTime.gameObject.SetActive(false);
            textVideoName.text = "";
            textVideoTime.text = "";
            isPlay = true;
        }

    }
}