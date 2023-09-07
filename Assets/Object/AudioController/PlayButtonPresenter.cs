using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Ken.DanceView{
    public class PlayButtonPresenter : MonoBehaviour
    {
        [SerializeField] Button _button;
        [SerializeField] Sprite play;
        [SerializeField] Sprite pause;
        [SerializeField] AudioSource audioSource;
        AudioControlPresenter _audioControl;
        AudioImportPresenter _audioImport;
        AudioCheckPresenter check;

        void Start(){
            _audioImport = AudioImportPresenter.I;
            check = AudioCheckPresenter.I;
            _audioControl = AudioControlPresenter.I;

            _button.onClick.AsObservable()
            .Where(_ => !AudioCheckPresenter.I.ClipIsNull())
            .Subscribe(_ =>{
                if(audioSource.isPlaying)   _audioControl.Pause();
                else                        _audioControl.Play();
            } )
            .AddTo(this);

            _audioImport.OnSelectMusic
            .Subscribe(_ => _button.image.sprite = play)
            .AddTo(this);

            Observable.EveryUpdate()
                .Select(_ => audioSource.isPlaying)
                .DistinctUntilChanged() // 状態が変化したときだけ通知
                .Subscribe(isPlaying =>
                {
                    if (isPlaying)    _button.image.sprite = pause;
                else                  _button.image.sprite = play;;
                })
                .AddTo(this);
        }

    }
}



