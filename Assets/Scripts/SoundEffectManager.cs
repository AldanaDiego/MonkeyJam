using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : Singleton<SoundEffectManager>
{
    [SerializeField] AudioClip _bulletShotAudio;
    [SerializeField] AudioClip _damageAudio1;
    [SerializeField] AudioClip _damageAudio2;
    [SerializeField] AudioClip _itemPickAudio;
    [SerializeField] AudioClip _enemyDeathAudio;
    [SerializeField] AudioClip _playerDeathAudio;

    private AudioSource _audioSource;
    private float _volume = 0.75f;

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
        _audioSource.PlayOneShot(_bulletShotAudio, 1f);
    }

    public void PlayItemPickAudio()
    {
        _audioSource.PlayOneShot(_itemPickAudio, 0.55f);
    }

    public void PlayDamageAudio()
    {
        _audioSource.PlayOneShot(_damageAudio1, _volume);
        _audioSource.PlayOneShot(_damageAudio2, _volume);
    }

    public void PlayEnemyDeathAudio()
    {
        _audioSource.PlayOneShot(_enemyDeathAudio, _volume);
    }

    public void PlayPlayerDeathAudio()
    {
        _audioSource.PlayOneShot(_playerDeathAudio, _volume);
    }
}
