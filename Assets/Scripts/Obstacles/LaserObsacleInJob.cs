using System;
using System.Collections;
using UnityEngine;

public class LaserObsacleInJob : MonoBehaviour
{
    [SerializeField] private float MoveTime = 2f;
    [SerializeField] private float LifeTime = 4f;

    [SerializeField] private float ActiveScaleY = 1f;
    private float _inactiveScaleY;
    private float _finishScaleY;
    private bool _isCompleted;
    
    public static event Action  LaserEndJob;
    void Update()
    {
        ScaleUpdate();
    }

    private void OnEnable()
    {
        _isCompleted = false;
        transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
        StartCoroutine(WaitPause(MoveTime));
    }

    private void ScaleUpdate()
    {
        transform.localScale = new Vector3(
            transform.localScale.x, 
            Mathf.Lerp(transform.localScale.y, _finishScaleY, 0.1f), 
            transform.localScale.z);
    }

    private void SetScaleLaser()
    {
        _finishScaleY = _finishScaleY == 0 ? ActiveScaleY : _inactiveScaleY;
        _isCompleted = _finishScaleY == 0;
        if (_isCompleted)
        {
            LaserEndJob?.Invoke();
            return;
        }
        StartCoroutine(WaitPause(LifeTime));
    }
    
    IEnumerator WaitPause(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (!_isCompleted) SetScaleLaser();
    }
}