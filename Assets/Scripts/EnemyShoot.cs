using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private BulletBehaviour _bulletPrefab;
    private Transform _transform;
    private float _shootCooldown = 3f;
    private float _shootTimer;
    private bool _hasShooted;
    private Vector2 _screenBounds;

    private void Start()
    {
        _transform = transform;
        _screenBounds = ScreenBoundary.GetInstance().GetScreenBounds();
        _shootTimer = 0f;
        _hasShooted = false;
    }

    private void Update()
    {
        if (!_hasShooted)
        {
            _shootTimer += Time.deltaTime;
            if (_shootTimer > _shootCooldown)
            {
                Vector3 direction = (new Vector3(0, UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f))) - _transform.position;
                BulletBehaviour bullet = Instantiate(_bulletPrefab, _transform.position, Quaternion.identity);
                bullet.Setup(direction);
                _hasShooted = true;
            }
        }
    }
}
