using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Ken.DanceView{
    public class PlayButtonPresenter : MonoBehaviour
    {
        [SerializeField] Button _button;
        [SerializeField] Image _image;
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
            .Where(_ => !AudioCheckPresenter.I.ClipIsNull() && audioSource.isPlaying)
            .Subscribe(_ => _audioControl.Pause())
            .AddTo(this);

            _button.onClick.AsObservable()
            .Where(_ => !AudioCheckPresenter.I.ClipIsNull() && !audioSource.isPlaying)
            .Subscribe(_ => _audioControl.Play())
            .AddTo(this);

            _audioImport.OnSelectMusic
            .Subscribe(_ => _image.sprite = play)
            .AddTo(this);

            Observable.EveryUpdate()
                .Select(_ => audioSource.isPlaying)
                .DistinctUntilChanged() // 状態が変化したときだけ通知
                .Subscribe(isPlaying =>
                {
                    if (isPlaying)
                    {
                        _image.sprite = pause;
                    }
                    else
                    {
                        _image.sprite = play;;
                    }
                })
                .AddTo(this);
        }

    }
}



