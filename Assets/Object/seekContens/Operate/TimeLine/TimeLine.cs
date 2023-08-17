using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Ken.Main
{
    public class TLData{
        //FIXME必ず誤差の手動修正が必要です。
        public int MaxDisplayBar;
        //仮に36.5秒の曲なら0,1...36,37(ダミー)で38個の箱が必要
        public float AllBeat;
        public int SecondCount;
        public float[] ScalePositionsBar;
        public float[] ScalePositionsSecond;

        public void Resize(float allBeat,int maxSecond){
            AllBeat = allBeat;
            MaxDisplayBar = (int)allBeat+1;
            SecondCount = maxSecond;
            Array.Resize(ref ScalePositionsBar,MaxDisplayBar);
            Array.Resize(ref ScalePositionsSecond,SecondCount);
        }

        public void SetPosition(float[] array,float[] arraySecond){
            Array.Copy(array,ScalePositionsBar,array.Length);
            Array.Copy(arraySecond,ScalePositionsSecond,arraySecond.Length);
        }

    }

    public class TimeLine : MonoBehaviour
    {
        //TODO小節単位だから秒とか動的に変更可能にする

        [SerializeField] Music music;
        [SerializeField] AudioSource Audio;
        [SerializeField] Ken.Zoom.ZoomModel _zoomController;
        [SerializeField] Content _contentZoom;
        [SerializeField] TimeLineView _timeLineView;

        TLData sendData;


        void Start(){
            _zoomController.ZoomLevel
            .SkipLatestValueOnSubscribe()
            .Subscribe(_ => Resize())
            .AddTo(this);
        }

        public void ReadyTimeLine(){
            sendData = new TLData();
            ResizeArray();
            CalcPosition();
            _timeLineView.ReadyTimeLineView(sendData);
        }


        //リサイズ
        //ズレの調整は完全に数字見て微調整
        private void Resize(){
            sendData = new TLData();
            //FIXME この２つが無いと正常に動作しない生焼けオブジェクトになってる
            ResizeArray();
            CalcPosition();

            //見た目
            _timeLineView.ResizeTimeLineView(sendData);
        }


        void ResizeArray(){
            //メモリの箱の数を用意する
            float AllBeat = GetMaxBarAndBeat();
            int maxSecond = GetMaxSecond();
            sendData.Resize(AllBeat,maxSecond);

            //displayScaleに合わせて返す値を変更するメソッド
            //FIXME今は小節単位だから、全てのbeat単位にする
            float GetMaxBarAndBeat(){
                //「60(1分)÷BPM(テンポ)で４分音符一拍分の長さ」(s)
                //例) 60 / 120 = 0.5s
                float oneBeat = 60f / music.myTempo;

                //1分の曲の場合60s / 0.5s = 120beatになる
                return Audio.clip.length / oneBeat;
            }

            ////とりあえず再生時間(秒数)を返す。
            int GetMaxSecond(){
                //0のぶん1足す。
                return (int)Audio.clip.length+1;
            }
        }

        //座標計算
        void CalcPosition(){
            //FIXME 抽象化出来る

            //キャッシュ
            var zoomLevel = _zoomController.ZoomLevel.Value;

            //誤差修正。余白を設けてるので、誤差は絶対に発生する
            //FIXMEハードコーディングになってます
            float fixL = (1942.94f + KenConst._originStart*5 ) /4 * (zoomLevel-1); 
            float fixR = (1948f - KenConst._originalEnd*5 ) /4 * (zoomLevel-1);            
            
            float left = KenConst._originStart * zoomLevel - fixL;
            float right = KenConst._originalEnd * zoomLevel +fixR;
            
            var length = Mathf.Abs(left)+right;

            //準備ーーーーーーーーーーーーーーーーーーーー

            //Bar版
            float[] scalePositions = sendData.ScalePositionsBar;

            scalePositions[0]=left;
            
            float between = length/sendData.AllBeat;

            //左から順番に入れていく
            for(int j=1;j<scalePositions.Length;j++){
                scalePositions[j] = scalePositions[j-1] + between;
            }


            #region  秒数版を作成
            float[] scalePositions2 = sendData.ScalePositionsSecond;

            scalePositions2[0]=left;
            
            between = length/Audio.clip.length;

            //左から順番に入れていく
            for(int j=1;j<scalePositions2.Length;j++){
                scalePositions2[j] = scalePositions2[j-1] + between;
            }
            #endregion

            sendData.SetPosition(scalePositions,scalePositions2);
        }
    }
}
