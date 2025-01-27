using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    //[SerializeField] private float SpeedBoost = 0f; //TODO: when adding game acceleration;
    [SerializeField] private Material MaterialForParallax;
    [SerializeField] private LayerBackground[] ConfigsLayers = new LayerBackground[5];
    private MeshRenderer[] _meshRenderersArray;
    
    private Vector2 _offset;
    void Start()
    {
        Init();
    }

    void Update()
    {
        ScrollingLayers();
    }

    private void Init()
    {
        _meshRenderersArray = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < _meshRenderersArray.Length; i++)
        {
            _meshRenderersArray[i].material = MaterialForParallax;
            _meshRenderersArray[i].material.mainTexture = ConfigsLayers[i].SpriteTexture.texture;
            _meshRenderersArray[i].material.renderQueue = i;
        }
    }

    private void ScrollingLayers()
    {
        for (int i = 0; i < _meshRenderersArray.Length; i++)
        {
            var offset = new Vector2 (_meshRenderersArray[i].material.mainTextureOffset.x + 
                                      ConfigsLayers[i].Speed * Time.deltaTime, 0);
            _meshRenderersArray[i].material.mainTextureOffset = offset;
        }
    }
}
