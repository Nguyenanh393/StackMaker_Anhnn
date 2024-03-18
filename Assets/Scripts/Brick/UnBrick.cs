using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnBrick : MonoBehaviour
{
    [SerializeField] private GameObject brick;
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
            brick.gameObject.SetActive(true);
            collision.GetComponent<Player>().RemoveBrick();
        }
    }
}