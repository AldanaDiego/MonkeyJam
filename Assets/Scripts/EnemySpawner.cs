using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _enemyPrefab;
    [SerializeField] private float _spawnCooldown = 2.5f;
    private Vector2 _screenBounds;
    private float _timer = 0.0f;

    private void Start()
    {
        _screenBounds = ScreenBoundary.GetInstance().GetScreenBounds();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _spawnCooldown)
        {
            _timer = 0.0f;
            Instantiate(_enemyPrefab, GenerateSpawnPosition(), _enemyPrefab.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        bool isYPositive = Random.value > 0.5f;
        bool isZPositive = Random.value > 0.5f;
        Vector3 spawnPos = new Vector3(
            0.0f,
            (isYPositive ?  Random.Range(1f, _screenBounds.y) : Random.Range(-1f, -_screenBounds.y)),
            (isZPositive ?  Random.Range(1f, _screenBounds.x) : Random.Range(-1f, -_screenBounds.x))
        );

        if (Random.value < 0.5f)
        {
            spawnPos.y = (spawnPos.y > 0.0f) ? _screenBounds.y + 1.5f : -_screenBounds.y - 1.5f;
        }
        else
        {
            spawnPos.z = (spawnPos.z > 0.0f) ? _screenBounds.x + 1.5f : -_screenBounds.x - 1.5f;
        }
        return spawnPos;
    }
}
