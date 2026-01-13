using System;
using UnityEngine;
using UnityEngine.Events;

namespace TFramework.Music
{
    public abstract class MusicInfo
    {
        public string MusicName;
        public string MusicWriter;
        public string MusicAlbum;
        public Sprite MusicCover;
    }
    public abstract class MusicPlay<TInfo> : MonoBehaviour where TInfo : MusicInfo
    {
        public UnityEvent onPlay;
        public UnityEvent onPause;
        public UnityEvent onEnd;
        public virtual TInfo MusicInfo { get; set; }
        /// <summary>
        /// 播放进度[0~1]
        /// </summary>
        public virtual float PlayProgress {
            get
            {
                if (TotalTime <= 0)
                    return 0;
                return CurrentTime / TotalTime;
            }
            set => SetProgress(value);
        }



        public abstract bool IsPlaying { get; }
        public abstract bool IsEnd { get; }

        /// <summary>
        /// 当前播放时间
        /// </summary>
        public abstract float CurrentTime { get; }

        /// <summary>
        /// 总时间
        /// </summary>
        public abstract float TotalTime { get; }


        /// <summary>
        /// 音乐名称
        /// </summary>
        public string MusicName => MusicInfo?.MusicName;
        /// <summary>
        /// 音乐作者
        /// </summary>
        public string MusicWriter => MusicInfo?.MusicWriter;

        
        public string MusicAlbum => MusicInfo?.MusicAlbum;

        /// <summary>
        /// 音乐头像
        /// </summary>
        public Sprite MusicCover => MusicInfo?.MusicCover;
        /// <summary>
        /// 当前音量
        /// </summary>
        protected float currentVolume = 0.6f;
        /// <summary>
        /// 音量
        /// </summary>
        public virtual float Volume
        {
            get => currentVolume;
            set
            {
                currentVolume = value;
                SetVolume(Mute?0:currentVolume);
            } 
        }


        protected bool mute = false;
        /// <summary>
        /// 是否静音
        /// </summary>
        public virtual bool Mute { 
            get=>mute;
            set
            {
                mute = value;
                SetVolume(Mute?0:currentVolume);
            }
        }
        protected abstract void SetProgress(float progress);
        protected abstract void SetVolume(float volume);

        public void LoadMusic(TInfo info)
        {
            Pause();
            MusicInfo = info;
            OnLoadMusic(info);
            SetProgress(0);
           
        }

        protected abstract void OnLoadMusic(TInfo info);
        /// <summary>
        /// 播放
        /// </summary>
        public abstract void Play();
        /// <summary>
        /// 暂停
        /// </summary>
        public abstract void Pause();

        /// <summary>
        /// 重新播放
        /// </summary>
        public virtual void ReStart()
        {
            Pause();
            SetProgress(0);
            Play();
        }

        public abstract void ClearPlay();

        public virtual void SwitchMute()
        {
            Mute = !Mute;
        }

        private bool isPlaying = false;
        protected virtual void Update()
        {
            if (isPlaying != IsPlaying)
            {
                isPlaying = IsPlaying;
                if(IsPlaying)
                    onPlay?.Invoke();
                else
                {
                    if (IsEnd)
                        onEnd?.Invoke();
                    else
                        onPause?.Invoke();
                }
            }

           
        }
    }
}

