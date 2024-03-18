using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winpos : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.CompleteLevel();
        }
    }
}
