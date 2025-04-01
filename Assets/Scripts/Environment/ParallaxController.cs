using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Material ParallaxMaterialTemplate;
    [SerializeField] private LayerBackground[] LayerConfigs;

    private MeshRenderer[] _meshRenderersArray;
    private float _speedBoost = 1f;
    private bool _needStopping;
    
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        UpdateScrolling();
        if (_needStopping) StopScrolling();
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
            AdaptQuadToScreen(_meshRenderersArray[i].gameObject);
        }
    }
    
    private void AdaptQuadToScreen(GameObject go)
    {
        var cam = Camera.main;
        var height = 2f * cam.orthographicSize;
        var width = height * cam.aspect;

        go.transform.localScale = new Vector3(width, height, 1f);
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
    
    private void UpdateScrolling()
    {
        for (var i = 0; i < _meshRenderersArray.Length; i++)
        {
            var offset = new Vector2(LayerConfigs[i].Speed * _speedBoost * Time.deltaTime, 0);
            _meshRenderersArray[i].material.mainTextureOffset += offset;
        }
    }

    private void StopScrolling()
    {
        _needStopping = true;
        _speedBoost = 0f;
    }
    
    private void SpeedUp(float boost)
    {
        if(_needStopping) return;
        _speedBoost += boost;
    }
}
