using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private PlayerInputActions _playerInput;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _playerInput = new PlayerInputActions();
        _playerInput.Enable();
    }

    public Vector2 GetMovementInput()
    {
        return _playerInput.Player.Move.ReadValue<Vector2>();
    }

    public bool IsShootTriggered()
    {
        return _playerInput.Player.Shoot.IsPressed();
    }
    
}
