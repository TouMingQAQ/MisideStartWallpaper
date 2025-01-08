using System;
using UnityEngine;
using FMOD;
using FMODUnity;
using Debug = UnityEngine.Debug;

public class FMODAudioCapture : MonoBehaviour
{
    private FMOD.System fmodSystem;
    private FMOD.ChannelGroup masterChannelGroup;
    private FMOD.DSP fftDSP;
    private const int FFT_WINDOW_SIZE = 512; // 窗口大小
    public  float[][] spectrum;

    void Start()
    {
        // 初始化 FMOD 系统
        fmodSystem = RuntimeManager.CoreSystem;
        // 获取主通道组
        fmodSystem.getMasterChannelGroup(out masterChannelGroup);

        // 创建 FFT DSP
        fmodSystem.createDSPByType(FMOD.DSP_TYPE.FFT, out fftDSP);

        // 配置 FFT DSP 参数（可选）
        fftDSP.setParameterInt((int)FMOD.DSP_FFT.WINDOWSIZE, FFT_WINDOW_SIZE);

        // 添加到主通道组
        masterChannelGroup.addDSP(FMOD.CHANNELCONTROL_DSP_INDEX.TAIL, fftDSP);
    }

    void Update()
    {
        // 获取 FFT 数据
        float[][] spectrum;
        GetSpectrumData(out spectrum);

        if (spectrum != null && spectrum.Length > 0)
        {
            // 打印第一个频段的值
            Debug.Log("Spectrum Value: " + spectrum[0][0]);
        }
    }

    void GetSpectrumData(out float[][] spectrum)
    {
        spectrum = null;

        IntPtr data;
        uint length;
        fftDSP.getParameterData((int)FMOD.DSP_FFT.SPECTRUMDATA, out data, out length);

        if (data != IntPtr.Zero)
        {
            // 将数据转换为托管结构
            FMOD.DSP_PARAMETER_FFT fft = (FMOD.DSP_PARAMETER_FFT)System.Runtime.InteropServices.Marshal.PtrToStructure(data, typeof(FMOD.DSP_PARAMETER_FFT));
            spectrum = fft.spectrum;
        }
    }

    void OnDestroy()
    {
        fftDSP.release();
    }
}