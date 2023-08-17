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
        [SerializeField] AudioControl _audioControl;
        [SerializeField] AudioImport _audioImport;
        AudioCheck check;

        void Start(){
            check = AudioCheck.I;

            _button.onClick.AsObservable()
            .Where(_ => !AudioCheck.I.ClipIsNull())
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



