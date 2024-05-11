using Ken;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    public GameObject popupPanel;

    void Start()
    {
        // ポップアップを非表示にする
        popupPanel.SetActive(false);
    }

    public void ShowPopup()
    {
        if(AudioCheckPresenter.I.ClipIsNull()) return;
        // ポップアップを表示する
        popupPanel.SetActive(true);
    }

    public void ClosePopup()
    {
        // ポップアップを非表示にする
        popupPanel.SetActive(false);
    }
}
