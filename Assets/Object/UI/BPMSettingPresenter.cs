using UniRx.Triggers;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Ken.Setting
{
    public class BPMSettingPresenter : MonoBehaviour
    {
        [SerializeField] BPMSetting _bpmSetting;
        [SerializeField] AudioControl con;
        [SerializeField] Slider _slider;
        [SerializeField] Button up;
        [SerializeField] Button down;
        [SerializeField] Button on;
        [SerializeField] Button off;
        [SerializeField] GameObject setting;
        [SerializeField] Text text;
        

        float speed;

        void Start(){
            _slider.maxValue = _bpmSetting.MaxBPM;

            _slider.onValueChanged.AsObservable()
            .Subscribe(_ => ChangeForSlider())
            .AddTo(this);

            _slider.OnPointerUpAsObservable()
            .Subscribe(_ => _bpmSetting.Apply())
            .AddTo(this);

            _slider.onValueChanged.AsObservable()
            .Throttle(TimeSpan.FromMilliseconds(100))
            .Subscribe(t => _bpmSetting.Apply())
            .AddTo(this);

            up.onClick.AsObservable()
            .Subscribe(_ => ChangeForButton(1))
            .AddTo(this);

            down.onClick.AsObservable()
            .Subscribe(_ => ChangeForButton(-1))
            .AddTo(this);

            on.onClick.AsObservable()
            .Where(_ => !AudioCheck.I.ClipIsNull())
            .Subscribe(_ => setting.SetActive(true))
            .AddTo(this);

            off.onClick.AsObservable()
            .Subscribe(_ => setting.SetActive(false))
            .AddTo(this);

            con.Speed
            .Subscribe(s => SpeedChanged(s))
            .AddTo(this);
        }
        
        void ChangeForButton(int value){
            _bpmSetting.ChangeBPM(_bpmSetting.BPM.Value + value);
            TextChange(_bpmSetting.BPM.Value);
            _bpmSetting.Apply();
        }

        public void ChangeForSlider(){
            _bpmSetting.ChangeBPM((int)_slider.value);
            TextChange(_bpmSetting.BPM.Value);
        }

        private void TextChange(int value){
            float f = (float)value * speed;
            value =(int)f;
            text.text = value.ToString();
        }

        void SpeedChanged(float playSpeed){
            speed = playSpeed;
            // TextChange();
        }
    }
}
