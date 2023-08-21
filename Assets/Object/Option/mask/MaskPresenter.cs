using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ken
{
    public class MaskPresenter : Singleton<MaskPresenter>
    {
        [SerializeField] Image mask;

        public void LoadMask(){
            mask.enabled = mask.enabled == true ? false:true;
        }
    }
}

