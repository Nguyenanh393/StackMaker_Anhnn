using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event Action <EventID> OnEvent;
    
    private void Event(EventID eventID) {
        OnEvent?.Invoke(eventID);
    }
    
    public void NextLevel() {
        Event(EventID.OnNextLevel);
    }
    
    public void CompleteLevel() {
        Event(EventID.OnCompleteLevel);
    }
    
    public void RestartLevel() {
        Event(EventID.OnRestartLevel);
    }
    
}
