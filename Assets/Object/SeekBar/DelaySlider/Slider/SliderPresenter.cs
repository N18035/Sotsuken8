using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System;
using Sirenix.OdinInspector;//SerializedMonoBehaviourを使うのに必要

namespace Ken.Delay
{
    public class SliderPresenter : MonoBehaviour
    {
        [SerializeField] DelaySliderManager delaySliderManager;
        [SerializeField] CountPresenter count;
        [SerializeField] TimePresenter time;
        [SerializeField] AudioSource _audio;
        [SerializeField] Slider thisSlider;
        
        public int BPM;
        public int ID;

        //初期化
        public void Ready(){
            thisSlider.maxValue = _audio.clip.length;
            thisSlider.value = 0;
        }

        void Start()
        {
            thisSlider.onValueChanged.AsObservable()
            .Where(_ => _audio.clip != null)
            .ThrottleFirst(TimeSpan.FromMilliseconds(100))
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

