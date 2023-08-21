using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class DelayData
{
    [SerializeField] private List<TB> tb;

    public DelayData(List<float> t, List<int> b){
        tb = new List<TB>(){};

        for(int i=0;i<t.Count;i++){
            var tmp = new TB(t[i],b[i]);
            tb.Add(tmp);
        }

        //ソート
        CompareLogic comp = new CompareLogic();
        tb.Sort(comp);
    }

    public int GetCount(){
        return tb.Count;
    }

    public float GetTime(int i){
        return tb[i].Time;
    }

    public int GetBPM(int i){
        return (int)tb[i].BPM;
    }

}


[System.Serializable]
public struct TB
{
    [SerializeField] public float Time;
    [SerializeField] public float BPM;

    public TB (float t, float bpm){
        Time = t;
        BPM = bpm;
    }
}