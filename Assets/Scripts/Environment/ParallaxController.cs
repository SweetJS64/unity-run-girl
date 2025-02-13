using UnityEngine;

public class ParallaxController : MonoBehaviour, IScrollingObject
{
    [SerializeField] private Material ParallaxMaterialTemplate;
    [SerializeField] private LayerBackground[] LayerConfigs;
    [SerializeField] private float ParallaxSpeed;

    private MeshRenderer[] _meshRenderersArray;
    private float _speedBoost = 1f;
    private bool _isSmoothStop;
    
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        UpdateScrolling();
        if (_isSmoothStop) StopScrolling();
    }

    private void Init()
    {
        _meshRenderersArray = GetComponentsInChildren<MeshRenderer>();
        
        if (_meshRenderersArray.Length != LayerConfigs.Length)
        {
            Debug.LogError("MeshRenderersArray.Length != LayerConfigs.Length");
            return;
        }
        
        for (var i = 0; i < _meshRenderersArray.Length; i++)
        {
            _meshRenderersArray[i].material = ParallaxMaterialTemplate;
            _meshRenderersArray[i].material.mainTexture = LayerConfigs[i].SpriteTexture.texture;
            _meshRenderersArray[i].material.renderQueue = 1000 + i * 10;
        }
    }
    
    private void OnEnable()
    {
        ObstacleTrigger.OnPlayerHit += StopScrolling;
        DistanceTracker.GameSpeedUp += SpeedUp;
    }

    private void OnDisable()
    {
        ObstacleTrigger.OnPlayerHit -= StopScrolling;
        DistanceTracker.GameSpeedUp -= SpeedUp;
    }
    
    public void UpdateScrolling()
    {
        for (var i = 0; i < _meshRenderersArray.Length; i++)
        {
            var offset = new Vector2(LayerConfigs[i].Speed * _speedBoost * Time.deltaTime, 0);
            _meshRenderersArray[i].material.mainTextureOffset += offset;
        }
    }

    public void StopScrolling()
    {
        _isSmoothStop = true;
        _speedBoost = Mathf.Lerp(_speedBoost, 0, 0.1f);
    }
    
    private void SpeedUp(float boost)
    {
        _speedBoost += boost;
    }
}
