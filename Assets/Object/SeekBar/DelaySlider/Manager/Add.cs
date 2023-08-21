using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ken.Delay
{
    public class Add : MonoBehaviour
    {
        [SerializeField] GameObject SliderPrefab;
        [SerializeField] GameObject Parents;

        public GameObject Instant(){
            var t = Instantiate(SliderPrefab,this.transform.position,Quaternion.identity);
            t.transform.SetParent(Parents.transform,false);
            t.transform.localPosition = new Vector3(-3f,0,0);
            return t;
        }

        public GameObject Reset(){
            foreach(Transform child in Parents.transform){
                Destroy(child.gameObject);
            }
            
            return Instant();
        }

    }
}

