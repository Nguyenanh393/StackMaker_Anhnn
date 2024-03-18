using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClick()
    {
        UIManager.Instance.HideUI(3);
        UIManager.Instance.ShowUI(2);
    }
}

