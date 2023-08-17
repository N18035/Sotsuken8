using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Ken.Delay{
    public class SettingPresenter : MonoBehaviour
    {
        [SerializeField] Toggle toggle;
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
            //ボタン
            toggle.onValueChanged.AsObservable()
            .Subscribe(t => manager.allChange=t)
            .AddTo(this);

            buttonPulsBeat.onClick.AsObservable()
            .Subscribe(_ =>manager.DelayAdjustForBeat(PM.Plus))
            .AddTo(this);

            buttonPulsSeconds.onClick.AsObservable()
            .Subscribe(_ => manager.DelayAdjustForSecond(PM.Plus))
            .AddTo(this);

            buttonMinusBeat.onClick.AsObservable()
            .Subscribe(_ => manager.DelayAdjustForBeat(PM.Minus))
            .AddTo(this);

            buttonMinusSeconds.onClick.AsObservable()
            .Subscribe(_ => manager.DelayAdjustForSecond(PM.Minus))
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
