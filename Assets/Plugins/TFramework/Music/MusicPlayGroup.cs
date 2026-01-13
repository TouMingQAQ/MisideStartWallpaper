using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Debug = Console.Command.Debug;
using Random = UnityEngine.Random;

namespace TFramework.Music
{
    public enum MusicPlayLoop
    {
        /// <summary>
        /// 单曲循环
        /// </summary>
        Loop,
        /// <summary>
        /// 列表循环
        /// </summary>
        Continue,
        /// <summary>
        /// 随机下一首
        /// </summary>
        Random
    }
    public abstract class MusicPlayGroup<T,TInfo> : MonoBehaviour where TInfo : MusicInfo where T : MusicPlay<TInfo>
    {
        public UnityEvent<TInfo> onMusicChange;

        [SerializeField]
        protected int currentIndex = 0;
        [SerializeField]
        protected MusicPlayLoop playLoop;
        public MusicPlayLoop PlayLoop
        {
            get => playLoop;
            set => playLoop = value;
        }

        public int CurrentIndex => currentIndex;
        [SerializeField]
        protected T play;

        public T MusicPlay => play;
        [SerializeField]
        protected List<TInfo> infoGroup = new();

        public List<TInfo> MusicInfoList => infoGroup;
        /// <summary>
        /// 下一首
        /// </summary>
        public void Next()
        {
            Play(currentIndex+1);
        }
        /// <summary>
        /// 上一首
        /// </summary>
        public void Last()
        {
            Play(currentIndex-1);
        }

        public void LoadGroup(IEnumerable<TInfo> group, bool autoPlay = true)
        {
            infoGroup.Clear();
            infoGroup.AddRange(group);
            currentIndex = 0;
            if (autoPlay)
            {
                Play(currentIndex);
            }
            else
            {
                onMusicChange?.Invoke(infoGroup[currentIndex]);
                Pause();
            }
        }

        public void Play(int index)
        {
            if(infoGroup.Count <= 0)
                return;
            if (index < 0)
                index = infoGroup.Count - 1;
            if (index >= infoGroup.Count)
                index = 0;
            var info = infoGroup[index];
            if (info == null)
            {
                Debug.LogError("PlayInfo is Null");
                return;
            }

            currentIndex = index;
            play.LoadMusic(info);
            onMusicChange?.Invoke(infoGroup[currentIndex]);
            Play();
        }

        public virtual void Play() => play.Play();
        public virtual void Pause() => play.Pause();
        public virtual void ReStart() => play.ReStart();
        public virtual void SwitchMute() => play.SwitchMute();

        public virtual bool IsPlaying => play.IsPlaying;
        public virtual bool IsMute => play.Mute;
        

        public virtual void PlayPause()
        {
            if(play.IsPlaying)
                play.Pause();
            else
                play.Play();
        }

        public virtual string MusicName => play.MusicName;
        public virtual string MusicWriter => play.MusicWriter;
        public virtual Sprite MusicCover => play.MusicCover;
        public virtual string MusicAlbum => play.MusicAlbum;
        
        public virtual float Progress
        {
            get => play.PlayProgress;
            set => play.PlayProgress = value;
        }

        public virtual float Volume
        {
            get => play.Volume;
            set => play.Volume = value;
        }

        public float CurrentTime => play.CurrentTime;
        public float TotalTime => play.TotalTime;
        public string CurrentTimeStr => CoverTime(CurrentTime);
        public string TotalTimeStr => CoverTime(TotalTime);
        public string TimeStr => $"{CurrentTimeStr}/{TotalTimeStr}";

        protected virtual void Awake()
        {
            play.onEnd.AddListener(OnPlayEnd);
        }

        void OnPlayEnd()
        {
            if(IsEmpty)
                return;
            switch (playLoop)
            {
                case MusicPlayLoop.Loop:
                    Play(currentIndex);
                    break;
                case MusicPlayLoop.Continue:
                    Next();
                    break;
                case MusicPlayLoop.Random:
                    var rIndex = Random.Range(0, infoGroup.Count);
                    Play(rIndex);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public virtual bool IsEmpty=>infoGroup.Count <= 0;

        protected string CoverTime(float second)=>$"{Mathf.FloorToInt(second / 60):00}:{Mathf.FloorToInt(second % 60):00}";
    }
}

