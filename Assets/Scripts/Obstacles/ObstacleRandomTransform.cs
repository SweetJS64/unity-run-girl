using UnityEngine;

public class ObstacleRandomTransform : MonoBehaviour
{
    
    [SerializeField] private float ScaleMinY = 2f;
    [SerializeField] private float ScaleMaxY = 2.8f;
    private Vector3 _scale;
    
    private BordersData _bordersData;
    private Vector3 _topBorder;
    private Vector3 _position;
    private float _hightSprite;

    private static float _minPosY;
    private static float _maxPosY;

    private void Awake()
    {
        _bordersData = GetComponentInParent<BordersData>();
        
        _hightSprite = GetComponent<SpriteRenderer>().bounds.size.y;
        _scale = transform.localScale;
        
        _position = transform.position;
        
        _maxPosY = _bordersData.MaxY;
        _minPosY = _bordersData.MinY;
    }
    private void OnEnable()
    {
        SetScaleY();
        SetPositionY();
    }

    private void SetScaleY()
    {
        _scale.y = Random.Range(ScaleMinY, ScaleMaxY);;
        transform.localScale = _scale;
    }

    private void SetPositionY()
    {
        var minY = _minPosY - _hightSprite / 2;
        var maxY = _maxPosY + _hightSprite / 2;
        _position.x = _bordersData.MaxX + _hightSprite / 2;
        var randomY = Random.Range(minY, maxY);
        Debug.Log(randomY);
        _position.y = randomY;
        Debug.Log(_position.y);
        transform.position = _position;
        Debug.Log(transform.position.y);
        Debug.Log(transform.position);
    }
}
