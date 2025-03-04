using UnityEngine;

public class LaserGeneratorAppearance : MonoBehaviour
{
    private float _widthSprite;
    private float _startPosX;
    private float _stopPosX;
    private bool _rightSide;
    private int _direction;
    private float _borderX;

    private float _finishPosX;
    
    void Awake()
    {
        Init();
    }

    private void Init()
    {
        _widthSprite = GetComponent<SpriteRenderer>().bounds.size.x;
        _rightSide = transform.localScale.z > 0;
        _direction = _rightSide ? -1 : 1;
        _borderX = _rightSide ? BordersDataSingleton.Instance.MaxX : BordersDataSingleton.Instance.MinX;
        _startPosX = _borderX - _widthSprite * _direction;
        _stopPosX = _borderX + _widthSprite * _direction;
        transform.position = new Vector3(_startPosX, transform.position.y, transform.position.z);
    }

    private void OnEnable()
    {
        _finishPosX = _stopPosX;
        LaserObsacleInJob.LaserEndJob += Dissapearance;
    }

    private void OnDisable()
    {
        LaserObsacleInJob.LaserEndJob -= Dissapearance;
    }

    private void Update()
    {
        MoveObstacle();
    }

    private void MoveObstacle()
    {
        var offset = new Vector3(
            Mathf.Lerp(transform.position.x, _finishPosX, 0.05f), 
            transform.position.y, 
            0f);
        transform.position = offset;
    }

    private void Dissapearance()
    {
        _finishPosX = _startPosX;
    }
}