using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public void OnClick()
    {
        UIManager.Instance.HideUI(2);
        int levelID = PlayerPrefs.GetInt("Level");
        SpawnLevelItem(levelID);
    }
    
    private void SpawnLevelItem(int levelID)
    {
        GameObject currentMap = Resources.Load<GameObject>($"{Constance.MAP_PATH}{levelID}");
        currentMap = Instantiate(currentMap);
        LevelManager.SetCurrentMap(currentMap);
        
    }
}

