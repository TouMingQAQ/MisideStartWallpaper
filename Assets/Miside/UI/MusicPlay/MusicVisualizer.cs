using System;
using System.Collections.Generic;
using TFramework.Music;
using UnityEngine;
using UnityEngine.UI;

public class MusicVisualizer : MonoBehaviour
{
    //Color
    [SerializeField]
    private Color fromColor;
    [SerializeField]
    private Color toColor;
    
    //Component
    [SerializeField]
    private UnityMusicVisualizer visualizer;
    private List<Image> visualizerImages =new();

    [SerializeField]
    private Image prefab;

    private void Awake()
    {
        visualizerImages.Clear();
        var count = visualizer.GetDataCount();
        for (int i = 0; i < count; i++)
        {
            var newImage = Instantiate<Image>(prefab, transform);
            visualizerImages.Add(newImage);
        }
    }

    private void Update()
    {
        for (int i = 0,count = visualizerImages.Count; i < count; i++)
        {
            var image = visualizerImages[i];
            var data = visualizer.GetSpectrumValue(i);
            if (data > 1)
            {
                data = 1 + Mathf.Atan(data);
            }
            else
            {
                data = Mathf.Clamp01(data);
            }
            
            
            
            var color = Color.Lerp(fromColor, toColor, data);
            var scale =  image.transform.localScale;
            scale.x = data;
            image.transform.localScale = scale;
            image.color = color;
        }
    }
}
