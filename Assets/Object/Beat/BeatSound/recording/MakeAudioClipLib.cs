using UnityEngine;
using System;
using System.Collections.Generic;

public class MakeAudioClipLib : MonoBehaviour
{
    int frequency = 44100; //サンプリング周波数
    //===============================

    //float[]からClipを作ります
    public AudioClip CombineClip(float[] combinedClipData){
        AudioClip combinedClip = AudioClip.Create("CombinedClip", combinedClipData.Length, 1, frequency, false);
        combinedClip.SetData(combinedClipData, 0);
        return combinedClip;
    }

    //delaySliderManagerから引用
    //dataからfloat[]を作る
    public float[] CombineFromData(DelayData data,AudioClip mainBGM, AudioClip beat){
        //絶対に1つはdelayがあって、2つに分離される
        //最初の無音、左の区間
        float[] combinedClipData = MakeSilence(data.GetTime(0));
        Debug.Log("さいしょ:"+data.GetTime(0));

        Debug.Log(data.GetCount());
        //もし、2つ以上あれば処理
        if(data.GetCount() >= 2){
            for(int i=0;i<data.GetCount()-1;i++){
                float time = data.GetTime(i+1) - data.GetTime(i);
                float[] secondClipData = MakeSample(data.GetBPM(i), time, beat);
                combinedClipData = ConcatenateArrays(combinedClipData,secondClipData);
                Debug.Log("つぎi:"+data.GetTime(i));
            }
        }

        //終わりまでを生成
        float finaltime = mainBGM.length - data.GetTime(data.GetCount()-1);
        float[] finalClipData = MakeSample(data.GetBPM(data.GetCount()-1), finaltime, beat);
        combinedClipData = ConcatenateArrays(combinedClipData,finalClipData);

        return combinedClipData;
    }


    //========================private

    private float[] MakeSilence(float lengthInSeconds){
    int lengthInSamples = (int)(lengthInSeconds * frequency);
    float[] samples = new float[lengthInSamples];

    return samples;
    }

    /// <param name="BPM">BPM</param>  
    /// <param name="duration">長さ(秒数)</param>  
    /// <param name="beat">beatSoundの音</param>  
    float[] MakeSample(int BPM,float duration,AudioClip beat) {
        int sampleCount = (int)(frequency * duration); //音声データのサンプル数

        float[] samples = new float[sampleCount]; //音声データ用の配列

        beat.GetData(samples, 0); //既存のAudioClipから音声データを取得

        float beatInterval = 60f / (float)BPM; //1拍の長さ(秒)
        int beatIntervalSampleCount = (int)(beatInterval * frequency); //1拍の長さのサンプル数

        for (int i = 0; i < sampleCount; i++) {
            //音声データを切り出し
            samples[i] = samples[i % beatIntervalSampleCount];
        }

        return samples;
    }


    // ============汎用
    //結合する
    static float[] ConcatenateArrays(float[] array1, float[] array2)
    {
        int length1 = array1.Length;
        int length2 = array2.Length;
        float[] result = new float[length1 + length2];
        Array.Copy(array1, 0, result, 0, length1);
        Array.Copy(array2, 0, result, length1, length2);
        return result;
    }
}
