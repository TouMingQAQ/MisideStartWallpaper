// using UnityEngine;
// using System.IO;
// using System;
//
// public static class NAudioPlay
// {
//     public static AudioClip FromByteArray(byte[] fileData, string url)
//     {
//         string extension = Path.GetExtension(url).ToLower();
//         AudioClip audioClip = null;
//
//         if (extension == ".wav")
//         {
//             audioClip = DecodeWav(fileData);
//         }
//         else if (extension == ".mp3")
//         {
//             audioClip = DecodeMp3(fileData);
//         }
//         else if (extension == ".ogg")
//         {
//             audioClip = DecodeOgg(fileData);
//         }
//         else
//         {
//             throw new Exception($"不支持的音频格式: {extension}");
//         }
//
//         return audioClip;
//     }
//
//     #region 各格式解码实现
//     private static AudioClip DecodeWav(byte[] data)
//     {
//         MemoryStream ms = new MemoryStream(data);
//         BinaryReader br = new BinaryReader(ms);
//         int chunkId = br.ReadInt32();
//         int fileSize = br.ReadInt32();
//         int riffType = br.ReadInt32();
//         int fmtId = br.ReadInt32();
//         int fmtSize = br.ReadInt32();
//         short fmtCode = br.ReadInt16();
//         short channels = br.ReadInt16();
//         int sampleRate = br.ReadInt32();
//         int byteRate = br.ReadInt32();
//         short blockAlign = br.ReadInt16();
//         short bitDepth = br.ReadInt16();
//         int dataId = br.ReadInt32();
//         int dataSize = br.ReadInt32();
//
//         float[] samples = new float[dataSize / 2];
//         for (int i = 0; i < samples.Length; i++)
//         {
//             samples[i] = br.ReadInt16() / 32768f;
//         }
//
//         AudioClip clip = AudioClip.Create("WavClip", samples.Length, channels, sampleRate, false);
//         clip.SetData(samples, 0);
//         br.Close();
//         ms.Close();
//         return clip;
//     }
//
//     private static AudioClip DecodeMp3(byte[] data)
//     {
//         using (var ms = new MemoryStream(data))
//         using (var mp3Reader = new NAudio.Wave.Mp3FileReader(ms))
//         using (var stream = new NAudio.Wave.WaveToSampleProvider(mp3Reader))
//         {
//             var samples = new float[stream.WaveFormat.SampleRate * stream.WaveFormat.Channels];
//             stream.Read(samples, 0, samples.Length);
//             AudioClip clip = AudioClip.Create("Mp3Clip", samples.Length, stream.WaveFormat.Channels, stream.WaveFormat.SampleRate, false);
//             clip.SetData(samples, 0);
//             return clip;
//         }
//     }
//
//     private static AudioClip DecodeOgg(byte[] data)
//     {
//         using (var ms = new MemoryStream(data))
//         using (var oggReader = new NAudio.Vorbis.VorbisWaveReader(ms))
//         using (var stream = new NAudio.Wave.WaveToSampleProvider(oggReader))
//         {
//             var samples = new float[stream.WaveFormat.SampleRate * stream.WaveFormat.Channels];
//             stream.Read(samples, 0, samples.Length);
//             AudioClip clip = AudioClip.Create("OggClip", samples.Length, stream.WaveFormat.Channels, stream.WaveFormat.SampleRate, false);
//             clip.SetData(samples, 0);
//             return clip;
//         }
//     }
//     #endregion
// }