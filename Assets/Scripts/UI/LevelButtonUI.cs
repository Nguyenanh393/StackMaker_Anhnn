using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelButtonUI : MonoBehaviour
{
    private int levelID;
    [SerializeField] private TextMeshProUGUI textIndex;
    [SerializeField] private UnityEngine.UI.Button levelButton;
    
    public void OnInit(int levelID, Action<string> onLevelSelected)
    {
        this.levelID = levelID;
        textIndex.text = levelID.ToString();
        levelButton.onClick.AddListener(()=>{
            onLevelSelected.Invoke(levelID.ToString());
        });
    }
}
