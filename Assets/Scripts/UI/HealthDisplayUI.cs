using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayUI : MonoBehaviour
{
    [SerializeField] private Texture _fullHeartTexture;
    [SerializeField] private Texture _emptyHeartTexture;
    [SerializeField] private RawImage _heartPrefab;
    private List<RawImage> _hearts;
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
        _hearts = new List<RawImage>();
        PlayerHealth.OnHealthChanged += OnPlayerHealthChanged;
        for (int i = 0; i < 5; i++)
        {
            _hearts.Add(Instantiate(_heartPrefab));
            _hearts[i].transform.SetParent(_transform);
        }
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

    private void OnDisable()
    {
        PlayerHealth.OnHealthChanged -= OnPlayerHealthChanged;
    }
}
