using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int _maxHealth = 5;
    private int _currentHealth = 5;
    private float _invincibleTime = 1.0f;
    private float _invincibleCooldown = 0.0f;

    public static EventHandler OnHealthChanged;

    private void Update()
    {
        if (_invincibleCooldown > 0.0f)
        {
            _invincibleCooldown -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_invincibleCooldown <= 0.0f && other.gameObject.tag == "Enemy")
        {
            _invincibleCooldown = _invincibleTime;
            _currentHealth--;
            OnHealthChanged?.Invoke(this, EventArgs.Empty);
            if (_currentHealth == 0)
            {
                Destroy(gameObject);
            }
        }    
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }
}
