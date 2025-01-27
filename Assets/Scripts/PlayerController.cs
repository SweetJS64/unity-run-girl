using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float FlyVelocity = 6f;
    private Rigidbody2D _rb;
    private Animator _anim;
    
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
        _anim = GetComponent<Animator>();
    }

    private void Fly()
    {
        _rb.velocity = new Vector2(0, Mathf.Lerp(_rb.velocity.y, FlyVelocity, 0.1f));
        _anim.SetBool("isFly", true);   
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        _anim.SetBool("isFly", false);
    }
}