using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LaserObsacleInJob : MonoBehaviour
{
    [SerializeField] private float LifeTime = 4f;
    [SerializeField] private float ActiveScaleY = 1f;
    
    private float _finishScaleY;
    
    private bool _inJob;
    private bool _isScalingDown;
    
    public static event Action LaserEndJob;

    private void Update()
    {
        if (_inJob) ScaleUpdate(_finishScaleY);
    }
    
    private void OnEnable()
    {
        LaserGeneratorMover.StartLaserJob += StartLaserJob;
        _isScalingDown = false;
        transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
        _finishScaleY = ActiveScaleY;
    }

    private void OnDisable()
    {
        LaserGeneratorMover.StartLaserJob -= StartLaserJob;
    }
    
    private void StartLaserJob()
    {
        _inJob = true;
    }

    private void ScaleUpdate(float finishScaleY)
    {
        transform.localScale = new Vector3(
            transform.localScale.x, 
            Mathf.Lerp(transform.localScale.y, finishScaleY, 2.5f * Time.deltaTime), 
            transform.localScale.z);

        var onFinish = Mathf.Abs(transform.localScale.y - finishScaleY) < 0.01f;
        
        if (onFinish && _isScalingDown)
        {
            LaserEndJob?.Invoke();
            _inJob = false;
            return;
        }
        if (onFinish) StartCoroutine(WaitPause(LifeTime));
    }
    
    private IEnumerator WaitPause(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _finishScaleY = 0;
        _isScalingDown = true;
    }
}