using UnityEngine;
using UnityEngine.UI;

namespace Ken
{
    public class DelaySliderHandleView : MonoBehaviour
{
    [SerializeField] Image handle;
    [SerializeField] Image line;
    private static readonly Color red = Color.red;
    private static readonly Color white = Color.white;

    public void SetColor(bool on){
        if(on)  handle.color = Color.red;
        else handle.color = Color.white;
    }


    public void BigImage(){
        handle.transform.localScale = new Vector3(3.5f, 1f, 1f);
        line.enabled = true;
    }

    public void SmallImage(){
        handle.transform.localScale = new Vector3(1f, 1f, 1f);
        line.enabled = false;
    }
}

}
