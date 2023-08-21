using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Ken{
    public class BPMViewer : MonoBehaviour
    {
        [SerializeField] Text text;
        [SerializeField] Music _music;
        [SerializeField] AudioSource _audioSource;
        AudioControlPresenter _audioControl;

        void Start()
        {
            _audioControl = AudioControlPresenter.I;
        }

        void Update(){
            if(_audioSource.clip == null) return;
            
            if(Music.Just.IsNull())   text.text ="---";
            else                     text.text = (_music.myTempo * _audioControl.Speed.Value).ToString();
        }
    }
}

