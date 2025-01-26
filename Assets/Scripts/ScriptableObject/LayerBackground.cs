using UnityEngine;

[CreateAssetMenu(fileName = "CustomParallaxConfiguration", menuName = "Configurations/ParallaxConfiguration")]
public class LayerBackground : ScriptableObject
{
    [SerializeField] private GameObject PrefabBg;
    public Vector2 Position = new Vector2(0, 0);
    public float Speed = 1;

    private GameObject _firstBg;
    private GameObject _secondBg;
    private float _width;
    
    public void InstantiateLayers()
    {
        _width = PrefabBg.GetComponent<SpriteRenderer>().bounds.size.x;
        _firstBg = Instantiate(PrefabBg, new Vector3(Position.x, Position.y, 0), Quaternion.identity);
        _secondBg = Instantiate(PrefabBg, new Vector3(Position.x + _width, Position.y, 0), Quaternion.identity);
    }
    
    //getSetNeParasha
    public float Width => _width;

    public GameObject[] GetLayersArray 
    {
        get { return new[] { _firstBg, _secondBg }; }
    }
}