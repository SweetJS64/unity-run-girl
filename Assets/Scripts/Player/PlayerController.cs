using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float FlyVelocity = 6f;
    [SerializeField] private GameObject FlyMagicEffect;
    private Rigidbody2D _rb;
    private Animator _anim;
    private ParticleSystem[] _particles;
    private bool _die;
    
    private void Start()
    {
        Init();
    }
    
    private void Update()
    {
        if (_die) return;
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)  FlyEffectsPlay();
    }

    private void FixedUpdate()
    {
        if (_die) return;
        if (Input.GetKey(KeyCode.Space) || Input.touchCount > 0) Fly();
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _particles = FlyMagicEffect.GetComponentsInChildren<ParticleSystem>();
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

    private void Fly()
    {
        _rb.velocity = new Vector2(0, Mathf.Lerp(_rb.velocity.y, FlyVelocity, 0.1f));
    }

    private void FlyEffectsPlay()
    {
        _anim.SetBool("isFly", true);
        foreach (var particle in _particles) particle.Play();
    }

    private void OnFloor()
    {
        _anim.SetBool("isFly", false);
        foreach (var particle in _particles) particle.Stop();
    }

    private void DiePlayer()
    {
        foreach (var particle in _particles) particle.Stop();
        _anim.SetBool("isDie", true);
        _die = true;
    }
}