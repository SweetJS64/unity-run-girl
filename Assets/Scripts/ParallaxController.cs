using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Material ParallaxMaterialTemplate;
    [SerializeField] private LayerBackground[] LayerConfigs;
    [SerializeField] private float ParallaxSpeed;
    //[SerializeField] private float SpeedBoost; //TODO: when adding game acceleration;
    private MeshRenderer[] _meshRenderersArray;
    
    void Start()
    {
        Init();
    }

    void Update()
    {
        ParallaxUpdate();
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
            _meshRenderersArray[i].material.renderQueue = 3000 + i * 10;
        }
    }

    private void ParallaxUpdate()
    {
        for (int i = 0; i < _meshRenderersArray.Length; i++)
        {
            var offset = new Vector2(LayerConfigs[i].Speed * Time.deltaTime, 0);
            _meshRenderersArray[i].material.mainTextureOffset += offset;
        }
    }
}
