using System;
using UnityEngine;
using UnityEngine.UI;

public class DistanceTracker : MonoBehaviour
{
    [SerializeField] private float Speed = 7f; 
    [SerializeField] private float SpeedBoostStep = 0.1f; 
    [SerializeField] private float DistanceStep = 50f;
    
    private Text _distanceText;
    private float _distanceTravelled;
    private float _lastDistanceForStep;
    private float _speedBoost = 1f;
    private bool _stopTracker;

    public static event Action<float>  GameSpeedUp;
    public static event Action<int>  SetScore;
    
    private void Awake()
    {
        Init();
    }
    
    private void Update()
    {
        UpdateDistance();
    }

    private void OnEnable()
    {
        ObstacleTrigger.OnPlayerHit += StopTracker;
    }

    private void OnDisable()
    {
        ObstacleTrigger.OnPlayerHit -= StopTracker;
    }
    
    private void Init()
    {
        _distanceText = GetComponent<Text>();
    }
    
    private void UpdateDistance()
    {
        if (_stopTracker) return;
        _distanceTravelled += Speed * _speedBoost * Time.deltaTime;
        _distanceText.text = $"{(int)_distanceTravelled} M.";
        
        var distanceForStep = (int)(_distanceTravelled / DistanceStep) * DistanceStep;
        
        if (distanceForStep > _lastDistanceForStep)
        {
            SpeedUp();
            _lastDistanceForStep = distanceForStep;
            DistanceStep *= 1.05f;
        }
    }

    private void SpeedUp()
    {
        _speedBoost += SpeedBoostStep;
        GameSpeedUp?.Invoke(SpeedBoostStep);
    }

    private void StopTracker()
    {
        _stopTracker = true;
        SetScore?.Invoke((int)_distanceTravelled);
    }
}