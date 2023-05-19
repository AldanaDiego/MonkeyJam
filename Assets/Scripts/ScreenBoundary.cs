using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundary : Singleton<ScreenBoundary>
{
    [SerializeField] private Camera _mainCamera;
    private Vector2 _screenBounds;

    protected override void Awake()
    {
        base.Awake();
        _screenBounds = new Vector2(
            _mainCamera.orthographicSize * Screen.width / Screen.height,
            _mainCamera.orthographicSize
        );
    }

    public Vector2 GetScreenBounds()
    {
        return _screenBounds;
    }
}
