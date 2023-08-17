
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Collections.Generic;

namespace Ken.Save{
    public class SavePresenter : MonoBehaviour
    {
        [SerializeField] Button saveB;
        [SerializeField] Button overRideB;
        [SerializeField] Button loadB;
        [SerializeField] Text infoText;
        [SerializeField] Text pathText;
        [SerializeField] SaveManager manager;
        [SerializeField] AudioImport import;
        

        void Start(){
            saveB.onClick.AsObservable()
            .Where(_ => !AudioCheck.I.ClipIsNull())
            .Subscribe(_ =>manager.Save())
            .AddTo(this);


            overRideB.onClick.AsObservable()
            .Where(_ => !AudioCheck.I.ClipIsNull())
            .Subscribe(_ =>manager.NewSave())
            .AddTo(this);

            loadB.onClick.AsObservable()
            .Where(_ => !AudioCheck.I.ClipIsNull())
            .Subscribe(_ =>manager.Load())
            .AddTo(this);

            manager.Info
            .Subscribe(t => infoText.text = t)
            .AddTo(this);

            manager.NowPath
            .Subscribe(t =>{
                string[] arr = t.Split('\\');
                var list = new List<string>();
                list.AddRange(arr);
                pathText.text = list[list.Count-1];
            })
            .AddTo(this);

            import.OnSelectMusic
            .Subscribe(_ => infoText.text = "")
            .AddTo(this);
        }
    }
}
