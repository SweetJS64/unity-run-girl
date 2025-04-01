using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [SerializeField] private float SpeedMove = 3.6f;
    [SerializeField] private float BaseCameraWidth = 17.78f;
    
    private ObstaclesManager _obstaclesManager;
    private bool _needStopping;
    private float _heightSprite;
    private static float _speedBoost = 1f;
    
    private float _currentWidth;
    private float _currentHeight;
    
    private Camera _camera;
    
    private void Awake()
    {
        Init();
    }
    
    private void Update()
    {
        if (_needStopping)
        {
            StopScrolling();
        }
        else
        {
            _speedBoost = _obstaclesManager.SpeedBoost;
        }
        UpdateScrolling(_speedBoost);
    }
    
    private void OnEnable()
    {
        ObstacleTrigger.OnPlayerHit += StopScrolling;
    }

    private void OnDisable()
    {
        ObstacleTrigger.OnPlayerHit -= StopScrolling;
    }

    private void Init()
    {
        _camera = Camera.main;  
        _currentHeight = 2f * _camera.orthographicSize;
        _currentWidth = _currentHeight * _camera.aspect;
        SpeedMove = 3.6f * (_currentWidth / BaseCameraWidth);
        _obstaclesManager = GetComponentInParent<ObstaclesManager>();
        //inParent - bad/ set value from manager
        _heightSprite = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void UpdateScrolling(float speedBoost)
    {
        var minX = BordersData.Instance.MinX - _heightSprite / 2;
        if (transform.position.x < minX)
        {
            gameObject.SetActive(false);
            return;
        }

        var offset = new Vector3(SpeedMove * speedBoost * Time.deltaTime, 0, 0);
        transform.position -= offset;
    }
    
    private void StopScrolling()
    {
        _needStopping = true;
        _speedBoost = 0f;
    }
}
