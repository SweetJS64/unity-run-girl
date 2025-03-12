using UnityEngine;

public class ObstacleMover : MonoBehaviour/*, IScrollingObject*/
{
    [SerializeField] private float SpeedMove = 3.6f;
    
    private ObstaclesManager _obstaclesController;
    private bool _needStopping;
    private float _heightSprite;
    private static float _speedBoost = 1f;
    
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
            _speedBoost = _obstaclesController.SpeedBoost;
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
        _obstaclesController = GetComponentInParent<ObstaclesManager>();
        _heightSprite = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void UpdateScrolling(float speedBoost)
    {
        var minX = BordersData.Instance.MinX - _heightSprite;
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
        _speedBoost = Mathf.Lerp(_speedBoost, 0, 0.1f);
    }
}
