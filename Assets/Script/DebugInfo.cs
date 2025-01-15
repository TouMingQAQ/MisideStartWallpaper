using System;
using System.Text;
using CSCore.CoreAudioAPI;
using CSCore.SoundIn;
using CSCore.Streams;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugInfo : MonoBehaviour
{
    public Button debugButton;
    public TMP_Text debugText;
    public AudioAnimation audioAnimation;
    public bool CaptureStart;
    private WasapiLoopbackCapture capture;
    private SoundInSource soundInSource;
    private MMDeviceEnumerator _enumerator;
    private string audioDeviceName;
    private string audioDeviceID;
    private string error;
    private void Awake()
    {
        _enumerator = new MMDeviceEnumerator();
        OnDebug();
        debugButton.onClick.AddListener(OnDebug);
    }
    
    private void OnApplicationQuit()
    {
        _enumerator?.Dispose();
        soundInSource?.Dispose();
        capture?.Stop();
        capture?.Dispose();
    }

    private void Update()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<color=green>MusicHead</color>");
        sb.AppendLine(CaptureStart ? $"Capture:<color=green>On</color>" : $"Capture:<color=red>Off</color>");
        sb.AppendLine($"AudioDeviceName:{audioDeviceName}");
        sb.AppendLine($"AudioDeviceID:{audioDeviceID}");
        sb.AppendLine($"AudioEnergy:{audioAnimation.currentEnergy}");
        sb.AppendLine($"<color=red>{error}</color>");
        debugText.text = sb.ToString();
    }

    public void OnDebug()
    {
        var device = _enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
        audioDeviceName = device.FriendlyName;
        audioDeviceID = device.DeviceID;
        try
        {
            capture = new WasapiLoopbackCapture();
            capture.Initialize();
            capture.Start();
            soundInSource = new SoundInSource(capture);
            CaptureStart = true;
        }
        catch (Exception e)
        {
            error =e.Message;
            capture = null;
            soundInSource = null;
        }
    }
}
