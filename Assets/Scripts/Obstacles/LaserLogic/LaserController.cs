using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : AbstractRareObstacle
{
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
