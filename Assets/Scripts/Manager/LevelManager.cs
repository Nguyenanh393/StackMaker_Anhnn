using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private LevelButtonUI buttonPrefab;
    [SerializeField] private Transform parentPosition;
    [SerializeField] private LevelSO levelDataSO;
    
    private static GameObject currentMap;
    private void Start()
    {
        SpawnLevelList();
        GameManager.Instance.OnEvent -= OnEvent;
        GameManager.Instance.OnEvent += OnEvent;
    }

    private void OnEvent(EventID eventID)
    {
        switch (eventID)
        {
            case EventID.OnCompleteLevel:
                DestroyCurrentMap();
                break;
            case EventID.OnRestartLevel:
                SpawnLevelItem(PlayerPrefs.GetInt("Level"));
                break;
        }
    }
    
    private void SpawnLevelItem(int levelID)
    {
        LevelButtonUI levelButtonUI = Instantiate(buttonPrefab, parentPosition);
        levelButtonUI.OnInit(levelID, OnLevelItemUIClickHandle);
    }

    private void OnLevelItemUIClickHandle(string levelID)
    {
        currentMap = Resources.Load<GameObject>($"{Constance.MAP_PATH}{levelID}");
        Debug.Log(currentMap);
        currentMap = Instantiate(currentMap);
        UIManager.Instance.HideUI(3);
    }
    
    [ContextMenu("destroy")]
    private void DestroyCurrentMap()
    {
        Destroy(currentMap);
    }

    private void SpawnLevelList()
    {
        List<LevelData> list = levelDataSO.list;
        for (int i = 0; i < list.Count; i++)
        {
            SpawnLevelItem(list[i].LevelID);
        }
    }
    
    public GameObject GetCurentMap()
    {
        return currentMap;  
    }
    
    public static void SetCurrentMap(GameObject map)
    {
        currentMap = map;
    }
}


