using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float FlyVelocity = 6f;
    [SerializeField] private GameObject FlyMagiceffect;
    private Rigidbody2D _rb;
    private Animator _anim;
    private bool _die;
    
    void Start()
    {
        Init();
    }
    
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) || Input.touchCount > 0) Fly();
        //if (Input.GetKeyUp(KeyCode.Space)) FlyMagiceffect.SetActive(false);
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        FlyMagiceffect.SetActive(false);
    }

    private void Fly()
    {
        if (_die) return;
        FlyMagiceffect.SetActive(true);
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
        FlyMagiceffect.SetActive(false);
    }

    private void DiePlayer()
    {
        FlyMagiceffect.SetActive(false);
        _anim.SetBool("isDie", true);
        _die = true;
    }

}