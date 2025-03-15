using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObstacleController : AbstractRareObstacle
{
    private void OnEnable()
    {
        LaserGeneratorMover.DisableLaserObstacle += Disable;
    }

    private void OnDisable()
    {
        LaserGeneratorMover.DisableLaserObstacle -= Disable;
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
