using System;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    public static event Action  PlayerOnFloor;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) PlayerOnFloor?.Invoke();
    }
}