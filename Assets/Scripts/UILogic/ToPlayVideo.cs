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
        public Text textVideoTime;          // 视频的当前时间 Text
        public Text textVideoName;          // 视频的总时长 Text
        public Slider sliderVideoTime;      // 视频的时间 Slider
        public Button btnPlayPause;       //播放暂停按钮
        public Button btnStop;            //停止播放按钮
        public Sprite[] spritePlayOrPause; // 0是播放 1是暂停

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
            ShowVideoLength();
        }

        private void Update()
        {
            if (!isPlay) return; 
            ShowVideoTime();
        }

        /// <summary>
        /// 显示当前视频的时间
        /// </summary>
        private void ShowVideoTime()
        {
            // 当前的视频播放时间
            currentHour = (int)videoPlayer.time / 3600;
            currentMinute = (int)(videoPlayer.time - currentHour * 3600) / 60;
            currentSecond = (int)(videoPlayer.time - currentHour * 3600 - currentMinute * 60);
            // 把当前视频播放的时间显示在 Text 上
            textVideoTime.text = string.Format("{0:D2}:{1:D2}:{2:D2}",
                currentHour, currentMinute, currentSecond);
            // 把当前视频播放的时间比例赋值到 Slider 上
            sliderVideoTime.value = (float)(videoPlayer.time / videoPlayer.clip.length);
        }

        /// <summary>
        /// 显示视频的总时长
        /// </summary>
        /// <param name="videos">当前视频</param>
        void ShowVideoLength(VideoClip videos)
        {
            videoPlayer.clip = videos;
            videoPlayer.Play();
            sliderVideoTime.gameObject.SetActive(true);
            clipHour = (int)videoPlayer.clip.length / 3600;
            clipMinute = (int)(videoPlayer.clip.length - clipHour * 3600) / 60;
            clipSecond = (int)(videoPlayer.clip.length - clipHour * 3600 - clipMinute * 60);
            textVideoName.text = string.Format("{0:D2}:{1:D2}:{2:D2} ",
                 clipHour, clipMinute, clipSecond);
        }
        void ShowVideoLength()
        {
      
            videoPlayer.Play();
            sliderVideoTime.gameObject.SetActive(true);
            clipHour = (int)videoPlayer.clip.length / 3600;
            clipMinute = (int)(videoPlayer.clip.length - clipHour * 3600) / 60;
            clipSecond = (int)(videoPlayer.clip.length - clipHour * 3600 - clipMinute * 60);
            textVideoName.text = string.Format("{0:D2}:{1:D2}:{2:D2} ",
                 clipHour, clipMinute, clipSecond);
        }
        /// <summary>
        /// 视频播放暂停
        /// </summary>
        void PlayOrPause()
        {
            //如果视频片段不为空，并且视频画面没有播放完毕
            if (videoPlayer.clip != null && (ulong)videoPlayer.frame < videoPlayer.frameCount)
            {

                sliderVideoTime.gameObject.SetActive(true);
                //如果视频正在播放
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