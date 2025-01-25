using Unity.VisualScripting;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private GameObject Clouds0;
    [SerializeField] private GameObject Clouds1;
    [SerializeField] private GameObject BackHill0;
    [SerializeField] private GameObject BackHill1;
    [SerializeField] private GameObject ForeHill0;
    [SerializeField] private GameObject ForeHill1;
    [SerializeField] private GameObject Ground0;
    [SerializeField] private GameObject Ground1;
    
    private GameObject[] _backgroundArray;
    private Transform[] _transformArray;
    
    private Transform _transformGround0;
    private Transform _transformGround1;
    private float _width;
    
    [SerializeField] private float _speedMove = 1.5f;
    
    void Start()
    {
        Init();
        
        _width = Clouds0.GetComponent<SpriteRenderer>().sprite.rect.width / Clouds0.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;;
    }

    void Update()
    {
        MoveBackgroundArray(_transformArray);
    }

    private void Init()
    {
        _backgroundArray = new []
        {
            Clouds0, 
            Clouds1,
            BackHill0, 
            BackHill1,
            ForeHill0, 
            ForeHill1,
            Ground0, 
            Ground1
        };
        _transformArray = new Transform[_backgroundArray.Length];
        
        for (var i = 0; i < _backgroundArray.Length; i++)
        {
            _transformArray[i] = _backgroundArray[i].GetComponent<Transform>();
        }
    }

    private void MoveBackgroundArray(Transform[] transformArray)
    {
        var dt = Time.deltaTime;
        for (var i = 0; i < transformArray.Length; i++)
        {
            transformArray[i].Translate( Vector3.left * _speedMove * dt);
            if (transformArray[i].position.x <= -_width)
            {
                transformArray[i].position = new Vector3(
                    transformArray[i].position.x + 2 * _width, 
                    transformArray[i].position.y, 
                    transformArray[i].position.z);
            }
        }
    }
}