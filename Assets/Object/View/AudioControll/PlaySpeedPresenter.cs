using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Ken
{
    public class PlaySpeedPresenter : MonoBehaviour
    {
        [SerializeField] Dropdown _dropdown;
        AudioControlPresenter _audioCon;

        void Start(){
            _audioCon = AudioControlPresenter.I;

            _dropdown.onValueChanged.AsObservable()
            .Subscribe(v => ChangeSpeed(v))
            .AddTo(this);
        }

        void ChangeSpeed(int v){
            float speed = float.Parse(_dropdown.options[v].text);
            _audioCon.ChangeSpeed(speed);
        }
    }

}
