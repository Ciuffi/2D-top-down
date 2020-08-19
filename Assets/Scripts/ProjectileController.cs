using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
    
    public AbstractProjectileAttribute projectileAttribute;
    
    protected Rigidbody2D _rb;
    
    
    
    private void FixedUpdate() {
        if (_rb == null) return;
        _rb.MovePosition(_rb.position + Time.fixedDeltaTime * projectileAttribute.speed * projectileAttribute.direction);
    }



    public void SetDirection(Vector2 direction) {
        projectileAttribute.direction = direction;
    }


    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
