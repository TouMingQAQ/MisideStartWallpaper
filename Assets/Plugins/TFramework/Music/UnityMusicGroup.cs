using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using VInspector;

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
        public async void LoadMusic()
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
            
            

            // 遍历加载所有音频【纯同步FileStream，无异步、无协程】
            foreach (FileInfo fileInfo in mp3Files)
            {
                var url = fileInfo.FullName;
                var info = await LoadAudioWebRequest(url, AudioType.MPEG);
                if(info == null)
                    continue;
                GetTag(fileInfo, info);
                infoGroup.Add(info);
            }
            foreach (FileInfo fileInfo in wavFiles)
            {
                var url = fileInfo.FullName;
                var info = await LoadAudioWebRequest(url, AudioType.WAV);
                if(info == null)
                    continue;
                GetTag(fileInfo, info);
                infoGroup.Add(info);
            }
            foreach (FileInfo fileInfo in oggFiles)
            {
                var url = fileInfo.FullName;
                var info = await LoadAudioWebRequest(url, AudioType.OGGVORBIS);
                if(info == null)
                    continue;
                GetTag(fileInfo, info);
                infoGroup.Add(info);
            }

            void GetTag(FileInfo fileInfo, UnityMusicInfo musicInfo)
            {
                var audioFile = TagLib.File.Create(fileInfo.FullName);
                var tag = audioFile.Tag;
                if(tag == null)
                    return;
                musicInfo.MusicName = string.IsNullOrEmpty(tag.Title) ? musicInfo.MusicName : tag.Title;
                musicInfo.MusicWriter = string.IsNullOrEmpty(tag.FirstPerformer) ? musicInfo.MusicWriter : tag.FirstPerformer;
                musicInfo.MusicAlbum =  string.IsNullOrEmpty(tag.Album) ? musicInfo.MusicAlbum : tag.Album;
                if (tag.Pictures != null && tag.Pictures.Length > 0)
                {
                    var pic = tag.Pictures[0];
                    if (pic != null && pic.Data != null && pic.Data.Data != null)
                    {
                        var imageArray = pic.Data.Data;
                        Texture2D texture = new Texture2D(0, 0);
                        texture.LoadImage(imageArray);
                        musicInfo.MusicCover = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                            new Vector2(0.5f, 0.5f));
                    }
                }
             
            }
        }
        

        private async Cysharp.Threading.Tasks.UniTask<UnityMusicInfo> LoadAudioWebRequest(string url, AudioType type)
        {
            UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(url, type);
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                var clip = DownloadHandlerAudioClip.GetContent(request);
                var info = new UnityMusicInfo()
                {
                    clip = clip,
                    MusicName = defaultMusicName,
                    MusicCover = defaultMusicCover,
                    MusicWriter = defaultMusicWriter
                };
                return info;
            }
            else
            {
                Debug.LogError("Load Audio error :" + url);
                return null;
            }
        }
    
    }

}