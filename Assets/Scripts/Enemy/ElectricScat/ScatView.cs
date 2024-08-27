using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ScatView : MonoBehaviour
{
    [SerializeField] private GameObject _hitEffect;
    [SerializeField] private GameObject _deathEffect;
    [SerializeField] private GameObject _attackEffect;

    [SerializeField] private AudioClip _attackClip;

    private AudioSource _audioSource;

    private SoundEffectsConfig _soundConfig;

    private ScatHealth _scatHealth;
    private ScatAttacker _scatAttacker;

    private void Awake()
    {
        _scatHealth = GetComponent<ScatHealth>();

        if (_scatHealth is null)
        {
            Debug.LogError($"The gameObject {gameObject} doesnt contains component ScatHealth");
        }

        _scatAttacker = GetComponent<ScatAttacker>();

        if (_scatAttacker is null)
        {
            Debug.LogError($"The gameObject {gameObject} doesnt contains component ScatAttacker");
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
        if (_scatHealth is not null)
        {
            _scatHealth.OnReaciaveHit += HandleHit;
            _scatHealth.OnDied += HandleDeath;
        }

        if (_scatAttacker is not null)
        {
            _scatAttacker.OnMadeAttack += HandleAttack;
        }
    }

    private void OnDisable()
    {
        if (_scatHealth is not null)
        {
            _scatHealth.OnReaciaveHit -= HandleHit;
            _scatHealth.OnDied -= HandleDeath;
        }

        if (_scatAttacker is not null)
        {
            _scatAttacker.OnMadeAttack -= HandleAttack;
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

    private void HandleAttack()
    {
        Instantiate(_attackEffect, transform.position, Quaternion.identity);

        _audioSource.PlayOneShot(_attackClip);
    }
}
