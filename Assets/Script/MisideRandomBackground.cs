using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UniTask = Cysharp.Threading.Tasks.UniTask;

public class MisideRandomBackground : MonoBehaviour
{
    [SerializeField]
    private RawImage diyBackgroundImage;
    [SerializeField]
    private RectTransform diyBackgroundRect;
    [SerializeField]
    private string path = "RandomBack";

    [SerializeField]
    private List<Texture2D> imageList;

    [SerializeField]
    private float changeTime = 360;
    private float changeTimer;
    private async void Awake()
    {
        imageList.Clear();
        var directory = Path.Combine(Application.streamingAssetsPath, path);
        DirectoryInfo directoryInfo = new DirectoryInfo(directory);
        if(!directoryInfo.Exists)
            directoryInfo.Create();
        var files = directoryInfo.GetFiles("*.png", SearchOption.AllDirectories);
        foreach (var fileInfo in files)
        {
            var t = await GetTexture2D(fileInfo.FullName);
            if(t == null)
                continue;
            imageList.Add(t);
        }

        changeTimer = changeTime;
        if(imageList.Count <= 0)
            return;
        RandomImage();
    }

    private void Update()
    {
        if (imageList.Count <= 0)
        {
            diyBackgroundImage.gameObject.SetActive(false);
            return;
        }
        if(changeTimer <= 0)
            RandomImage();
        else
        {
            changeTimer -= Time.deltaTime;
        }
    }

    void RandomImage()
    {
        changeTimer = changeTime;
        var rindex = Random.Range(0, imageList.Count);
        var image = imageList[rindex];
        diyBackgroundImage.gameObject.SetActive(false);
        if(image == null)
            return;
        diyBackgroundImage.gameObject.SetActive(true);
        diyBackgroundImage.texture = image;
        float height = 1080;
        var width = (float)image.width/ image.height  * height ; 
        diyBackgroundRect.sizeDelta = new Vector2(width, height);
        diyBackgroundRect.anchoredPosition = new Vector2(0, 0);
    }

    async Cysharp.Threading.Tasks.UniTask<Texture2D> GetTexture2D(string filePath)
    {
        var req = UnityWebRequestTexture.GetTexture(filePath);
        await req.SendWebRequest();
        if (req.result == UnityWebRequest.Result.Success)
        {
            var texture = DownloadHandlerTexture.GetContent(req);
            return texture;
        }
        else
        {
            return null;
        }
    }
}
