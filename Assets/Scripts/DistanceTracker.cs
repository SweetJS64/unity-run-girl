using System;
using UnityEngine;
using UnityEngine.UI;

public class DistanceTracker : MonoBehaviour
{
    //[SerializeField] private Text distanceText;
    [SerializeField] private float Speed = 7f; 
    [SerializeField] private float SpeedBoostStep = 0.1f; 
    [SerializeField] private int DistanceForBoost = 100;
    
    private Text _distanceText;
    private float _distanceTravelled;
    private float _speedBoost;

    public static event Action<float>  GameSpeedUp;
    
    private void Awake()
    {
        Init();
    }
    
    private void Update()
    {
        UpdateDistance();
    }

    void Init()
    {
        _distanceText = GetComponent<Text>();
    }

private void UpdateDistance()
    {
        _distanceTravelled += Speed * Time.deltaTime;
        _distanceText.text = $"{(int)_distanceTravelled} M.";

        if ((int)_distanceTravelled % DistanceForBoost == 0 && _distanceTravelled >= DistanceForBoost)
        {
            SpeedUp();
        }
    }

    private void SpeedUp()
    {
        _speedBoost += SpeedBoostStep;
        GameSpeedUp?.Invoke(SpeedBoostStep);
        //Debug.Log("SpeedUp");
    }
}
