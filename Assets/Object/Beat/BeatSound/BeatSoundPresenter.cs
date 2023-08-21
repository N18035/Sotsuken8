using UnityEngine;

namespace Ken{
    public class BeatSoundPresenter : MonoBehaviour
    {
        [SerializeField] private AudioSource _SESource;
        [SerializeField] AudioSource _audio;
        [SerializeField] Music music;

        BeatSoundData data;

        void Update()
        {
            //null  ちぇっく
            if(!_audio.isPlaying) return;
            if(data.Number==2) return;

            //beatSound鳴らす
            if(Music.IsJustChangedBeat()){
                _SESource.Play();
            }
        }

        public void SetBeatSoundSetting(BeatSoundData d){
            data = d;
        } 
    }
}
