using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private SoundEffectManager _soundEffectManager;
    private int _maxHealth = 5;
    private int _currentHealth = 5;
    private float _invincibleTime = 1.0f;
    private float _invincibleCooldown = 0.0f;

    public static EventHandler<int> OnHealthChanged;
    public static EventHandler OnPlayerDeath;

    private void Start()
    {
        ItemDrop.OnItemHeal += OnItemHeal;
        _soundEffectManager = SoundEffectManager.GetInstance();
    }

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
            OnHealthChanged?.Invoke(this, _currentHealth + 1);
            
            if (_currentHealth == 0)
            {
                _soundEffectManager.PlayPlayerDeathAudio();
                OnPlayerDeath?.Invoke(this, EventArgs.Empty);
                StartCoroutine("TriggerGameOver");
            }
            else
            {
                _soundEffectManager.PlayDamageAudio();
            }
        }    
    }

    private IEnumerator TriggerGameOver()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        SceneManager.LoadScene("GameOverScene");
    }

    private void OnItemHeal(object sender, int amount)
    {
        int previousHealth = _currentHealth;
        _currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);
        OnHealthChanged?.Invoke(this, previousHealth);
    }

    private void OnDisable()
    {
        ItemDrop.OnItemHeal -= OnItemHeal;    
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
