using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

namespace Ken
{
    public class AudioControl : MonoBehaviour
    {
        [SerializeField] Music _music;
        [SerializeField] AudioSource _audioSource;

        public IObservable<Unit> OnSeek =>_seeked;
        private Subject<Unit> _seeked = new Subject<Unit>();

        public IObservable<Unit> OnPlayStart =>_play;
        private Subject<Unit> _play = new Subject<Unit>();

        public IReadOnlyReactiveProperty<float> Speed => speed;
        private readonly ReactiveProperty<float> speed = new ReactiveProperty<float>(1);

        private int loopStart=0;
        private int loopEnd=0;
        private bool loopFlag=false;

        void Start(){
            this.UpdateAsObservable()
            .Where(_ => _audioSource.time > (float)loopEnd && loopFlag)
            .Subscribe(_ =>{                 
                _audioSource.time = loopStart;
                _seeked.OnNext(Unit.Default);
            })
            .AddTo(this);
        }

        public void Play(){
            if(_audioSource.clip == null) return;

            //FIXME 色々残ってる
            //audioのplayから直で呼べないから仮でこうしてる
            StartCoroutine("stop");
            _play.OnNext(Unit.Default);
            _music.Play("musicengine","");
        }
        IEnumerator stop(){
            yield return new WaitForSeconds(1);
        }

        public void Pause(){
            if(_audioSource.clip == null) return;
            Music.Pause();
        }
        public void ReStart(){
            if(_audioSource.clip == null) return;
            _audioSource.time = 0f;
            _seeked.OnNext(Unit.Default);
        } 
        public void Forward10(){
            if(_audioSource.clip == null) return;
            if(_audioSource.time + 10f > _audioSource.clip.length) return;
            _audioSource.time += 10f;
            _seeked.OnNext(Unit.Default);
        }

        public void BackForward10(){
            if(_audioSource.clip == null) return;
            if(_audioSource.time - 10f < 0) return;
            _audioSource.time -= 10f;
            _seeked.OnNext(Unit.Default);
        }
        public void Seek(float length){
            if(_audioSource.clip == null) return;

            _audioSource.time = length;
            _seeked.OnNext(Unit.Default);
        }

        public void Loop(int start,int end){
            loopStart = start;
            Mathf.Clamp(end,1,_audioSource.clip.length);
            loopEnd = end;
            
            if(loopFlag)    loopFlag = false;
            else loopFlag = true;
        }

        public void ChangeSpeed(float v){
            //ピッチ変えるだけならnullチェック不要
            _audioSource.pitch = v;
            speed.Value = v;
        }

        public void ReadyAudioTime(){
            _audioSource.time = 0f;
        }
    }
}
