using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    
    private bool isTriggered;
    private void OnTriggerEnter(Collider collision)
    {
        if (isTriggered)
        {
            return;
        }
        
        if (collision.CompareTag("Player"))
        {
            isTriggered = true;
            transform.gameObject.SetActive(false);
            collision.GetComponent<Player>().AddBrick();
        }
    }
}
