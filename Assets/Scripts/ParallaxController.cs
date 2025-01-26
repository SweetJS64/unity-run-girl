using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private LayerBackground[] ParallaxLayers = new LayerBackground[4];

    private float _speedBoost = 0; //TODO: when adding game acceleration;
    
    void Start()
    {
        Init();
    }

    void Update()
    {
        MoveBackgroundArray(ParallaxLayers);
    }

    private void Init()
    {
        for (int i = 0; i < ParallaxLayers.Length; i++)
        {
            ParallaxLayers[i].InstantiateLayers();
        }
    }
    
    private void MoveBackgroundArray(LayerBackground[] layerBackgrounds)
    {
        var dt = Time.deltaTime;

        foreach (var layerBackground in layerBackgrounds)
        {
            var speed = (layerBackground.Speed + _speedBoost) * dt;
            var layersArray = layerBackground.GetLayersArray;
            
            for (int j = 0; j < layersArray.Length; j++)
            {
                layersArray[j].transform.Translate( Vector3.left * speed);
                if (layersArray[j].transform.position.x <= -layerBackground.Width)
                {
                    layersArray[j].transform.position = new Vector3(
                        layersArray[j].transform.position.x + 2 * layerBackground.Width,
                        layersArray[j].transform.position.y, 
                        layersArray[j].transform.position.z);
                }
            }
        }
    }
}
