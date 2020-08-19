using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public GameObject projectile;

    private PlayerAttributes _playerAttributes;



    void Awake() {
        _playerAttributes = GetComponent<PlayerAttributes>();
    }
    
    
    
    void Update() {
        if (Input.GetMouseButton(0) && _playerAttributes.CanAttack()) {
            Attack();
            _playerAttributes.Attack();
        }
    }



    void Attack() {
        GameObject proj = Instantiate(projectile);
        proj.transform.position = gameObject.transform.position;
        ProjectileController projectileControllerController = proj.GetComponent<ProjectileController>();
        
        Vector2 direction = GetAttackDirection();
        projectileControllerController.SetDirection(direction);
    }



    Vector2 GetAttackDirection() {
        Vector2 playerPos = gameObject.transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mousePos - playerPos).normalized;
    }
}
