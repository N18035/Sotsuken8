using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using Ken.Setting;

namespace Ken.DanceView
{
    public class AudioControlPresenter : MonoBehaviour
    {
        [SerializeField] Button Restart;
        [SerializeField] Button forward;
        [SerializeField] Button backForward;
        [SerializeField] AudioSource _audioSource;
        [SerializeField] AudioControl _audioControl;
        [SerializeField] TimeViewer timeViewer;
        void Start()
        {
            Restart.onClick.AsObservable()
            .Where(_ => _audioSource.clip != null)
            .Subscribe(_ => _audioControl.ReStart())
            .AddTo(this);

            forward.onClick.AsObservable()
            .Where(_ => _audioSource.clip != null)
            .Subscribe(_ => _audioControl.Forward10())
            .AddTo(this);


            backForward.onClick.AsObservable()
            .Where(_ => _audioSource.clip != null)
            .Subscribe(_ => _audioControl.BackForward10())
            .AddTo(this);
        }

        void Update(){
            if(_audioSource.clip == null) return;

            //再生時間
            timeViewer.AudioTIme(_audioSource.time.ToString("F2"));

            if(Music.Just.IsNull())   timeViewer.MusicTime("---");
            else                     timeViewer.MusicTime(Music.Just.ToString());
        }
    }
}

