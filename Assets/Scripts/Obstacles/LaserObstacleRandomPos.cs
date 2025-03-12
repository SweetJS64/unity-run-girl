using UnityEngine;
using Random = UnityEngine.Random;

public class LaserObstacleRandomPos : MonoBehaviour
{
    [SerializeField] private Transform FirstLaser;
    [SerializeField] private Transform SecondLaser;

    private float _minY;
    private float _maxY;
    private float _weightLaser;
    private float _firstPosY;
    private float _secondPosY;
    
    private void Awake()
    {
        Init();
    }
    
    private void OnEnable()
    {
        RandomPosition();
    }

    private void Init()
    {
        _minY = BordersData.Instance.MinY;
        _maxY = BordersData.Instance.MaxY;
        _weightLaser = GetComponentInChildren<SpriteRenderer>().sprite.bounds.size.y;
    }
    
    private void RandomPosition()
    {
        var caseId = Random.Range(0, 3);
        switch (caseId)
        {
            case 0:
                _firstPosY = _maxY - _weightLaser / 2;
                _secondPosY = _minY + _weightLaser / 2;
                SetPosition(_firstPosY, _secondPosY);
                break;
            case 1:
                _firstPosY = _maxY - _weightLaser / 2;
                _secondPosY = _firstPosY - _weightLaser / 2;
                SetPosition(_firstPosY, _secondPosY);
                break;
            case 2:
                _firstPosY = _minY + _weightLaser / 2;
                _secondPosY = _firstPosY + _weightLaser / 2; 
                SetPosition(_firstPosY, _secondPosY);
                break;
        }
    }

    private void SetPosition(float firstPosY, float secondPosY)
    {
        FirstLaser.position = new Vector3(FirstLaser.position.x,firstPosY, FirstLaser.position.z);
        SecondLaser.position = new Vector3(SecondLaser.position.x,secondPosY , SecondLaser.position.z);
    }
}
