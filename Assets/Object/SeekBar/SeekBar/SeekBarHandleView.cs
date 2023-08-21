using UnityEngine;
using UnityEngine.UI;

namespace Ken
{
    //クリックしやすくしてます
    public class SeekBarHandleView : MonoBehaviour
    {
        [SerializeField] Image handle;

        public void BigImage(){
            handle.transform.localScale = new Vector3(3f, 9f, 1f);
        }

        public void SmallImage(){
            handle.transform.localScale = new Vector3(1f, 9f, 1f);
        }
    }
}

