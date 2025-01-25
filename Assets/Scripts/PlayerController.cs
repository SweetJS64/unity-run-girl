using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float FlyVelocity = 6f;
    
    private Rigidbody2D _rb;
    
    void Start()
    {
        Init();
    }
    
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Fly();
        }
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Fly()
    {
        _rb.velocity = new Vector2(0, FlyVelocity);
    }
}