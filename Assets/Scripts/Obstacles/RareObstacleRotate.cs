using UnityEngine;

public class RareObstacleRotate : AbstractRareObstacle
{
    [SerializeField] private float RotationSpeed = 100f;
    
    private int _direction;

    private void Update()
    {
        RotateObject();
    }
    
    private void OnEnable()
    {
        _direction = Random.Range(0, 2) * 2 - 1;
    }

    private void RotateObject()
    {
        transform.Rotate(0, 0, RotationSpeed * _direction * Time.deltaTime);
    }
}