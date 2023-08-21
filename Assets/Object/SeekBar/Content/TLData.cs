using System;


public class TLData{
    //FIXME必ず誤差の手動修正が必要です。
    
    public int MaxDisplayBar;
    //仮に36.5秒の曲なら0,1...36,37(ダミー)で38個の箱が必要
    public float AllBeat;
    public int SecondCount;
    public float[] ScalePositionsBar;
    public float[] ScalePositionsSecond;

    //秒とビートに合わせて最大配列数を調整
    public void Resize(float allBeat,int maxSecond){
        AllBeat = allBeat;
        MaxDisplayBar = (int)allBeat+1;
        SecondCount = maxSecond;
        Array.Resize(ref ScalePositionsBar,MaxDisplayBar);
        Array.Resize(ref ScalePositionsSecond,SecondCount);
    }

    //座標を入れる
    public void SetPosition(float[] array,float[] arraySecond){
        Array.Copy(array,ScalePositionsBar,array.Length);
        Array.Copy(arraySecond,ScalePositionsSecond,arraySecond.Length);
    }

}