using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropManager : MonoBehaviour
{
    [SerializeField] private Transform _watermelonPrefab;

    private int deathCount = 0;

    private void Start()
    {
        EnemyBehaviour.OnEnemyDeathByPlayer += OnEnemyDeath;
    }

    private void OnEnemyDeath(object sender, EventArgs empty)
    {
        deathCount++;
        if (deathCount > 3 && UnityEngine.Random.value > 0.5f)
        {
            deathCount = 0;
            EnemyBehaviour enemy = (EnemyBehaviour) sender;
            Instantiate(_watermelonPrefab, enemy.transform.position, _watermelonPrefab.rotation);
        }
    }
}
