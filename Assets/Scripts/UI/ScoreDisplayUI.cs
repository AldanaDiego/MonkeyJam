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
        _scoreText.text = "Fish x 0";
    }

    private void OnEnemyDeath(object sender, EventArgs empty)
    {
        _score++;
        _scoreText.text = "Fish x " + _score;
    }
}
