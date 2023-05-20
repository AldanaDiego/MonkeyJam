using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int _score = 0;

    private void Start()
    {
        EnemyBehaviour.OnEnemyDeathByPlayer += OnEnemyDeath;
        ItemDrop.OnItemHeal += OnItemHeal;
        _scoreText.text = "Score: 0";
    }

    private void OnEnemyDeath(object sender, EventArgs empty)
    {
        _score++;
        _scoreText.text = "Score: " + _score;
    }

    private void OnItemHeal(object sender, int amount)
    {
        _score += (amount == 1) ? 2 : 5;
        _scoreText.text = "Score: " + _score;
    }

    private void OnDisable()
    {
        EnemyBehaviour.OnEnemyDeathByPlayer -= OnEnemyDeath;
        ItemDrop.OnItemHeal -= OnItemHeal;    
    }
}
