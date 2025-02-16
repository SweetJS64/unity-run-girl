using UnityEngine;

public class BordersData : MonoBehaviour
{
    [SerializeField] private BoxCollider2D BorderBottom;
    public float MaxX { get; private set; }
    public float MinX { get; private set; }
    public float MaxY { get; private set; }
    public float MinY { get; private set; }
    void Awake()
    {
        var positiveAngle = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0));
        MaxX = positiveAngle.x;
        MinX = -positiveAngle.x;
        MaxY = positiveAngle.y;
        var posColliderY = BorderBottom.transform.position.y;
        MinY = BorderBottom.size.y / 2 + posColliderY;
    }
}
