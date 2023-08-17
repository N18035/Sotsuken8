using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ken.Setting
{
    public class Mask : MonoBehaviour
    {
        [SerializeField] GameObject maske;

        public void LoadMask(){
            var b = maske.activeSelf == true ? false:true;
            maske.SetActive(b);
        }
    }
}

