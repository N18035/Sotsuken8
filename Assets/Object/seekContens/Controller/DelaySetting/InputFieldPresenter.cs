using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Ken.Save;

namespace Ken.Delay{
    [RequireComponent(typeof(InputField))]
    public class InputFieldPresenter : MonoBehaviour
    {
        InputField thisInput;

        [SerializeField] DelaySliderManager manager;
        [SerializeField] AudioControl _audioControl;
        [SerializeField] SaveManager save;

        void Start(){
            thisInput = this.gameObject.GetComponent<InputField>();
            
            thisInput.OnEndEditAsObservable()
            .Where(t => t!=null)
            .Where(t => t!="")
            .Subscribe(t =>{
                var value = float.Parse(t);
                int bpm = (int)(value / _audioControl.Speed.Value);
                manager.BPMSet(bpm);
            })
            .AddTo(this);

            //外部
            _audioControl.Speed
            .Subscribe(_ => SetBPM(_audioControl.Speed.Value,manager.GetNowBPM()))
            .AddTo(this);

            manager.OnNowChanged
            .Subscribe(_ => SetBPM(_audioControl.Speed.Value,manager.GetNowBPM()))
            .AddTo(this);

            _audioControl.Speed
            .Subscribe(s =>{
                if(s == 1) thisInput.textComponent.color = Color.black;
                else thisInput.textComponent.color = Color.red;
            })
            .AddTo(this);

            save.OnLoad
            .Subscribe(_ =>SetBPM(_audioControl.Speed.Value,manager.GetNowBPM()))
            .AddTo(this);
        }

        void SetBPM(float speed , int BPM)
        {
            int bpm =(int)( speed * BPM);
            thisInput.text = bpm.ToString();
        }
    }
}
