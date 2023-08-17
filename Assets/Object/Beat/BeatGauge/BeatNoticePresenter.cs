using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken.DanceView{
    public class BeatNoticePresenter : MonoBehaviour
    {
        [SerializeField] AudioImport _audioImport;
        [SerializeField] AudioControl _audioControl;
        [SerializeField] Ken.Setting.BeatTypeSetting beatTypeSetting;

        [SerializeField] BeatNoticeView view;
        [SerializeField] AudioSource _audio;
        [SerializeField] Music music;

        [SerializeField] GameObject Plus8;

        void Start(){
            _audioImport.OnSelectMusic
            .Subscribe(_ => view.DeleteNotice())
            .AddTo(this);

            this.UpdateAsObservable()
            .Where(_ => _audio.isPlaying)
            .Subscribe(_ => Check())
            .AddTo(this);

            _audioControl.OnSeek
            .Subscribe(_ => view.DeleteNotice())
            .AddTo(this);

            beatTypeSetting.OnSelectBeatType
            .Subscribe(_ => ChangeMode())
            .AddTo(this);
        }

        void Check(){
            if(Music.Just.IsNull()) view.DeleteNotice();

            //mybeatが4なら2が裏拍になる
            if(music.myBeat == 4)    view.PlayBeatNotice(2);
            else                    view.PlayBeatNotice2(4);
        }

        void ChangeMode(){
            if(music.myBeat == 4){
                Plus8.SetActive(false);
            }else{
                Plus8.SetActive(true);
            }
            
        }
    }
}

