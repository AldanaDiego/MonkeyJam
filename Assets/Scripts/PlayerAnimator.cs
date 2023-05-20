using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private GameObject _vessel;
    [SerializeField] private GameObject _propulsors;
    [SerializeField] private Animator _monkeyAnimator;

    private void Start()
    {
        PlayerHealth.OnPlayerDeath += OnPlayerDeath;
    }

    private void OnPlayerDeath(object sender, EventArgs empty)
    {
        Destroy(_vessel);
        Destroy(_propulsors);
        _monkeyAnimator.SetTrigger("OnDeath");
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= OnPlayerDeath;
    }

}