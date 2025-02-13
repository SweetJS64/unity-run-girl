using UnityEngine;

public class ObstacleMover : MonoBehaviour, IScrollingObject
{
    [SerializeField] private float TransformMove = 3.6f;
    
    private Camera _cameraMain;
    private ObstaclesController _obstaclesController;
    private Vector3 _spawnPos;
    private Vector3 _stopPos;
    private bool _isSmoothStop;
    private float _widthSprite;
    private static float _speedBoost = 1f;
    
    private void Awake()
    {
        Init();
    }
    private void Update()
    {
        UpdateScrolling();
        if (_isSmoothStop) StopScrolling();
    }
    
    private void OnEnable()
    {
        ObstacleTrigger.OnPlayerHit += StopScrolling;
        transform.position = _spawnPos;
    }

    private void OnDisable()
    {
        ObstacleTrigger.OnPlayerHit -= StopScrolling;
    }

    private void Init()
    {
        var childSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _obstaclesController = GetComponentInParent<ObstaclesController>();
        _widthSprite = childSpriteRenderers[0].bounds.size.x;
        //мб тут перемудрил
        _cameraMain = Camera.main;
        if (_cameraMain == null)
        {
            Debug.LogError("_cameraMain == null");
            return;
        }
        _spawnPos = _cameraMain.ViewportToWorldPoint(new Vector3(1, 0.5f, 0)) + new Vector3(_widthSprite, 0, 0)/2;
        _stopPos = -_spawnPos;
        _speedBoost = _obstaclesController.SpeedBoost;
    }

    public void UpdateScrolling()
    {
        if (transform.position.x < _stopPos.x)
        {
            gameObject.SetActive(false);
            return;
        }
        var offset = new Vector3(
            TransformMove * _obstaclesController.SpeedBoost * Time.deltaTime, 
            transform.position.y, 
            transform.position.z);
        transform.position -= offset;
    }
    
    public void StopScrolling()
    {
        _isSmoothStop = true;
        _speedBoost = Mathf.Lerp(_speedBoost, 0, 0.1f);
    }
}
