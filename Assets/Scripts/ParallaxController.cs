using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Material ParallaxMaterialTemplate;
    [SerializeField] private LayerBackground[] LayerConfigs;
    [SerializeField] private float ParallaxSpeed;

    private MeshRenderer[] _meshRenderersArray;
    private float _speedBoost = 1f;// add abstact class?
    private bool _isSmoothStop;// add abstact class?
    
    void Start()
    {
        Init();
    }

    void Update()
    {
        ParallaxUpdate();
        if (_isSmoothStop) SmoothStop();// add abstact class?
    }

    private void Init()
    {
        _meshRenderersArray = GetComponentsInChildren<MeshRenderer>();
        
        if (_meshRenderersArray.Length != LayerConfigs.Length)
        {
            Debug.LogError("MeshRenderersArray.Length != LayerConfigs.Length");
            return;
        }
        
        for (int i = 0; i < _meshRenderersArray.Length; i++)
        {
            _meshRenderersArray[i].material = ParallaxMaterialTemplate;
            _meshRenderersArray[i].material.mainTexture = LayerConfigs[i].SpriteTexture.texture;
            _meshRenderersArray[i].material.renderQueue = 1000 + i * 10;
        }
    }

    private void ParallaxUpdate()
    {
        for (int i = 0; i < _meshRenderersArray.Length; i++)
        {
            var offset = new Vector2(LayerConfigs[i].Speed * _speedBoost * Time.deltaTime, 0);
            _meshRenderersArray[i].material.mainTextureOffset += offset;
        }
    }
    
    private void OnEnable()
    {
        ObstacleTrigger.OnPlayerHit += SmoothStop;
    }

    private void OnDisable()
    {
        ObstacleTrigger.OnPlayerHit -= SmoothStop;
    }

    private void SmoothStop()// add abstact class?
    {
        _isSmoothStop = true;
        _speedBoost = Mathf.Lerp(_speedBoost, 0, 0.1f);
    }
}
