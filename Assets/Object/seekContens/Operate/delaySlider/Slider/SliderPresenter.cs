using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System;
using Ken.Setting;
using Sirenix.OdinInspector;//SerializedMonoBehaviourを使うのに必要

namespace Ken.Delay
{
    public class SliderPresenter : MonoBehaviour
    {
        [SerializeField] DelaySliderManager delaySliderManager;
        [SerializeField] CountPresenter count;
        [SerializeField] TimePresenter time;
        [SerializeField] AudioSource _audio;

        private Slider thisSlider;

        // [BoxGroup("データ")][ReadOnly]
        [SerializeField]int bpm;
        // int BPM;
        public int BPM => bpm;

        // [BoxGroup("データ")][ReadOnly]
        // [SerializeField] int id;
        int id;
        public int ID => id;
        public void SetID(int id)
        {
            this.id = id;
        }

        public void SetBPM(int bpm)
        {
            this.bpm = bpm;
        }
        
        
        //初期化
        public void Ready(){
            thisSlider = this.gameObject.GetComponent<Slider>();
            thisSlider.maxValue = _audio.clip.length;
            thisSlider.value = 0;
        }

        void Start()
        {
            thisSlider = this.gameObject.GetComponent<Slider>();

            thisSlider.onValueChanged.AsObservable()
            .Where(_ => _audio.clip != null)
            .Throttle(TimeSpan.FromMilliseconds(100))
            .Subscribe(t => {
                delaySliderManager.CheckBatting();//被りがあれば警告
                count.PublicValidate();
                time.ChangeTime();
            })
            .AddTo(this);

            thisSlider.onValueChanged.AsObservable()
            .Where(_ => _audio.clip == null)
            .Subscribe(t => thisSlider.value = 0)
            .AddTo(this);
        }
    }
}

