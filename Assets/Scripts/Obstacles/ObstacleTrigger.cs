using System;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    public static event Action OnPlayerHit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("die");
        //if (other.CompareTag("Player")) OnPlayerHit?.Invoke();
    }
}