using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGeneratorAppearance : MonoBehaviour
{
    private BordersData _bordersData;
    private float _widthSprite;
    private float _startPosX;
    private float _stopPosX;
    private bool _rightSide;
    private int _direction;
    
    void Awake()
    {
        _bordersData = GetComponentInParent<BordersData>();
        if (_bordersData == null) Debug.LogError("BordersData is null");
        _widthSprite = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void OnEnable()
    {
        _rightSide = transform.localScale.z > 0;
        Debug.Log($"Name{gameObject.name} rightSide:{_rightSide}");
        //_direction = _rightSide ? -1 : 1;
        if (_rightSide)
        {
            _startPosX = _bordersData.MaxX + _widthSprite;
            _stopPosX = _bordersData.MaxX - _widthSprite;
        }
        else
        {
            _startPosX = _bordersData.MinX - _widthSprite;
            _stopPosX = _bordersData.MinX + _widthSprite;
        }
        transform.position = new Vector3(_startPosX, transform.position.y, transform.position.z);
    }
    void Update()
    {
        var direction = _rightSide ? -1 : 1;
        var offset = new Vector3(
            Mathf.Lerp(transform.position.x, _stopPosX, 0.01f), 
            transform.position.y, 
            0f);
        transform.position = offset;
    }
}
