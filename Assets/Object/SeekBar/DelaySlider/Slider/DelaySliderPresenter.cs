using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace Ken
{
    public class DelaySliderPresenter : MonoBehaviour
    {
        DelaySliderManager delaySliderManager;
        DelayChangePointPresenter count;
        NowDelayTimeViewer time;
        AudioCheckPresenter audioCheck;
        [SerializeField] AudioSource _audio;

        [SerializeField] Slider thisSlider;
        
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

        void Start()
        {
            audioCheck = AudioCheckPresenter.I;
            time = NowDelayTimeViewer.I;
            count = DelayChangePointPresenter.I;
            delaySliderManager = DelaySliderManager.I;

            thisSlider.onValueChanged.AsObservable()
            .Where(_ => !audioCheck.ClipIsNull())
            .ThrottleFirst(TimeSpan.FromMilliseconds(100))
            .Subscribe(t => {
                delaySliderManager.CheckBatting();//被りがあれば警告
                count.PublicValidate();
                time.ChangeTime(t);
            })
            .AddTo(this);

            thisSlider.onValueChanged.AsObservable()
            .Where(_ => audioCheck.ClipIsNull())
            .Subscribe(t => thisSlider.value = 0)
            .AddTo(this);
        }

        //初期化
        public void Ready(){
            // if(!audioCheck.TryGetAudioLength(out var length)) return;
            thisSlider.maxValue = _audio.clip.length;
            thisSlider.value = 0;
        }
    }
}

