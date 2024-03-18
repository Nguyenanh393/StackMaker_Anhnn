using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButton : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnEvent += OnEvent;
    }
    
    private void OnEvent(EventID eventID)
    {
        if (eventID == EventID.OnCompleteLevel)
        {
            OnClick();
        }
    }
    
    public void OnClick()
    {
        UIManager.Instance.HideUI(0);
        int levelID = PlayerPrefs.GetInt("Level");
        GameManager.Instance.RestartLevel();
    }
}
