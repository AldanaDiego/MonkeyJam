using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : Singleton<SoundEffectManager>
{
    [SerializeField] AudioClip _bulletShotAudio;
    private AudioSource _audioSource;
    private float _volume = 1f;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayBulletShotAudio()
    {
        Debug.Log($"Play sound!");
        _audioSource.PlayOneShot(_bulletShotAudio, _volume);
    }
}
