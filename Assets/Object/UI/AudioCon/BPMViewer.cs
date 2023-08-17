using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using Ken.Setting;

namespace Ken.DanceView{
    public class BPMViewer : MonoBehaviour
    {
        [SerializeField] Text text;
        [SerializeField] BPMSetting _bpmSetting;
        [SerializeField] AudioControl _audioControl;

        void Start()
        {
            _bpmSetting.BPM
            .Subscribe(_ => calc())
            .AddTo(this);

            _audioControl.Speed
            .Subscribe(_ => calc())
            .AddTo(this);
        }

        void calc(){
            var bpm = _bpmSetting.BPM.Value * _audioControl.Speed.Value;
            text.text = bpm.ToString();
        }
    }
}

