using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;

    private void Start()
    {
        PlayerHealth.OnHealthChanged += OnPlayerHealthChanged;
        _healthText.text = "HEALTH 5/5";
    }

    private void OnPlayerHealthChanged(object sender, EventArgs empty)
    {
        PlayerHealth playerHealth = (PlayerHealth) sender;
        _healthText.text = "HEALTH " + playerHealth.GetCurrentHealth() + "/" + playerHealth.GetMaxHealth();
    }
}
