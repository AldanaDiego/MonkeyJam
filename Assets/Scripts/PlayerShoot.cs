using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private BulletBehaviour _bulletPrefab;
    [SerializeField] private float _shootCooldown;
    private Transform _transform;
    private PlayerMovement _playerMovement;
    private InputManager _inputManager;
    private float _cooldownTimer = 0.0f;

    private List<Vector3> _shootPoints = new List<Vector3> { new Vector3(0, 0.6f, 1.3f), new Vector3(0, 0.6f, -1.3f) };

    private void Start()
    {
        _transform = transform;
        _inputManager = InputManager.GetInstance();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (_cooldownTimer > 0.0f)
        {
            _cooldownTimer -= Time.deltaTime;
        }

        if (_cooldownTimer <= 0.0f && _inputManager.IsShootTriggered())
        {
            _cooldownTimer = _shootCooldown;
            Vector3 shootDirection = _playerMovement.GetUpDirection();
            foreach (var shootPoint in GetShootingPoints())
            {
                BulletBehaviour bullet = Instantiate(_bulletPrefab, shootPoint, Quaternion.identity);
                bullet.Setup(shootDirection);
            }
        }
    }

    private List<Vector3> GetShootingPoints()
    {
        Quaternion playerRotation = Quaternion.AngleAxis(_playerMovement.GetRotation().z, Vector3.right);
        Vector3 rotationCenter = _transform.position;
        List<Vector3> rotatedShootPoints = new List<Vector3>();

        foreach(var shootPoint in _shootPoints)
        {
            rotatedShootPoints.Add(playerRotation * (shootPoint) + rotationCenter);
        }

        return rotatedShootPoints;
    }

}
