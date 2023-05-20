using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _propulsors;
    [SerializeField] private float _movementSpeed = 100f;
    [SerializeField] private float _rotationSpeed = 75f;
    [SerializeField] private float _brakeSpeed = 0.05f;
    [SerializeField] private float _maxSpeed = 30f;
    [SerializeField] private float _boundsOffset = 1f;

    private Transform _transform;
    private InputManager _inputManager;
    private Rigidbody _rigidBody;
    private Vector2 _screenBounds;

    private void Start()
    {
        _transform = transform;
        _inputManager = InputManager.GetInstance();
        _screenBounds = ScreenBoundary.GetInstance().GetScreenBounds();
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector2 movInput = _inputManager.GetMovementInput();
        if (movInput != Vector2.zero)
        {
            HandleAcceleration(movInput.y);
            HandleRotation(movInput.x);
        }
    }

    private void LateUpdate()
    {
        Vector3 pos = _transform.position;
        pos.x = 0.0f;
        pos.z = Mathf.Clamp(pos.z, (_screenBounds.x * -1.0f) + _boundsOffset, _screenBounds.x - _boundsOffset);
        pos.y = Mathf.Clamp(pos.y, (_screenBounds.y * -1.0f) + _boundsOffset, _screenBounds.y - _boundsOffset);
        _transform.position = pos;
    }

    private void HandleAcceleration(float value)
    {
        if (value == 0.0f)
        {
            return;
        }
        else if (value > 0.0f) //Accelerate
        {
            Vector3 direction = _propulsors.up * (_movementSpeed * Time.deltaTime);
            _rigidBody.AddForce(direction, ForceMode.Force);
            if (_rigidBody.velocity.magnitude > _maxSpeed)
            {
                _rigidBody.velocity = _rigidBody.velocity.normalized * _maxSpeed;
            }
        }
        else //Brake
        {
            if (_rigidBody.velocity.magnitude > 0.1f)
            {
                _rigidBody.AddForce(_rigidBody.velocity * (_brakeSpeed * -1.0f), ForceMode.Force);
            }
        }
    }

    private void HandleRotation(float value)
    {
        if (value == 0.0f)
        {
            return;
        }

        float rotation = _rotationSpeed * Time.deltaTime * (value > 0.0f ? 1.0f : -1.0f);
        _propulsors.Rotate(0.0f, 0.0f, rotation);
    }

    public Vector3 GetRotation()
    {
        return _propulsors.transform.eulerAngles;
    }

    public Vector3 GetUpDirection()
    {
        return _propulsors.transform.up;
    }

}
