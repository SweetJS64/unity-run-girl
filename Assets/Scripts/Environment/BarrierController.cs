using UnityEngine;

public class BarrierController : MonoBehaviour, IScrollingObject
{
    [SerializeField] private float TransformMove = 1.8f;
    
    private Transform[] _barriersTransformArray;
    private float _speedBoost = 1f;
    private bool _isSmoothStop;
    
    void Start()
    {
        Init();
    }
    
    void Update()
    {
        UpdateScrolling();
        if (_isSmoothStop) StopScrolling();
    }

    private void Init()
    {
        _barriersTransformArray = GetComponentsInChildren<Transform>();
    }
    
    private void OnEnable()
    {
        ObstacleTrigger.OnPlayerHit += StopScrolling;
    }

    private void OnDisable()
    {
        ObstacleTrigger.OnPlayerHit -= StopScrolling;
    }

    public void UpdateScrolling()
    {
        for (int i = 0; i < _barriersTransformArray.Length; i++)
        {
            var offset = new Vector3(-TransformMove * _speedBoost * Time.deltaTime, 0, 0);
            _barriersTransformArray[i].position += offset;
        }
    }
    
    public void StopScrolling()
    {
        _isSmoothStop = true;
        _speedBoost = Mathf.Lerp(_speedBoost, 0, 0.1f);
    }
}
