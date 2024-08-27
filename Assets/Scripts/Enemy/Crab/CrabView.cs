using UnityEngine;

[RequireComponent(typeof(CrabHealth))]
[RequireComponent(typeof(AudioSource))]
public class CrabView : MonoBehaviour
{
    [SerializeField] private GameObject _hitEffect;
    [SerializeField] private GameObject _deathEffect;

    [SerializeField] private AudioClip _deathClip;

    private AudioSource _audioSource;

    private SoundEffectsConfig _soundConfig;

    private CrabHealth _crabHealth;

    private void Awake()
    {
        _crabHealth = GetComponent<CrabHealth>();

        if (_crabHealth is null)
        {
            Debug.LogError($"The gameObject {gameObject} doesnt contains component CrabHealth");
        }

        _audioSource = GetComponent<AudioSource>();
        _soundConfig = Resources.Load<SoundEffectsConfig>(GameplayConsts.SoundConfigPath);

        if (_soundConfig is not null)
        {
            _audioSource.volume = _soundConfig.SoundsVolume;
        }
    }

    private void OnEnable()
    {
        if (_crabHealth is not null)
        {
            _crabHealth.OnReaciaveHit += HandleHit;
            _crabHealth.OnDied += HandleDeath;
        }
    }

    private void OnDisable()
    {
        if (_crabHealth is not null)
        {
            _crabHealth.OnReaciaveHit -= HandleHit;
            _crabHealth.OnDied -= HandleDeath;
        }
    }

    private void HandleHit() 
    {
        Instantiate(_hitEffect, transform.position, Quaternion.identity);
    }

    private void HandleDeath()
    {
        Instantiate(_deathEffect, transform.position, Quaternion.identity);
    }
}
