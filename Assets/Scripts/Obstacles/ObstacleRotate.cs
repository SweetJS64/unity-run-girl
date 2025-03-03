using UnityEngine;

public class ObstacleRotate : MonoBehaviour
{
    [SerializeField] private float RotationSpeed = 100f;
    
    private int _direction;

    private void Update()
    {
        RotateObject();
    }

    private void RotateObject()
    {
        transform.Rotate(0, 0, RotationSpeed * _direction * Time.deltaTime);
    }

    private void OnEnable()
    {
        _direction = Random.Range(0, 2) * 2 - 1;
    }
}