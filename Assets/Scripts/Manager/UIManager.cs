using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private List<GameObject> UIs;
    private void Start()
    {
        GameManager.Instance.OnEvent -= OnEvent;
        GameManager.Instance.OnEvent += OnEvent;
        UIManager.Instance.HideUI(3);
        UIManager.Instance.HideUI(0);
    }
    
    private void OnEvent(EventID eventID)
    {
        switch (eventID)
        {
            case EventID.OnCompleteLevel:
                StartCoroutine(ShowWin());
                break;
            case EventID.OnRestartLevel:
                HideWin();
                break;
        }
    }

    public void HideUI(int id)
    {
        UIs[id].SetActive(false);
    }

    public void ShowUI(int id)
    {
        UIs[id].SetActive(true);
    }

    private IEnumerator ShowWin()
    {
        ShowUI(0);
        yield return new WaitForSeconds(5);
        HideUI(0);
    }
    
    public void HideWin()
    {
        HideUI(0);
    }
}
