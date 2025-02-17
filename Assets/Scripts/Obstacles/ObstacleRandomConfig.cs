using UnityEngine;

public class ObstacleRandomConfig : MonoBehaviour
{
    [SerializeField] private Sprite[] Sprites;
    [SerializeField] private float ScaleMinY = 2f;
    [SerializeField] private float ScaleMaxY = 2.8f;
    private Vector3 _scale;
    
    private BordersData _bordersData;
    private Vector3 _topBorder;
    private Vector3 _position;
    private float _hightSprite;

    private static float _minPosY;
    private static float _maxPosY;
    
    private Vector3 _rotation;
    private static int[] _rotationAngles = { -45, 0, 0, 0, 45 };
    private static int _lastAngle;

    private void Awake()
    {
        Init();
    }
    private void OnEnable()
    {
        SetScaleY();
        SetPosition();
        SetRotation();
        SetSprite();
    }

    private void Init()
    {
        _bordersData = GetComponentInParent<BordersData>();
        _hightSprite = GetComponent<SpriteRenderer>().bounds.size.y;
        _scale = transform.localScale;
        _maxPosY = _bordersData.MaxY;
        _minPosY = _bordersData.MinY;
    }

    private void SetScaleY()
    {
        _scale.y = Random.Range(ScaleMinY, ScaleMaxY);;
        transform.localScale = _scale;
    }

    private void SetPosition()
    {
        var minY = _minPosY + _hightSprite / 2;
        var maxY = _maxPosY - _hightSprite / 2;
        _position.x = _bordersData.MaxX + _hightSprite / 2;
        _position.y = Random.Range(minY, maxY);
        transform.position = _position;
    }

    private void SetRotation()
    {
        var angle = _rotationAngles[Random.Range(0, _rotationAngles.Length)];
        if (_lastAngle == angle) angle = -angle;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        _lastAngle = angle;
    }

    private void SetSprite()
    {
        var sprite = Sprites[Random.Range(0, Sprites.Length)];
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
