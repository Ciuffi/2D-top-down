using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : ProjectileController {

    void Awake() {
        projectileAttribute = new FireballAttribute();
        _rb = GetComponent<Rigidbody2D>();
    }
}
