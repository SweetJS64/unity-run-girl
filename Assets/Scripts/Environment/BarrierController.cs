using UnityEngine;

public class BarrierController : MonoBehaviour
{
    [SerializeField] private float TransformMove = 1.8f;
    
    private Transform[] _barriersTransformArray;
    private float _speedBoost = 1f;// add abstact class?
    private bool _isSmoothStop;// add abstact class?
    
    void Start()
    {
        Init();
    }
    
    void Update()
    {
        MoveBarrier();
        if (_isSmoothStop) SmoothStop();// add abstact class?
    }

    private void Init()
    {
        _barriersTransformArray = GetComponentsInChildren<Transform>();
    }

    private void MoveBarrier()
    {
        for (int i = 0; i < _barriersTransformArray.Length; i++)
        {
            var offset = new Vector3(-TransformMove * _speedBoost, 0, 0);
            _barriersTransformArray[i].position += offset * Time.deltaTime;
        }
    }
    
    private void OnEnable()
    {
        ObstacleTrigger.OnPlayerHit += SmoothStop;
    }

    private void OnDisable()
    {
        ObstacleTrigger.OnPlayerHit -= SmoothStop;
    }

    private void SmoothStop()// add abstact class?
    {
        _isSmoothStop = true;
        _speedBoost = Mathf.Lerp(_speedBoost, 0, 0.1f);
    }
}
