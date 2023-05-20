using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _stopDistance = 0.1f;

    private SoundEffectManager _soundEffectManager;
    private Transform _transform;
    private Vector2 _screenBounds;
    private Vector3 _targetPosition;

    public static EventHandler OnEnemyDeathByPlayer;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        Vector3 pos = _transform.position;
        if (pos.z > 0.0f)
        {
            _transform.RotateAround(pos, _transform.up, 180.0f);
        }
        _screenBounds = ScreenBoundary.GetInstance().GetScreenBounds();
        _targetPosition = GenerateTargetPosition(pos);
        Vector3 targetDirection = (_targetPosition - pos).normalized;
        _transform.forward = targetDirection;
        _soundEffectManager = SoundEffectManager.GetInstance();
    }

    private void Update() 
    {
        HandleMovement();
        CheckTarget();
    }

    private void HandleMovement()
    {
        _transform.position += _transform.forward * _movementSpeed * Time.deltaTime;
    }

    private void CheckTarget()
    {
        if (Vector3.Distance(_transform.position, _targetPosition) < _stopDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            _smoke.transform.parent = null;
            _smoke.Play(false);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Bullet")
        {
            _smoke.transform.parent = null;
            _smoke.Play(false);
            _soundEffectManager.PlayEnemyDeathAudio();
            OnEnemyDeathByPlayer?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }
    }

    private Vector3 GenerateTargetPosition(Vector3 currentPos)
    {
        Vector3 target = new Vector3(
            0.0f,
            ((currentPos.y > 0.0f) ? UnityEngine.Random.Range(-1f, -_screenBounds.y) : UnityEngine.Random.Range(1f, _screenBounds.y)),
            ((currentPos.z > 0.0f) ? UnityEngine.Random.Range(-1f, -_screenBounds.x) : UnityEngine.Random.Range(1f, _screenBounds.x))
        );

        if (UnityEngine.Random.value < 0.5f)
        {
            target.y = (target.y > 0.0f) ? _screenBounds.y + 1.5f : -_screenBounds.y - 1.5f;
        }
        else
        {
            target.z = (target.z > 0.0f) ? _screenBounds.x + 1.5f : -_screenBounds.x - 1.5f;
        }

        return target;
    }
}
