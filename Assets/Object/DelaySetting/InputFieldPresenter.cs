using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken{
    public class InputFieldPresenter : Singleton<InputFieldPresenter>
    {
        [SerializeField] InputField BPMInput;

        DelaySliderManager manager;
        AudioControlPresenter _audioControl;
        [SerializeField] SaveManager save;

        void Start(){
            _audioControl = AudioControlPresenter.I;
            manager =DelaySliderManager.I;
            
            BPMInput.OnEndEditAsObservable()
            .Where(t => t!=null)
            .Where(t => t!="")
            .Subscribe(t =>{
                var value = float.Parse(t);
                int bpm = (int)(value / _audioControl.Speed.Value);
                bpm = bpm < 1 ? 1 : bpm;
                manager.BPMSet(bpm);
            })
            .AddTo(this);

            //外部
            _audioControl.Speed
            .Subscribe(_ => SetBPM(_audioControl.Speed.Value,manager.GetNowBPM()))
            .AddTo(this);

            manager.OnNow
            .Subscribe(_ => SetBPM(_audioControl.Speed.Value,manager.GetNowBPM()))
            .AddTo(this);

            _audioControl.Speed
            .Subscribe(s =>{
                if(s == 1) BPMInput.textComponent.color = Color.white;
                else BPMInput.textComponent.color = Color.red;
            })
            .AddTo(this);

            if(save==null) return;
            save.OnLoad
            .Subscribe(_ =>SetBPM(_audioControl.Speed.Value,manager.GetNowBPM()))
            .AddTo(this);
        }

        public void SetBPM(float speed , int BPM)
        {
            int bpm =(int)( speed * BPM);
            BPMInput.text = bpm.ToString();
        }

    }
}
