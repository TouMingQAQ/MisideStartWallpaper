using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Profiling;
using VInspector;
using UniTask = Cysharp.Threading.Tasks.UniTask;

namespace TFramework.Music
{
    public class UnityMusicGroup : MusicPlayGroup<UnityMusic,UnityMusicInfo>
    {
        [SerializeField]
        private string MusicPath = "Music";

        [SerializeField]
        private string defaultMusicName = "Null";

        public string DefaultMusicName => defaultMusicName;
        [SerializeField]
        private string defaultMusicWriter = "Null";
        public string DefaultMusicWriter => defaultMusicWriter;

        [SerializeField]
        private Sprite defaultMusicCover = null;
        public Sprite DefaultMusicCover => defaultMusicCover;
        
        [Button]
        public async UniTask LoadMusicAsync()
        {
            infoGroup.Clear();
            string directoryPath = Path.Combine(Application.streamingAssetsPath, MusicPath);
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
                Debug.Log($"创建音乐文件夹：{directoryPath}");
                return;
            }

            var mp3Files = directoryInfo.GetFiles("*.mp3", SearchOption.AllDirectories);
            var wavFiles = directoryInfo.GetFiles("*.wav", SearchOption.AllDirectories);
            var oggFiles = directoryInfo.GetFiles("*.ogg", SearchOption.AllDirectories);

            List<(FileInfo, AudioType)> audioInfoList = new();
            // 遍历加载所有音频【纯同步FileStream，无异步、无协程】
            foreach (FileInfo fileInfo in mp3Files)
                audioInfoList.Add((fileInfo,AudioType.MPEG));
            foreach (FileInfo fileInfo in wavFiles)
                audioInfoList.Add((fileInfo,AudioType.WAV));
            foreach (FileInfo fileInfo in oggFiles)
                audioInfoList.Add((fileInfo,AudioType.OGGVORBIS));
            foreach (var (fileInfo,audioType) in audioInfoList)
            {
                var url = fileInfo.FullName;
                var info = await LoadAudioWebRequest(url,audioType);
                if(info == null)
                    continue;
                await GetTag(fileInfo, info);
                Debug.Log("<color=green>LoadMusic</color> : "+info.MusicName);
                await UniTask.WaitForEndOfFrame();
                infoGroup.Add(info);
            }

            async UniTask GetTag(FileInfo fileInfo, UnityMusicInfo musicInfo)
            {
                Profiler.BeginSample("GetTag");
                var audioFile = TagLib.File.Create(fileInfo.FullName);
                Profiler.EndSample();
                var tag = audioFile.Tag;
                if(tag == null)
                    return;
                musicInfo.MusicName = string.IsNullOrEmpty(tag.Title) ? musicInfo.MusicName : tag.Title;
                musicInfo.MusicWriter = string.IsNullOrEmpty(tag.FirstPerformer) ? musicInfo.MusicWriter : tag.FirstPerformer;
                musicInfo.MusicAlbum =  string.IsNullOrEmpty(tag.Album) ? musicInfo.MusicAlbum : tag.Album;
                musicInfo.clip.name = musicInfo.MusicName;
                if (tag.Pictures != null && tag.Pictures.Length > 0)
                {
                    var pic = tag.Pictures[0];
                    if (pic != null && pic.Data != null && pic.Data.Data != null)
                    {
                        var imageArray = pic.Data.Data;
                        Texture2D texture = new Texture2D(0, 0);
                        Profiler.BeginSample("LoadImage");
                        texture.LoadImage(imageArray);
                        Profiler.EndSample();
                        musicInfo.MusicCover = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                            new Vector2(0.5f, 0.5f));
                        musicInfo.MusicCover.name = musicInfo.MusicName;
                    }
                }
             
            }
        }
        

        private async Cysharp.Threading.Tasks.UniTask<UnityMusicInfo> LoadAudioWebRequest(string url, AudioType type)
        {
            //fw玩意，还不如老东西好用
            // UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(url, type);
            // await request.SendWebRequest();
            // if (request.result == UnityWebRequest.Result.Success)
            // {
            //     //Todo:巨大的加载消耗，需要寻找方式优化
            //     var clip = DownloadHandlerAudioClip.GetContent(request);
            //     
            //     // await UniTask.RunOnThreadPool(() =>
            //     // {
            //     // });
            //     var info = new UnityMusicInfo()
            //     {
            //         clip = clip,
            //         MusicName = defaultMusicName,
            //         MusicCover = defaultMusicCover,
            //         MusicWriter = defaultMusicWriter
            //     };
            //     return info;
            // }
            // else
            // {
            //     Debug.LogError("Load Audio error :" + url);
            //     return null;
            // }
            WWW ww = new WWW(url);
            while (!ww.isDone)
            {
                await UniTask.WaitForEndOfFrame();
            }
            //GetAudioClipCompressed发力了
            var clip = ww.GetAudioClipCompressed();
            var info = new UnityMusicInfo()
            {
                clip = clip,
                MusicName = defaultMusicName,
                MusicCover = defaultMusicCover,
                MusicWriter = defaultMusicWriter
            };
            return info;
        }
    }

}