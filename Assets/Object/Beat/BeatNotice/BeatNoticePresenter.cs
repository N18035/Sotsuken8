using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Ken{
    public class BeatNoticePresenter : Singleton<BeatNoticePresenter>
    {
        AudioImportPresenter _audioImport;
        AudioControlPresenter _audioControl;
        [SerializeField] BeatTypeSetting beatTypeSetting;

        [SerializeField] BeatNoticeView view;
        [SerializeField] AudioSource mainAudio;
        [SerializeField] Music music;

        void Start(){
            _audioImport = AudioImportPresenter.I;
            _audioControl = AudioControlPresenter.I;

            _audioImport.OnSelectMusic
            .Subscribe(_ => view.DeleteNotice())
            .AddTo(this);

            this.UpdateAsObservable()
            .Where(_ => mainAudio.isPlaying)
            .Subscribe(_ => Check())
            .AddTo(this);

            _audioControl.OnSeek
            .Subscribe(_ => view.DeleteNotice())
            .AddTo(this);

            beatTypeSetting.OnSelectBeatType
            .Subscribe(_ => ChangeBars())
            .AddTo(this);

            view.Hide8Notice();
        }

        //ビート表示を表示
        void Check(){
            if(Music.Just.IsNull()) view.DeleteNotice();

            view.PlayBeatNotice(Music.Just.Beat);
        }

        //4小節と8小節を変更する
        void ChangeBars(){
            if(music.myBeat == 4){
                view.Hide8Notice();
            }else{
                view.Show8Notice();
            }
        }
    }
}

