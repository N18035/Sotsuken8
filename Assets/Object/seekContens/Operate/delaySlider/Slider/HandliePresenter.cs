using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken.Delay
{
[RequireComponent(typeof(SliderPresenter))]
[RequireComponent(typeof(ObservableEventTrigger))]
public class HandliePresenter : MonoBehaviour
{
    bool isGrag;
    [SerializeField] SliderView view;
    [SerializeField] DelaySliderManager Manager;
    SliderPresenter sliderP;
    
    void Start()
    {
        sliderP = this.gameObject.GetComponent<SliderPresenter>();
        var eventTrigger = this.gameObject.GetComponent<ObservableEventTrigger>();

        //ハンドルにふれる系
        eventTrigger.OnPointerEnterAsObservable()
            .Subscribe(_ =>{
                Selected();
                Manager.ChangeNow(sliderP.ID);
            })
            .AddTo(this);

        eventTrigger.OnPointerDownAsObservable()
            .Subscribe(_ =>{
                isGrag=true;
                Manager.ChangeNow(sliderP.ID);
                Selected();
            })
            .AddTo(this);

        eventTrigger.OnPointerExitAsObservable()
            .Where(_ => !isGrag)
            .Subscribe(_ => view.SmallImage())
            .AddTo(this);
        
        eventTrigger.OnPointerUpAsObservable()
            .Subscribe(_ =>{
                view.SmallImage();
                isGrag=false;
            })
            .AddTo(this);

        //外部系
        Manager.OnNowChanged
        .Where(now => now == sliderP.ID)
        .Subscribe(_ => view.SetColor(true))
        .AddTo(this);

        Manager.OnNowChanged
        .Where(now => now != sliderP.ID)
        .Subscribe(_ => view.SetColor(false))
        .AddTo(this);
    }

    void Selected(){
        view.BigImage();
        view.SetColor(true);
    }
}

}
