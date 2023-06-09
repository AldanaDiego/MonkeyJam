using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int _amountToHeal;
    private SoundEffectManager _soundEffectManager;
    private Transform _transform;
    private Vector2 _screenBounds;
    private float _rotationSpeed = 15f;

    public static EventHandler<int> OnItemHeal;

    private void Start()
    {
        _transform = transform;
        _screenBounds = ScreenBoundary.GetInstance().GetScreenBounds();
        _soundEffectManager = SoundEffectManager.GetInstance();
    }

    private void Update()
    {
        _transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
        if (_transform.position.y < -_screenBounds.y)
        {
            Destroy(gameObject);
        }    
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnItemHeal?.Invoke(this, _amountToHeal);
            _soundEffectManager.PlayItemPickAudio();
            Destroy(gameObject);
        }    
    }
}
