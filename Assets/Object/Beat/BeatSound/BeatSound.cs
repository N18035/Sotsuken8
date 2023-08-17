using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ken.Beat{
    public class BeatSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _SESource;
        [SerializeField] private AudioSource _SESource2;
        [SerializeField] AudioSource _audio;
        [SerializeField] Music music;

        BeatSoundData data;

        void Update()
        {
            if(!_audio.isPlaying) return;
            if(data.Number==2) return;

            if(Music.IsJustChangedBeat()){
                if(Music.Just.Beat%music.myTempo==0){
                    if(data.Is1Up){
                        // _SESource.pitch = 1.3f;
                        _SESource2.Play();   
                    }else{
                        // _SESource.pitch =1f;
                        _SESource.Play();
                    }
                }else{
                    if(!data.IsOnly1){
                    // _SESource.pitch =1f;
                    _SESource.Play();  
                    } 
                }                 
            }
        }

        public void SetBeatSoundSetting(BeatSoundData d){
            data = d;
        } 
        #region 保留
        void PlayBeatSound(){
            // if(type==1){//音の変更アリ
            //     if(Music.IsJustChangedBeat()){
            //         // if(Music.Just.Beat%4==3)    Music.QuantizePlay(Click,1);//ボツ
            //         if(dd3or4.value==1){//3はく
            //             if(Music.Just.Beat%4==2)    Clickupkey.Play();
            //             else    Click.Play();
            //         }else if(dd3or4.value==0){//4はく
            //             if(Music.Just.Beat%4==3)    Clickupkey.Play();
            //             else    Click.Play();
            //         }
            //     }
            // }else if(type==2){//拍手
            //     if(Music.IsJustChangedBeat())   Clap.Play();
            // }else if(type==3){//声 //3はく未対応
            //     if(dd3or4.value==1){//3はく
            //     if(Music.Just.Beat%3==0)   two.Play();
            //     else if(Music.Just.Beat%3==1)   three.Play();
            //     else if(Music.Just.Beat%3==2)   one.Play();
            //     }else if(dd3or4.value==0){//4はく
            //     if(Music.Just.Beat%4==0)   two.Play();
            //     else if(Music.Just.Beat%4==1)   three.Play();
            //     else if(Music.Just.Beat%4==2)   four.Play();
            //     else if(Music.Just.Beat%4==3)   one.Play();
            //     }
            // }else if(type==4){//無音
            //     //無音
            // }else if(type==0){//一定
            //     if(Music.IsJustChangedBeat())   Click.Play();
            // }
        }
    #endregion   
    }

    public struct BeatSoundData{
        public int Number { get; }
        public bool Is1Up { get; }
        public bool IsOnly1 { get; }

        public BeatSoundData(int n, bool isup, bool isonly1) { Number = n; Is1Up = isup; IsOnly1=isonly1; }
    }
}
