using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private GameObject _vessel;
    [SerializeField] private GameObject _propulsors;
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private Animator _monkeyAnimator;

    private bool _active;
    private float _changeDanceCooldown = 3f;
    private float _changeDanceTimer;

    private void Start()
    {
        PlayerHealth.OnPlayerDeath += OnPlayerDeath;
        _active = true;
        _changeDanceTimer = 0f;
    }

    private void Update()
    {
        if (_active)
        {
            _changeDanceTimer += Time.deltaTime;
            if (_changeDanceTimer > _changeDanceCooldown)
            {
                _changeDanceTimer = 0;
                _monkeyAnimator.SetInteger("DanceStyle", UnityEngine.Random.Range(1, 4));
            }
        }
    }

    private void OnPlayerDeath(object sender, EventArgs empty)
    {
        _active = false;
        Destroy(_vessel);
        Destroy(_propulsors);
        _smoke.Play();
        _monkeyAnimator.SetTrigger("OnDeath");
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= OnPlayerDeath;
    }

}