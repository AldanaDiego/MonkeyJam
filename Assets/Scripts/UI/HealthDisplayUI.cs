using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayUI : MonoBehaviour
{
    //[SerializeField] private Texture _fullHeartTexture;
    [SerializeField] private Texture _emptyHeartTexture;
    [SerializeField] private RawImage[] _hearts;

    private void Start()
    {
        PlayerHealth.OnHealthChanged += OnPlayerHealthChanged;
    }

    private void OnPlayerHealthChanged(object sender, EventArgs empty)
    {
        PlayerHealth playerHealth = (PlayerHealth) sender;
        _hearts[playerHealth.GetCurrentHealth()].texture = _emptyHeartTexture;
    }
}
