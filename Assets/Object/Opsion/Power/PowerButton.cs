using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PowerButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AsObservable()
        .Subscribe(_ => UnityEngine.Application.Quit())
        .AddTo(this);
    }

}
