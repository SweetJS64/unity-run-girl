using UnityEngine;

public class ObstacleController : MonoBehaviour, IScrollingObject
{
    [SerializeField] private float TransformMove = 3.6f;
    
    private Camera _cameraMain;
    private Vector3 _spawnPos;
    private Vector3 _stopPos;
    private float _speedBoost = 1f;
    private bool _isSmoothStop;
    private float _widthSprite;
    
    private void Start()
    {
        var childSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _widthSprite = childSpriteRenderers[0].bounds.size.x;
        _cameraMain = Camera.main;
        if (_cameraMain == null)
        {
            Debug.LogError("_cameraMain == null");
            return;
        }
        _spawnPos = _cameraMain.ViewportToWorldPoint(new Vector3(1, 0.5f, 0)) + new Vector3(_widthSprite, 0, 0)/2;
        _stopPos = _cameraMain.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)) - new Vector3(_widthSprite, 0, 0)/2;
        transform.position = new Vector3(_spawnPos.x, transform.position.y, transform.position.z);
    }
    private void Update()
    {
        UpdateScrolling();
        if (_isSmoothStop) StopScrolling();
    }
    
    private void OnEnable()
    {
        ObstacleTrigger.OnPlayerHit += StopScrolling;
    }

    private void OnDisable()
    {
        ObstacleTrigger.OnPlayerHit -= StopScrolling;
        transform.position = new Vector3(_spawnPos.x, transform.position.y, transform.position.z);
    }

    public void UpdateScrolling()
    {
        if (transform.position.x < _stopPos.x)
        {
            gameObject.SetActive(false);
            return;
        }
        
        var offset = new Vector3(-TransformMove * _speedBoost * Time.deltaTime, 0, 0);
        transform.position += offset;
    }
    
    public void StopScrolling()
    {
        _isSmoothStop = true;
        _speedBoost = Mathf.Lerp(_speedBoost, 0, 0.1f);
    }
}
