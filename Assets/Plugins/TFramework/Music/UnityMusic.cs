using System;
using UnityEngine;

namespace TFramework.Music
{
    [Serializable]
    public class UnityMusicInfo : MusicInfo
    {
        public AudioClip clip;
    }
    [RequireComponent(typeof(AudioSource))]
    public class UnityMusic : MusicPlay<UnityMusicInfo>
    {
       
        [SerializeField]
        private AudioSource audioSource;

        public AudioSource AudioSource => audioSource;
        public override bool IsPlaying => audioSource.isPlaying;
        public override bool IsEnd => TotalTime > 0 && (PlayProgress >= 0.99f);

        public override float CurrentTime => audioSource.time;
        public override float TotalTime
        {
            get
            {
                if (audioSource == null || audioSource.clip == null)
                    return 0;
                return audioSource.clip.length;
            }
        }

        public override bool Mute
        {
            get => audioSource.mute;
            set => audioSource.mute = value;
        }

        public override float Volume
        {
            get => currentVolume;
            set
            {
                currentVolume = value;
                SetVolume(currentVolume);
            } 
        }

        protected override void SetVolume(float volume)
        {
            audioSource.volume = volume;
        }
        protected override void SetProgress(float progress)
        {
            audioSource.time = TotalTime * progress;
        }

        protected override void OnLoadMusic(UnityMusicInfo info)
        {
            audioSource.clip = info.clip;
        }

        private void Awake()
        {
            audioSource.loop = false;
            audioSource.playOnAwake = false;
        }

        public override void Play()
        {
            audioSource.Play();
        }

        public override void Pause()
        {
            audioSource.Pause();
        }

        public override void ClearPlay()
        {
            audioSource.Pause();
            audioSource.time = 0;
            audioSource.clip = null;
        }

        private void Reset()
        {
            TryGetComponent(out audioSource);
        }
    }
}

