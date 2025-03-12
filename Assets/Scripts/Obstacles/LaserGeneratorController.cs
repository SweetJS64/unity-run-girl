using UnityEngine;

public class LaserGeneratorController : MonoBehaviour
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
    
    private void Update()
    {
        MoveGenerator();
    }

    private void OnEnable()
    {
        _finishPosX = _stopPosX;
        LaserObsacleInJob.LaserEndJob += Deactivate;
    }

    private void OnDisable()
    {
        LaserObsacleInJob.LaserEndJob -= Deactivate;
    }
    
    private void Init()
    {
        _widthSprite = GetComponent<SpriteRenderer>().bounds.size.x;
        _rightSide = transform.localScale.z > 0;
        _direction = _rightSide ? -1 : 1;
        _borderX = _rightSide ? BordersData.Instance.MaxX : BordersData.Instance.MinX;
        _startPosX = _borderX - _widthSprite * _direction;
        _stopPosX = _borderX + _widthSprite * _direction;
        transform.position = new Vector3(_startPosX, transform.position.y, transform.position.z);
    }

    private void MoveGenerator()
    {
        var offset = new Vector3(
            Mathf.Lerp(transform.position.x, _finishPosX, 0.05f), 
            transform.position.y, 
            0f);
        transform.position = offset;
    }

    private void Deactivate()
    {
        _finishPosX = _startPosX;
    }
}