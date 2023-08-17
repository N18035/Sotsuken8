using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using System.Linq;
using Sirenix.OdinInspector;//SerializedMonoBehaviourを使うのに必要

namespace Ken.Delay
{
    public class DelaySliderManager : MonoBehaviour
    {
        [ReadOnly]
        public int now=0;
        [ReadOnly]
        public bool allChange=false;
        public List<GameObject> Sliders = new List<GameObject>(1);

        public IReactiveProperty<int> OnNowChanged => _nowChange;
        private readonly ReactiveProperty<int> _nowChange = new ReactiveProperty<int>();

        [SerializeField] CountPresenter count;
        [SerializeField] SettingPresenter setting;

        
        float oneBeat;

        //委譲
        Add add;
        AudioCheck check;

        void Start(){
            check = AudioCheck.I;
            add = this.GetComponent<Add>();
        }

        public void AddSlider(){
            if(check.ClipIsNull()) return;
            
            for(int i=0;i<Sliders.Count;i++){
                if(Sliders[i].GetComponent<Slider>().value == check.GetTime()) return;
            }

            var obj = InstantSlider();
            obj.GetComponent<Slider>().value = check.GetTime();
            
            count.PublicValidate();
            ChangeNow(Sliders.Count-1);
        }

        public void RemoveSlider(){
            if(check.ClipIsNull()) return;
            if(Sliders.Count <= 1) return;

            var tmp = Sliders[now];
            Sliders.RemoveAt(now);
            
            Destroy(tmp);
            ChangeNow(0);
            for(int i=0;i<Sliders.Count;i++){
                Sliders[i].GetComponent<SliderPresenter>().SetID(i);
            }
            count.PublicValidate();
        }

        //初期化
        public void Reset(){
            Sliders.Clear();
            var obj = add.Reset();
            Sliders.Add(obj);
            
            //初期値代入
            Sliders[0].GetComponent<SliderPresenter>().Ready();
        }

        public void DelaySetupForAudioTime(){
            if(check.TryGetAudioTime(out var time ))
                Sliders[now].GetComponent<Slider>().value = time;
            else    throw new Exception("えら-");
        }

        public void DelayAdjustForBeat(PM pm){
            if(check.ClipIsNull()) return;
            //多分clampしてくれる
            // if(Sliders[now].GetComponent<Slider>().value == _delaySlider.maxValue) return;
            
            // 「60(1分)÷BPM(テンポ)で４分音符一拍分の長さ」(s)
            // 例) 60 / 120 = 0.5s
            oneBeat = 60f / Sliders[now].GetComponent<SliderPresenter>().BPM;

            if(pm == PM.Plus) Sliders[now].GetComponent<Slider>().value += oneBeat;
            else    Sliders[now].GetComponent<Slider>().value -= oneBeat;
        }

        public void DelayAdjustForSecond(PM pm){
            if(check.ClipIsNull()) return;
            // if(Slider[now].value == _delaySlider.maxValue) return;
            if(pm == PM.Plus) Sliders[now].GetComponent<Slider>().value += 0.01f;
            else    Sliders[now].GetComponent<Slider>().value -= 0.01f;
        }

        public void BPMSet(int bpm)
        {
            if(allChange){
                for(int i=0;i<Sliders.Count;i++){
                    Sliders[i].GetComponent<SliderPresenter>().SetBPM(bpm);
                }
            }else            Sliders[now].GetComponent<SliderPresenter>().SetBPM(bpm);

            count.PublicValidate();
        }

        public void ChangeNow(int id){
            now=id;
            //Sliders[now].transform.SetAsLastSibling();
            _nowChange.Value = id;
        }

       //テスト段階
        public DelayData CreateDelayTimeData(){

            List<float> time = new List<float>(){0};
            List<int> bpm = new List<int>(){0};

            time[0] = Sliders[0].GetComponent<Slider>().value;
            bpm[0] = Sliders[0].GetComponent<SliderPresenter>().BPM;

            for(int i=1;i<Sliders.Count;i++){
                time.Add(Sliders[i].GetComponent<Slider>().value);
                bpm.Add(Sliders[i].GetComponent<SliderPresenter>().BPM);
            }

            DelayData data = new DelayData(time,bpm);
            return data;
        }

        public bool JsonToDelayTimeData(DelayData data){
            //破壊
            Reset();
            Sliders[0].GetComponent<Slider>().value = data.GetTime(0);
            Sliders[0].GetComponent<SliderPresenter>().SetBPM(data.GetBPM(0));

            //1個だけなら終了
            if(data.GetCount() <=0){ 
                end();
                return true;
            }else{
                check.TryGetAudioLength(out var length); 
                for(int i=1;i<data.GetCount();i++){
                    //クリップの長さよりも点が大きければ間違いなので止める
                    if(length < data.GetTime(i)){
                        end();
                        return false;
                    } 

                    var obj = InstantSlider();
                    obj.GetComponent<Slider>().value = data.GetTime(i);
                    Sliders[i].GetComponent<SliderPresenter>().SetBPM(data.GetBPM(i));
                }
                end();
                return true;
            }

            void end(){
                ChangeNow(0);
                count.PublicValidate();
            }
        }

        public float GetNowValue(){
            return Sliders[now].GetComponent<Slider>().value;
        }

        public int GetNowBPM(){
            return Sliders[now].GetComponent<SliderPresenter>().BPM;
        }

        public void CheckBatting(){
            if(Sliders.Count <= 1) return;

            List<int> lis = new List<int>();
            for(int i=0;i<Sliders.Count;i++){
                // Debug.Log("かte"+(int)(Sliders[i].GetComponent<Slider>().value * 100));
                lis.Add((int)(Sliders[i].GetComponent<Slider>().value * 100));
            }

            var duplicates = lis.GroupBy(x => x)
                .Where(x => x.Count() > 1)
                .Select(x => new { Item = x.Key, Count = x.Count() })
                .ToList();

            //被りを検知
            if(String.Join(", ", duplicates) != ""){
                setting.Batting(true);
                Debug.Log(String.Join(", ", duplicates));
            } 
            else setting.Batting(false);
        }

        GameObject InstantSlider(){
            var obj = add.Instant();
            Sliders.Add(obj);           
            obj.GetComponent<SliderPresenter>().Ready();
            now = Sliders.Count -1;
            Sliders[now].GetComponent<SliderPresenter>().SetID(now);

            return obj;
        }
    }

    public enum PM{
        Plus,
        Minus
    }
}
