using UnityEngine;

public class BordersData : MonoBehaviour
{
    public static BordersData Instance { get; private set; }
    
    [SerializeField] private BoxCollider2D BorderBottom;
    public float MaxX { get; private set; }
    public float MinX { get; private set; }
    public float MaxY { get; private set; }
    public float MinY { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            CalculateBorders();
        }
        else
        {
            Debug.LogError("BorderData not singleton");
        }
    }
    
    private void CalculateBorders()
    {
        var positiveAngle = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0));
        MaxX = positiveAngle.x;
        MinX = -positiveAngle.x;
        MaxY = positiveAngle.y;
        var posColliderY = BorderBottom.transform.position.y;
        MinY = BorderBottom.size.y / 2 + posColliderY;
    }
}
