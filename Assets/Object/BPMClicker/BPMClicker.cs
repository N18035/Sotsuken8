using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using System.Collections.Generic;

public class BPMClicker : MonoBehaviour
{
    public Button button; // BPMを計測するボタン
    private List<float> clickTimes = new List<float>(); // ボタンを押した時間のリスト
    private float bpm = 0f; // BPM

    void Start()
    {
        // ボタンがnullでないことを確認
        if (button != null)
        {
            // ボタンがクリックされた時のObservableを取得し、その時にOnButtonClicked()を呼び出す
            button.OnClickAsObservable()
                .Subscribe(_ => OnButtonClicked());
        }
        else
        {
            Debug.LogError("Button not assigned!");
        }
    }

    // ボタンがクリックされた時の処理
    void OnButtonClicked()
    {
        float currentTime = Time.time;
        clickTimes.Add(currentTime);

        // 最後の2つのボタン押下時間からBPMを計算
        if (clickTimes.Count >= 2)
        {
            float lastClickTime = clickTimes[clickTimes.Count - 2];
            float deltaTime = currentTime - lastClickTime;
            bpm = 60f / deltaTime;
            Debug.Log("BPM: " + bpm);
        }
    }
}
