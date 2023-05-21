using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int _score;

    private void Start()
    {
        EnemyBehaviour.OnEnemyDeathByPlayer += OnEnemyDeath;
        BossBehaviour.OnBossDeath += OnBossDeath;
        ItemDrop.OnItemHeal += OnItemHeal;
        _score = 0;
        PlayerPrefs.SetInt("Score", _score);
        _scoreText.text = "Score: 0";
    }

    private void OnEnemyDeath(object sender, EventArgs empty)
    {
        _score++;
        PlayerPrefs.SetInt("Score", _score);
        _scoreText.text = "Score: " + _score;
    }

    private void OnItemHeal(object sender, int amount)
    {
        _score += (amount == 1) ? 2 : 5;
        PlayerPrefs.SetInt("Score", _score);
        _scoreText.text = "Score: " + _score;
    }

    private void OnBossDeath(object sender, EventArgs empty)
    {
        _score += 3;
        PlayerPrefs.SetInt("Score", _score);
        _scoreText.text = "Score: " + _score;
    }

    private void OnDisable()
    {
        EnemyBehaviour.OnEnemyDeathByPlayer -= OnEnemyDeath;
        ItemDrop.OnItemHeal -= OnItemHeal;
        BossBehaviour.OnBossDeath -= OnBossDeath;
    }
}
