using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken.Main.SeekBar
{
    public class HandlePresenter : MonoBehaviour
    {
        bool isGrag;
        [SerializeField] SeekBarView view;
        [SerializeField] AudioControl audioControl;

        void Start(){
            // sliderP = this.gameObject.GetComponent<SliderPresenter>();
            var eventTrigger = this.gameObject.GetComponent<ObservableEventTrigger>();

            //ハンドルにふれる系
            eventTrigger.OnPointerEnterAsObservable()
                .Subscribe(_ =>{
                    view.BigImage();
                })
                .AddTo(this);

            eventTrigger.OnPointerDownAsObservable()
                .Subscribe(_ =>{
                    isGrag=true;
                    audioControl.Pause();
                    Debug.Log("どらっぐ");
                })
                .AddTo(this);

            eventTrigger.OnPointerExitAsObservable()
                .Where(_ => !isGrag)
                .Subscribe(_ => view.SmallImage())
                .AddTo(this);
            
            eventTrigger.OnPointerUpAsObservable()
                .Subscribe(_ =>{
                    view.SmallImage();
                    audioControl.Play();
                    isGrag=false;
                })
                .AddTo(this);
        }
    }
}