using System;
using UnityEngine;

namespace Ken
{
    //Updateで呼ぶと重くなるから、単発呼びのみ可能
    //検証：nullチェックでエラーを吐くだけで、べつにupdateで呼んでもだいじょうぶ？
    public class AudioCheck : Singleton<AudioCheck>
    {
        [SerializeField] AudioSource _audio;

        public bool IsPlaying(){
            return _audio.isPlaying;
        }
        public bool ClipIsNull(){
            if(_audio.clip == null) return true;
            return false;
        }

        public bool TryGetAudioTime(out float time){
            if(_audio.clip != null){
                time = _audio.time;
                return true;
            }else{
                throw new Exception("ClipがNullです");
            }            
        }

        public float GetTime(){
            return _audio.time;
        }

        public bool TryGetAudioLength(out float time){
            if(_audio.clip != null){
                time = _audio.clip.length;
                return true;
            }else{
                throw new Exception("ClipがNullです");
            }            
        }

        public bool TryGetClip(out AudioClip clip){
            if(_audio.clip != null){
                clip = _audio.clip;
                return true;
            }else{
                throw new Exception("ClipがNullです");
            }            
        }
    }
}

