using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken.Delay
{
    public class SliderView : MonoBehaviour
{
    [SerializeField] Image handle;
    [SerializeField] Image sen;
    private static readonly Color red = Color.red;
    private static readonly Color white = Color.white;

    public void SetColor(bool on){
        if(on)  handle.color = red;
        else handle.color = white;
    }


    public void BigImage(){
        handle.transform.localScale = new Vector3(3.5f, 1f, 1f);
        sen.enabled = true;
    }

    public void SmallImage(){
        handle.transform.localScale = new Vector3(1f, 1f, 1f);
        sen.enabled = false;
    }
}

}
