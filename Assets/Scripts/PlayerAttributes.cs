using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttributes : MonoBehaviour {

    public int maxHealth;
    public int maxStamina;
    public float healthRegenRate = 0;
    public float staminaRegenRate = 0.25f;
    public float staminaRegenDelay = 0.25f;
    public float attackSpeed = 0.1f;

    public Slider healthSlider;
    public Slider staminaSlider;

    
    private int _health;
    private int _stamina;
    private float _healthRegenTimer;
    private float _staminaRegenTimer;
    private float _staminaRegenDelayTimer;
    private float _attackSpeedTimer;

    void Awake() {
        _health = maxHealth;
        _stamina = maxStamina;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
    }
    
    
    
    
    void Update() {
        RegenHealth();
        RegenStamina();
        AttackTimer();
        
        healthSlider.value = _health;
        staminaSlider.value = _stamina;
    }


    void RegenHealth() {
        if (_health < maxHealth && _healthRegenTimer <= 0) _healthRegenTimer = healthRegenRate;
        if (_healthRegenTimer > 0) _healthRegenTimer -= Time.deltaTime;
        
        if (_healthRegenTimer <= 0) {
            _health++;
            _healthRegenTimer = 0;
        }

        if (_health > maxHealth) _health = maxHealth;
    }
    
    
    void RegenStamina() {
        if (_staminaRegenDelayTimer > 0) _staminaRegenDelayTimer -= Time.deltaTime;
        if (_staminaRegenDelayTimer > 0) return;
        if (_staminaRegenDelayTimer < 0) _staminaRegenDelayTimer = 0;
        
        if (_stamina < maxStamina && _staminaRegenTimer <= 0) _staminaRegenTimer = staminaRegenRate;
        if (_staminaRegenTimer > 0) _staminaRegenTimer -= Time.deltaTime;
        
        if (_staminaRegenTimer <= 0) {
            _stamina++;
            _staminaRegenTimer = 0;
        }

        if (_stamina > maxStamina) _stamina = maxStamina;
    }


    void AttackTimer() {
        if (_attackSpeedTimer > 0) _attackSpeedTimer -= Time.deltaTime;
        if (_attackSpeedTimer < 0) _attackSpeedTimer = 0;
    }
    
    
    

    public void DecreaseHealth(int amount) {
        _health -= amount;
        // TODO handle death
    }
    
    
    
    public void DecreaseStamina(int amount) {
        _stamina -= amount;
        _staminaRegenDelayTimer = staminaRegenDelay;
        if (_stamina < 0) _stamina = 0;
    }
    
    
    
    public bool HasXStamina(int amount) {
        return _stamina >= amount;
    }


    public void Attack() {
        _attackSpeedTimer = attackSpeed;
    }


    public bool CanAttack() {
        return _attackSpeedTimer <= 0;
    }
}
