using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken
{
    public class HandlePresenter : MonoBehaviour
    {
        bool isGrag;
        [SerializeField] SeekBarHandleView view;
        [SerializeField] AudioControlPresenter audioControl;
        [SerializeField] ObservableEventTrigger eventTrigger;

        void Start(){
            //ハンドルにふれる系
            eventTrigger.OnPointerEnterAsObservable()
                .Subscribe(_ =>{
                    view.BigImage();
                })
                .AddTo(this);

            eventTrigger.OnPointerExitAsObservable()
                .Where(_ => !isGrag)
                .Subscribe(_ => view.SmallImage())
                .AddTo(this);

            eventTrigger.OnPointerDownAsObservable()
                .Subscribe(_ =>{
                    isGrag=true;
                    audioControl.Pause();
                    Debug.Log("どらっぐ");
                })
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