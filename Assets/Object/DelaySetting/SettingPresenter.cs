using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Ken
{
    public class SettingPresenter : MonoBehaviour
    {
        // [SerializeField] Toggle toggle;
        [SerializeField] Button buttonMinusBeat;
        [SerializeField] Button buttonMinusSeconds;
        [SerializeField] Button buttonPulsBeat;
        [SerializeField] Button buttonPulsSeconds;
        [SerializeField] Button nowTimeSet;
        [SerializeField] Button addSliderRight;
        [SerializeField] Button removeSlider;
        [SerializeField] GameObject keikoku;
        

        [SerializeField] DelaySliderManager manager;

        void Start(){
            buttonPulsBeat.onClick.AsObservable()
            .Subscribe(_ =>manager.DelayAdjustForSecond(PM.Plus, 0.1f))
            .AddTo(this);

            buttonPulsSeconds.onClick.AsObservable()
            .Subscribe(_ => manager.DelayAdjustForSecond(PM.Plus,0.01f))
            .AddTo(this);

            buttonMinusBeat.onClick.AsObservable()
            .Subscribe(_ => manager.DelayAdjustForSecond(PM.Minus, 0.1f))
            .AddTo(this);

            buttonMinusSeconds.onClick.AsObservable()
            .Subscribe(_ => manager.DelayAdjustForSecond(PM.Minus, 0.01f))
            .AddTo(this);

            nowTimeSet.onClick.AsObservable()
            .Subscribe(_ => manager.DelaySetupForAudioTime())
            .AddTo(this);

            addSliderRight.onClick.AsObservable()
            .Subscribe(_ => manager.AddSlider())
            .AddTo(this);

            removeSlider.onClick.AsObservable()
            .Subscribe(_ => manager.RemoveSlider())
            .AddTo(this);
        }

        public void Batting(bool flag){
            if(flag)    keikoku.SetActive(true);
            else keikoku.SetActive(false);
        }
    }
}
