using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQFramework;
using UnityEngine.UI; 
using UnityEngine.Video;
namespace TcpVideo
{
    using UniRx;
    public class ToPlayVideo : MonoBehaviour
    {
        public string Name;
        public Text textDialog; 

        public VideoPlayer videoPlayer;
        public Text textVideoTime;          // 视频的当前时间 Text
        public Text textVideoName;          // 视频的总时长 Text
        public Slider sliderVideoTime;      // 视频的时间 Slider
        public Button btnPlayPause;       //播放暂停按钮
        public Button btnStop;            //停止播放按钮
        public Sprite[] spritePlayOrPause; // 0是暂停 1是播放

        public bool is01 = false;

        public BoolReactiveProperty isPlay = new BoolReactiveProperty(true);
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
            btnPlayPause.image.sprite = spritePlayOrPause[1];
            ShowVideoLength();
            textVideoTime.text = "";
            isPlay.Skip(1).Subscribe(_=> 
            {
                Debug.Log(isPlay.Value);
                if (_)
                {
                    btnPlayPause.image.sprite = spritePlayOrPause[1];
                    videoPlayer.Pause();

                    MainControl.Instance().SendMsg(Name + TcpOrder.orderPause);
                }
                else
                { 
                    btnPlayPause.image.sprite = spritePlayOrPause[0];
                    Debug.Log("Play");
                    videoPlayer.Play();
                    MainControl.Instance().SendMsg(Name + TcpOrder.orderPlay);
                }

            });
            if (is01)
            {
                MainControl.Instance().tcpServer.CallTcpSuccess01 += () => {
                    ThreadHelperTool.QueueOnMainThread(() => {
                        SetDialog(true);
                    });
                };
            }
            else
            {
                MainControl.Instance().tcpServer.CallTcpSuccess02 += () => {
                    ThreadHelperTool.QueueOnMainThread(() => {
                        SetDialog(true);
                    });
                };
            }

       

            //Observable.NextFrame().Subscribe((_) => {
            //}); ;

            videoPlayer.frame = 1;
            SetDialog(false);
        }




        private void Update()
        {
            if (isPlay.Value) return; 
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
            Debug.Log((ulong)videoPlayer.frame);
            Debug.Log(videoPlayer.frameCount);

            if (videoPlayer.clip != null )
            {
                textVideoName.gameObject.Show();
                sliderVideoTime.gameObject.SetActive(true);
                isPlay.Value = !isPlay.Value; 
            }
        }
        void StopVideo()
        {
            videoPlayer.Stop();
            btnPlayPause.image.sprite = spritePlayOrPause[0]; 
            sliderVideoTime.gameObject.SetActive(false);
            textVideoName.gameObject.Hide() ;
            textVideoTime.text = "";
            isPlay.Value = true;
            MainControl.Instance().SendMsg(Name + TcpOrder.orderStop);
        }

        public void SetDialog(bool b)
        {
            if (b)
            {
                textDialog.text = Name + "监听成功";
            }
            else
            {
                textDialog.text = Name + "等待监听";

            }
        }



    }
}