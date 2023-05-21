using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _enemyPrefab;
    [SerializeField] private Transform _bossPrefab;
    [SerializeField] private float _spawnCooldown = 2.5f;
    private Vector2 _screenBounds;
    private float _timer;
    private bool _active;

    private float _bossSpawnTime = 15f;
    private float _bossTimer;
    private bool _bossSpawned;

    private void Start()
    {
        _screenBounds = ScreenBoundary.GetInstance().GetScreenBounds();
        PlayerHealth.OnPlayerDeath += OnPlayerDeath;
        BossBehaviour.OnBossDeath += OnBossDeath;
        _timer = 0f;
        _bossTimer = 0f;
        _active = true;
        _bossSpawned = false;
    }

    private void Update()
    {
        if (_active)
        {
            _timer += Time.deltaTime;
            
            if (_timer > _spawnCooldown)
            {
                _timer = 0.0f;
                Instantiate(_enemyPrefab, GenerateSpawnPosition(), _enemyPrefab.rotation);
            }
            if (!_bossSpawned)
            {
                _bossTimer += Time.deltaTime;
                if (_bossTimer > _bossSpawnTime)
                {
                    _active = false;
                    _bossSpawned = true;
                    Instantiate(_bossPrefab, new Vector3(0, -_screenBounds.y - 2f, 0), _bossPrefab.rotation);
                }
            }
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        bool isYPositive = UnityEngine.Random.value > 0.5f;
        bool isZPositive = UnityEngine.Random.value > 0.5f;
        Vector3 spawnPos = new Vector3(
            0.0f,
            (isYPositive ?  UnityEngine.Random.Range(1f, _screenBounds.y) : UnityEngine.Random.Range(-1f, -_screenBounds.y)),
            (isZPositive ?  UnityEngine.Random.Range(1f, _screenBounds.x) : UnityEngine.Random.Range(-1f, -_screenBounds.x))
        );

        if (UnityEngine.Random.value < 0.5f)
        {
            spawnPos.y = (spawnPos.y > 0.0f) ? _screenBounds.y + 1.5f : -_screenBounds.y - 1.5f;
        }
        else
        {
            spawnPos.z = (spawnPos.z > 0.0f) ? _screenBounds.x + 1.5f : -_screenBounds.x - 1.5f;
        }
        return spawnPos;
    }

    private void OnPlayerDeath(object sender, EventArgs empty)
    {
        _active = false;
    }

    private void OnBossDeath(object sender, EventArgs empty)
    {
        _timer = 0f;
        _bossTimer = 0f;
        _bossSpawnTime += 15f;
        _bossSpawned = false;
        _spawnCooldown = Mathf.Max(_spawnCooldown - 0.5f, 1.5f);
        _active = true;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= OnPlayerDeath;
        BossBehaviour.OnBossDeath -= OnBossDeath;
    }
}
