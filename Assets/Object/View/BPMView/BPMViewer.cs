using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Ken{
    public class BPMViewer : MonoBehaviour
    {
        [SerializeField] Text text;
        AudioBPMPresenter _bpmSetting;
        AudioControlPresenter _audioControl;

        void Start()
        {
            _bpmSetting = AudioBPMPresenter.I;
            _audioControl = AudioControlPresenter.I;

            _bpmSetting.BPM
            .Subscribe(_ => calc())
            .AddTo(this);

            _audioControl.Speed
            .Subscribe(_ => calc())
            .AddTo(this);
        }

        void calc(){
            var bpm = _bpmSetting.BPM.Value * _audioControl.Speed.Value;
            text.text = ((int)bpm).ToString();
        }
    }
}

