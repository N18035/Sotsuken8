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
        AudioControlPresenter _audioControl;
        AudioImportPresenter _audioImport;
        AudioCheckPresenter check;

        void Start(){
            _audioImport = AudioImportPresenter.I;
            check = AudioCheckPresenter.I;
            _audioControl = AudioControlPresenter.I;

            _button.onClick.AsObservable()
            .Where(_ => !AudioCheckPresenter.I.ClipIsNull())
            .Subscribe(_ => Click())
            .AddTo(this);

            _audioImport.OnSelectMusic
            .Subscribe(_ => _image.sprite = play)
            .AddTo(this);
        }

        void Click(){
            if(check.IsPlaying()){
                //停止処理
                _image.sprite = play;
                _audioControl.Pause();
            }
            else{
                //再生処理
                _image.sprite = pause;
                _audioControl.Play();
            } 
        }
    }
}



