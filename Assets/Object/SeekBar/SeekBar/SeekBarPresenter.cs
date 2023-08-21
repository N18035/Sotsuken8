using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken
{
    public class SeekBarPresenter : MonoBehaviour
    {
        [SerializeField] Slider _slider;
        [SerializeField] AudioControlPresenter _audioController;
        [SerializeField] Music _music;
        [SerializeField] AudioSource _audio;

        //曲の長さ(秒)updateで使うからキャッシュ
        float musicLength;

        void Start()
        {
            this.UpdateAsObservable()
            .Where(_ => _audio.clip == null)
            .Subscribe(_ => _slider.value = 0)
            .AddTo(this);

            //曲が停止しているときのシーク対応
            _audioController.OnSeek
            .Where(_ => _audio.clip != null)
            .Subscribe(_=> _slider.value = _audio.time)
            .AddTo(this);

            //曲が停止しているときのスライダー操作
            //変更されてはじめて値が更新されてしまう
            _slider.onValueChanged.AsObservable()
            .ThrottleFirst(TimeSpan.FromMilliseconds(80))//連続の読み込みを防止
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
                _slider.value = _audio.time;
            }
        }

        //疑似Startで使用。初期化
        public void ReadySeekBar(){
            var length = _audio.clip.length;
            _slider.maxValue = length;
            musicLength = length;

            //初期化
            _slider.value = 0;
        }
    }
}

