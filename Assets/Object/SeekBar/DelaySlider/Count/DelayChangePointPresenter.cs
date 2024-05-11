using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;

namespace Ken{
    public class DelayChangePointPresenter : Singleton<DelayChangePointPresenter>
    {

        [SerializeField] AudioControlPresenter audioControl;
        [SerializeField] DelaySliderManager manager;
        [SerializeField] Music _music;
        BPMManager _bpmSetting;
        [SerializeField] AudioSource audioSource;
 
        float buffer;
        [SerializeField]DelayData data;
        int tmpIndex=0;
        int NowIndex=0;


        void Update()
        {
            if(!audioSource.isPlaying) return;

            //今が何番目のdelayスライダーのハンドルかを確認する
            for(int i=0;i<data.GetCount();i++){
                //1:再生時間がdata[]よりも小さいと判定が出たら終了する
                if(audioSource.time + buffer < data.GetTime(i))    break;
                else    tmpIndex = i;
            }

            //2:それが今のindexと同じなら変更しない
            if(NowIndex == tmpIndex)    return;
            //ここまで来たという事はtmpが新しいdelayになっている
            ValidateDelay();
            Debug.Log("パブリッシャー");
        }

        void Start(){
            _bpmSetting = BPMManager.I;

            audioControl.OnPlayStart
            .Subscribe(_ => ValidateDelay())
            .AddTo(this);
        }

        void ValidateDelay(){
            //データ取得
            data = manager.CreateDelayTimeData();

            // 一般的には44100
            //開始秒数をtimesampleで指定
            //Debug.Log(_music.EntryPointSample);
            _music.EntryPointSample = (int)(data.GetTime(tmpIndex) * audioSource.clip.frequency);
            //Debug.Log(_music.EntryPointSample);

            //BPMをセット
            _bpmSetting.ChangeBPM(data.GetBPM(tmpIndex));
            _bpmSetting.Apply();

            //今が何個目のdelayかを設定
            NowIndex = tmpIndex;

            //今となっては謎。いらん
            //間に合うラインの設定
            //60/bpm/4 = bar * 速度ごとに間に合うラインがあるからそこを見つける(speed * 係数)
            //buffer = 60f / _music.myTempo / 4 * audioControl.Speed.Value * 2;
            // Debug.Log(buffer);
        }

        public void PublicValidate(){
            ValidateDelay();
        }

        public void Reset(){
            tmpIndex=0;
            NowIndex=0;
        }

        public DelayData GetDelayData(){
            return data;
        }
    }
}
