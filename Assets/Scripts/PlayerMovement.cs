using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed = 5f;
    public float dashSpeed = 5f;
    public float dashDuration = 0.1f;
    public float dashCooldown = 0.1f;
    
    public Rigidbody2D rb;
    public Animator animator;
    public PlayerAttributes playerAttributes;
    
    private Vector2 _movement;
    private float _dashTimer;
    private float _dashCooldownTimer = 0f;
    private bool _dashed;
    private Vector2 _prevMovement = new Vector2(0, -1);
    
    void Awake() {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
        if (playerAttributes == null) playerAttributes = GetComponent<PlayerAttributes>();
    }


    
    void Update() {
        HandleMove();    
        HandleDash();
        
        if (Input.GetKeyDown(KeyCode.H))
            playerAttributes.DecreaseHealth(10);
    }

    

    void HandleMove() {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        if (_movement != Vector2.zero) {
            _prevMovement = _movement;
            animator.SetFloat("PrevHorizontal", _prevMovement.x);
            animator.SetFloat("PrevVertical", _prevMovement.y);
        }

        animator.SetFloat("Horizontal", _movement.x);
        animator.SetFloat("Vertical", _movement.y);
        animator.SetFloat("Speed", _movement.sqrMagnitude);
    }

    
    
    void HandleDash() {
        if (Input.GetKeyDown(KeyCode.Space) && _dashTimer <= 0 && _dashCooldownTimer <= 0 && playerAttributes.HasXStamina(1)) {
            _dashed = true;
            _dashTimer = dashDuration;
            playerAttributes.DecreaseStamina(1);
            animator.SetBool("Dashing", _dashed);
        }
        
        if (_dashTimer > 0) _dashTimer -= Time.deltaTime;
        if (_dashTimer <= 0 && _dashed) {
            _dashTimer = 0;
            _dashed = false;
            _dashCooldownTimer = dashCooldown;
            animator.SetBool("Dashing", _dashed);
        }

        if (_dashCooldownTimer > 0) _dashCooldownTimer -= Time.deltaTime;
        if (_dashCooldownTimer <= 0) _dashCooldownTimer = 0;
    }

    
    
    void FixedUpdate() {
        float speed = moveSpeed;
        Vector2 direction = _movement;

        if (_dashTimer > 0) {
            speed = dashSpeed;
            direction = _prevMovement;
        }
        
        rb.MovePosition(rb.position + Time.fixedDeltaTime * speed * direction);
    }
}
