using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _playerPrefab;

    private void Start()
    {
        Instantiate(_playerPrefab);
    }
}
