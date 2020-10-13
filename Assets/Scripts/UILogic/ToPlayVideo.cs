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


        public VideoPlayer videoPlayer;
        public Text textVideoTime;          // ��Ƶ�ĵ�ǰʱ�� Text
        public Text textVideoName;          // ��Ƶ����ʱ�� Text
        public Slider sliderVideoTime;      // ��Ƶ��ʱ�� Slider
        public Button btnPlayPause;       //������ͣ��ť
        public Button btnStop;            //ֹͣ���Ű�ť
        public Sprite[] spritePlayOrPause; // 0�ǲ��� 1����ͣ

        private BoolReactiveProperty isPlay = new BoolReactiveProperty(false);
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

            isPlay.Skip(1).Subscribe(_=> 
            {
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
            this.Delay(0.5f,()=> {
                 isPlay.Value = true;

            });
            //Observable.NextFrame().Subscribe((_) => {
            //}); ;


        }




        private void Update()
        {
            if (isPlay.Value) return; 
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
        /// ��ʾ��Ƶ����ʱ��
        /// </summary>
        /// <param name="videos">��ǰ��Ƶ</param>
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
        /// ��Ƶ������ͣ
        /// </summary>
        void PlayOrPause()
        {
            //�����ƵƬ�β�Ϊ�գ�������Ƶ����û�в������
            Debug.Log((ulong)videoPlayer.frame);
            Debug.Log(videoPlayer.frameCount);

            if (videoPlayer.clip != null && (ulong)videoPlayer.frame < videoPlayer.frameCount)
            {

                sliderVideoTime.gameObject.SetActive(true);
                isPlay.Value = !isPlay.Value; 
            }
        }
        void StopVideo()
        {
            videoPlayer.Stop();
            btnPlayPause.image.sprite = spritePlayOrPause[0]; 
            sliderVideoTime.gameObject.SetActive(false);
            textVideoName.text = "";
            textVideoTime.text = "";
            isPlay.Value = true;
            MainControl.Instance().SendMsg(Name + TcpOrder.orderStop);
        }

    }
}