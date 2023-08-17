using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken.Main.SeekBar
{
    public class SeekBarView : MonoBehaviour
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

