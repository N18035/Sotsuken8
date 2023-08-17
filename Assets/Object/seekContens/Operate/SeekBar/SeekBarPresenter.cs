using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken.Main.SeekBar
{
    [RequireComponent(typeof(Slider))]
    public class SeekBarPresenter : MonoBehaviour
    {
        Slider slider;
        [SerializeField] AudioControl _audioController;
        [SerializeField] Music _music;
        [SerializeField] AudioSource _audio;

        //曲の長さ(秒)updateで使うからキャッシュ
        float musicLength;

        void Start()
        {
            slider = this.GetComponent<Slider>();
            
            this.UpdateAsObservable()
            .Where(_ => _audio.clip == null)
            .Subscribe(_ => slider.value = 0)
            .AddTo(this);

            //曲が停止しているときのシーク対応
            _audioController.OnSeek
            .Where(_ => _audio.clip != null)
            .Subscribe(_=> slider.value = _audio.time)
            .AddTo(this);

            //曲が停止しているときのスライダー操作
            slider.onValueChanged.AsObservable()
            .Throttle(TimeSpan.FromMilliseconds(100))
            .Where(_ => !_audio.isPlaying && _audio.clip !=null)
            .Subscribe(t =>{
                 _audioController.Seek(t);
                 //FIXMEなぜかここじゃないと動かない
                 _music.LoadTiming();
            })
            .AddTo(this);
        }        

        void Update(){
            //楽曲停止時は止めないとスライダー操作が出来ない
            if(_audio.isPlaying){
                slider.value = _audio.time;
            }
        }

        //疑似Startで使用。初期化
        public void ReadySeekBar(){
            var length = _audio.clip.length;
            slider.maxValue = length;
            musicLength = length;

            //初期化
            slider.value = 0;
        }
    }
}

