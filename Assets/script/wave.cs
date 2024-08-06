using UnityEngine;
using System;
using System.IO;

public static class WavUtility
{
    public static byte[] FromAudioClip(AudioClip clip)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            WriteWavFileHeader(memoryStream, clip);
            float[] samples = new float[clip.samples];
            clip.GetData(samples, 0);
            WriteWavFileData(memoryStream, samples);
            return memoryStream.ToArray();
        }
    }

    private static void WriteWavFileHeader(MemoryStream stream, AudioClip clip)
    {
        int sampleCount = clip.samples * clip.channels;
        int byteRate = clip.frequency * clip.channels * 2;

        stream.Write(new byte[44], 0, 44);

        stream.Seek(0, SeekOrigin.Begin);
        stream.Write(System.Text.Encoding.UTF8.GetBytes("RIFF"), 0, 4);
        stream.Write(BitConverter.GetBytes(36 + sampleCount * 2), 0, 4);
        stream.Write(System.Text.Encoding.UTF8.GetBytes("WAVE"), 0, 4);

        stream.Write(System.Text.Encoding.UTF8.GetBytes("fmt "), 0, 4);
        stream.Write(BitConverter.GetBytes(16), 0, 4);
        stream.Write(BitConverter.GetBytes((short)1), 0, 2);
        stream.Write(BitConverter.GetBytes((short)clip.channels), 0, 2);
        stream.Write(BitConverter.GetBytes(clip.frequency), 0, 4);
        stream.Write(BitConverter.GetBytes(byteRate), 0, 4);
        stream.Write(BitConverter.GetBytes((short)(clip.channels * 2)), 0, 2);
        stream.Write(BitConverter.GetBytes((short)16), 0, 2);

        stream.Write(System.Text.Encoding.UTF8.GetBytes("data"), 0, 4);
        stream.Write(BitConverter.GetBytes(sampleCount * 2), 0, 4);
    }

    private static void WriteWavFileData(MemoryStream stream, float[] samples)
    {
        for (int i = 0; i < samples.Length; i++)
        {
            short sample = (short)(samples[i] * short.MaxValue);
            stream.Write(BitConverter.GetBytes(sample), 0, 2);
        }
    }
}