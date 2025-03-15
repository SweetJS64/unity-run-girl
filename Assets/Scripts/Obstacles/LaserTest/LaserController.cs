using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    //[SerializeField] private GameObject LeftGenerator;
    //[SerializeField] private GameObject RightGenerator;
    //[SerializeField] private GameObject Laser;
    
    private void OnEnable()
    {
        LaserGeneratorController.DisableLaserObstacle += Disable;
    }

    private void OnDisable()
    {
        LaserGeneratorController.DisableLaserObstacle -= Disable;
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
