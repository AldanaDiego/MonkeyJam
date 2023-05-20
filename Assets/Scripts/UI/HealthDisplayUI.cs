using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayUI : MonoBehaviour
{
    [SerializeField] private Texture _fullHeartTexture;
    [SerializeField] private Texture _emptyHeartTexture;
    [SerializeField] private RawImage[] _hearts;

    private void Start()
    {
        PlayerHealth.OnHealthChanged += OnPlayerHealthChanged;
    }

    private void OnPlayerHealthChanged(object sender, int previousHealth)
    {
        PlayerHealth playerHealth = (PlayerHealth) sender;
        int currentHealth = playerHealth.GetCurrentHealth();
        if (currentHealth > previousHealth) //Healed
        {
            for (int i = previousHealth; i < currentHealth; i++)
            {
                _hearts[i].texture = _fullHeartTexture;
            }
        }
        else
        {
            for (int i = currentHealth; i < previousHealth; i++)
            {
                _hearts[i].texture = _emptyHeartTexture;
            }
        }
    }
}
