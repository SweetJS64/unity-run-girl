using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MaxFlyVelocity = 6f;
    
    private float _flyVelocity = 0.5f;
    private Rigidbody2D _rb;
    private Animator _anim;
    private bool _isJumping;
    
    
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
        else
        {
            _flyVelocity = 0.5f;
        }
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Fly()
    {
        if (_flyVelocity < MaxFlyVelocity) _flyVelocity += 0.2f;
        _rb.velocity = new Vector2(0, _flyVelocity);
        _anim.SetBool("isJump", true);
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        _anim.SetBool("isJump", false);
    }
}