using System;
using UnityEngine;

public class LaserGeneratorController : MonoBehaviour
{
    [SerializeField] private float SpeedGenerators;
    [SerializeField] private LaserObsacleInJob Laser;
    
    private float _widthSprite;
    
    private float _startPosX;
    private float _stopPosX;
    
    private bool _rightSide;
    private int _direction;
    private float _borderX;

    private bool _laserInJob;
    private bool _laserFinished;
    
    public static event Action DisableLaserObstacle;
    public static event Action StartLaserJob;
    
    private void Awake()
    {
        Init();
    }
    
    private void Update()
    {
        if (!_laserInJob)
        {
            MoveGenerator(_stopPosX);
            if (Mathf.Abs(transform.position.x - _stopPosX) < 0.01f)
            {
                StartLaserJob.Invoke();
                _laserInJob = true;
            }
        }
        if (_laserInJob && _laserFinished)
        {
            MoveGenerator(_startPosX);
            if (Mathf.Abs(transform.position.x - _startPosX) < 0.01f)
            {
                DisableLaserObstacle.Invoke();
                _laserInJob = false;
            }
        }
        
    }

    private void OnEnable()
    {
        LaserObsacleInJob.LaserEndJob += Deactivate;
        _laserInJob = false;
        _laserFinished = false;
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

    private void MoveGenerator(float targetPosX)
    {
        if (Mathf.Abs(transform.position.x - targetPosX) < 0.01f) return;
        
        var offset = new Vector3(
            Mathf.Lerp(transform.position.x, targetPosX, 2.5f * Time.deltaTime), 
            transform.position.y, 
            0f);
        transform.position = offset;
    }

    private void Deactivate()
    {
        _laserFinished = true;
    }
}