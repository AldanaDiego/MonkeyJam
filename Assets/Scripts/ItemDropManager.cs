using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropManager : MonoBehaviour
{
    [SerializeField] private Transform _watermelonPrefab;
    [SerializeField] private Transform _bananaPrefab;

    private int deathCount = 0;

    private void Start()
    {
        EnemyBehaviour.OnEnemyDeathByPlayer += OnEnemyDeath;
        BossBehaviour.OnBossDeath += OnBossDeath;
    }

    private void OnEnemyDeath(object sender, EventArgs empty)
    {
        deathCount++;
        if (deathCount > 3 && UnityEngine.Random.value > 0.1f)
        {
            deathCount = 0;
            EnemyBehaviour enemy = (EnemyBehaviour) sender;
            Instantiate(_watermelonPrefab, enemy.transform.position, _watermelonPrefab.rotation);
        }
    }

    private void OnBossDeath(object sender, EventArgs empty)
    {
        BossBehaviour boss = (BossBehaviour) sender;
        Instantiate(_bananaPrefab, boss.transform.position, _bananaPrefab.rotation);
    }

    private void OnDisable()
    {
        EnemyBehaviour.OnEnemyDeathByPlayer -= OnEnemyDeath;
        BossBehaviour.OnBossDeath -= OnBossDeath;
    }
}
