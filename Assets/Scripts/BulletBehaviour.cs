using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 10f;
    private Vector2 _screenBounds;
    private Vector3 _shootDirection;
    private Transform _transform;
    private bool _active = false;

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
            _transform.position.y > _screenBounds.y ||
            _transform.position.z > _screenBounds.x ||
            _transform.position.y < -_screenBounds.y ||
            _transform.position.z < -_screenBounds.x
        )
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }    
    }

    public void Setup(Vector3 shootDirection)
    {
        _shootDirection = shootDirection;
        _active = true;
    }
}
