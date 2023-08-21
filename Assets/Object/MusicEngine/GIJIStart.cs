using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Ken.Delay;

namespace Ken{
    //主にMusicの初期化関連を担当
    public class GIJIStart : Singleton<GIJIStart>
    {
        [SerializeField] AudioImportPresenter _audioImport;
        [SerializeField] AudioBPMPresenter _bpmSetting;
        [SerializeField] BeatTypeSetting _beatTypeSetting;
        [SerializeField] DelayPresenter _delay;
        [SerializeField] Music _music;
        [SerializeField] AudioControlPresenter _audioControll;

        [SerializeField] SoundWave _soundWave;
        [SerializeField] ZoomModel _zoom;
        [SerializeField] SeekBarPresenter _seekBarPresenter;
        [SerializeField] TimeLinePresenter _timeLine;
        [SerializeField] Ken.Main.ContentLengthPresenter _content;
        [SerializeField] AudioSource _audioSource;

        [SerializeField] MaskPresenter mask;
        void Start()
        {
            //楽曲を取り込む
            _audioImport.OnSelectMusic
            .Subscribe(_ => FirstOnValidate())
            .AddTo(this);

            //bpmを設定
            _bpmSetting.OnSelectBPM
            .Subscribe(_ => NormalOnValidate())
            .AddTo(this);

            //48の変更
            _beatTypeSetting.OnSelectBeatType
            .Subscribe(_ => NormalOnValidate())
            .AddTo(this);

            _delay.OnSelectDelay
            .Subscribe(_ => NormalOnValidate())
            .AddTo(this);

            _audioControll.OnSeek
            .Subscribe(_ => NormalOnValidate())
            .AddTo(this);

            _audioControll.OnPlayStart
            .Subscribe(_ => NormalOnValidate())
            .AddTo(this);
        }

        public void NormalOnValidate(){
            //FIXME暫定対応
            if(_audioSource.clip == null) return;
            _music.OnValidate();
            _music.OnClickAwake();

            _timeLine.ReadyTimeLine();
        }

        public void OnValidatePublic(){
            NormalOnValidate();
        }

        private void FirstOnValidate(){
            mask.LoadMask();
            _audioControll.ReadyAudioTime();

            //Musicに値を適応
            _music.OnValidate();
            _music.OnClickAwake();

            //UIの変更
            _seekBarPresenter.ReadySeekBar();//楽曲設定の時だけでok
            _content.ReadyStartAndEnd();//楽曲設定の時だけ
            _timeLine.ReadyTimeLine();
            _soundWave.CreateSoundWave();
            _delay.Initialize();

            StartCoroutine("OneMore");
        }

        IEnumerator OneMore()
        {
            //音楽の長さに応じて変える。係数は適当
            float time = _audioSource.clip.length * 0.03f;
            yield return new WaitForSeconds(time);
            _soundWave.CreateSoundWave();
            mask.LoadMask();
        }

        IEnumerator TakeOneTime()
        {
            //初期化
            _audioControll.ReadyAudioTime();

            //雑に数秒待つ
            yield return new WaitForSeconds(3);
        }
    }
}
