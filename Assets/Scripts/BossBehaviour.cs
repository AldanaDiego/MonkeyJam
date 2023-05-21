using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public static EventHandler OnBossDeath;

    [SerializeField] private BulletBehaviour _bulletPrefab;

    private float _movementSpeed = 3f;
    private float _rotationSpeed = 5f;
    private int _bossHealth;
    private float _invincibleTime = 1.5f;
    private float _invincibleCooldown = 0.0f;

    private Transform _transform;
    private List<Vector3> _targetPositions;
    private Vector3 _moveDirection;
    private int _currentTargetIndex;
    private float _stopDistance = 0.1f;
    private Vector2 _screenBounds;
    private bool _activeMovement;
    private bool _activeShooting;

    private float _shootCooldown = 7f;
    private float _shootTimer;

    private void Start()
    {
        _transform = transform;
        _currentTargetIndex = 0;
        _screenBounds = ScreenBoundary.GetInstance().GetScreenBounds();
        CalculateTargetPositions();
        _activeMovement = true;
        _activeShooting = false;
        _bossHealth = 10;
        _shootTimer = 0;
    }

    private void Update()
    {
        if (_invincibleCooldown > 0.0f)
        {
            _invincibleCooldown -= Time.deltaTime;
        }
        if (_activeMovement)
        {
            HandleMovement();
        }
        if (_activeShooting)
        {
            HandleShooting();
        }
    }

    private void HandleMovement()
    {
        if (Vector3.Distance(_transform.position, _targetPositions[_currentTargetIndex]) > _stopDistance)
        {
            _transform.position += _moveDirection * _movementSpeed * Time.deltaTime;
            _transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.LookRotation(_moveDirection), _rotationSpeed * Time.deltaTime);
        }
        else
        {
            _activeShooting = true;
            _currentTargetIndex++;
            if (_currentTargetIndex >= _targetPositions.Count)
            {
                _currentTargetIndex = 0;
            }
            _moveDirection = (_targetPositions[_currentTargetIndex] - _transform.position).normalized;
        }
    }

    private void HandleShooting()
    {
        _shootTimer += Time.deltaTime;
        if (_shootTimer >= _shootCooldown)
        {
            _shootTimer = 0f;
            Vector3 direction = (new Vector3(0, UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f))) - _transform.position;
            BulletBehaviour bullet = Instantiate(_bulletPrefab, _transform.position, Quaternion.identity);
            bullet.Setup(direction);
        } 
    }

    private void CalculateTargetPositions()
    {
        //If you read this and it confuses you, I am confused too
        float z = _screenBounds.x;
        float y = _screenBounds.y;
        _targetPositions = new List<Vector3>();
        _targetPositions.Add(new Vector3(0, -y + 1f, 0));
        _targetPositions.Add(new Vector3(0, -y + 2f, z - 2f));
        _targetPositions.Add(new Vector3(0, 0, z - 1f));
        _targetPositions.Add(new Vector3(0, y - 4f, z - 4f));
        _targetPositions.Add(new Vector3(0, y - 2f, 0));
        _targetPositions.Add(new Vector3(0, y - 4f, -z + 4f));
        _targetPositions.Add(new Vector3(0, 0, -z + 1f));
        _targetPositions.Add(new Vector3(0, -y + 2f, -z + 2f));
        _moveDirection = (_targetPositions[0] - _transform.position).normalized;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_invincibleCooldown <= 0.0f && other.gameObject.tag == "Bullet")
        {
            _invincibleCooldown = _invincibleTime;
            _bossHealth--;
            if (_bossHealth <= 0)
            {
                OnBossDeath?.Invoke(this, EventArgs.Empty);
                Destroy(gameObject);
            }
        }    
    }
}