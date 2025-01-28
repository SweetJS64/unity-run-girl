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
        if (Input.GetKey(KeyCode.Space) || Input.touchCount > 0)
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
    
    private void OnEnable()
    {
        FloorTrigger.PlayerOnFloor += OnFloor;
        ObstacleTrigger.OnPlayerHit += DiePlayer;
    }

    private void OnDisable()
    {
        FloorTrigger.PlayerOnFloor -= OnFloor;
        ObstacleTrigger.OnPlayerHit -= DiePlayer;
    }

    private void OnFloor()
    {
        _anim.SetBool("isFly", false);
        Debug.Log("you on floor");
    }

    private void DiePlayer()
    {
        _anim.SetBool("isDie", true);
        Debug.Log("you die");
    }

}