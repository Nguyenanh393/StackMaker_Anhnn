using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void OnClick()
    {
        UIManager.Instance.HideUI(2);
        UIManager.Instance.ShowUI(3);
    }
}
