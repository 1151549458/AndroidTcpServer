/** 
 *Copyright(C) 2018 by #COMPANY# 
 *All rights reserved. 
 *FileName:     #SCRIPTFULLNAME# 
 *Author:       #AUTHOR# 
 *Version:      #VERSION# 
 *UnityVersion：#UNITYVERSION# 
 *Date:         #DATE# 
 *Description:    
 *History: 
*/


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SQFramework
{

    [RequireComponent(typeof(AudioSource))]
    public class AudioTrigger : MonoBehaviour
    {

        private static AudioTrigger instance;

        private AudioSource _audiosource;
        private Coroutine coroutine;

        public static AudioTrigger Instance
        {
            get
            {
                return instance;
            }
        }

        private void Start()
        {
            _audiosource = GetComponent<AudioSource>();
            instance = this;
        }

        public void PlayAudioByClip(AudioClip clip, Action playDoneAction = null)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            _audiosource.PlayOneShot(clip);
            if (playDoneAction != null)
            {
                coroutine = StartCoroutine(PlayingAudio(playDoneAction));
            }
        } 
        private IEnumerator PlayingAudio(Action action)
        {
            while (_audiosource.isPlaying)
            {
                yield return new WaitForSeconds(Time.deltaTime);
            }
            action();
        }

        public void PlayAudioByClip(AudioClip clip)
        {
            if (clip != null)
            {
                _audiosource.clip = clip;
                _audiosource.Play();
            }
        }
        public void StopAudioClip()
        {
            if (_audiosource.isPlaying)
            {
                _audiosource.Stop();
            }

        }






    }
}
