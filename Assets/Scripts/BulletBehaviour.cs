using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 10f;
    [SerializeField] private ParticleSystem _smokeTrail;
    [SerializeField] private ParticleSystem _smoke;
    private Vector2 _screenBounds;
    private Vector3 _shootDirection;
    private Transform _transform;
    private bool _active = false;
    private float _outOfBoundsOffset = 2f;

    private void Start()
    {
        _transform = transform;
        _screenBounds = ScreenBoundary.GetInstance().GetScreenBounds();
    }

    private void Update()
    {
        if (_active)
        {
            _transform.position += _shootDirection * _movementSpeed * Time.deltaTime;
            CheckBounds();
        }
    }

    private void CheckBounds()
    {
        if (
            _transform.position.y - _outOfBoundsOffset > _screenBounds.y ||
            _transform.position.z - _outOfBoundsOffset > _screenBounds.x ||
            _transform.position.y + _outOfBoundsOffset < -_screenBounds.y ||
            _transform.position.z + _outOfBoundsOffset < -_screenBounds.x
        )
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (gameObject.tag == "Bullet" && (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss"))
        {
            _smoke.transform.parent = null;
            _smoke.Play(false);
            Destroy(gameObject);
        }
        else if (gameObject.layer == LayerMask.NameToLayer("EnemyBullet") && other.gameObject.tag == "Player")
        {
            _smoke.transform.parent = null;
            _smoke.Play(false);
            Destroy(gameObject);
        }
    }

    public void Setup(Vector3 shootDirection)
    {
        _shootDirection = shootDirection;
        _smokeTrail.transform.forward = shootDirection * -1f;
        _active = true;
    }
}
