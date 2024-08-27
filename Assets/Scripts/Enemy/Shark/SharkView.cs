using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SharkView : MonoBehaviour
{
    [SerializeField] private GameObject _hitEffect;
    [SerializeField] private GameObject _deathEffect;
    [SerializeField] private GameObject _attackEffect;

    [SerializeField] private AudioClip _hitClip;
    [SerializeField] private AudioClip _deathClip;

    private AudioSource _audioSource;

    private SoundEffectsConfig _soundConfig;

    private SharkAttacker _sharkAttacker;
    private SharkHealth _sharkHealth;

    private void Awake()
    {
        _sharkHealth = GetComponent<SharkHealth>();

        if (_sharkHealth is null)
        {
            Debug.LogError($"The gameObject {gameObject} doesnt contains component SharkHelath");
        }

        _sharkAttacker = GetComponent<SharkAttacker>();

        if (_sharkAttacker is null)
        {
            Debug.LogError($"The gameObject {gameObject} doesnt contains component SharkAttacker");
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
        if (_sharkHealth is not null)
        {
            _sharkHealth.OnReaciaveHit += HandleHit;
            _sharkHealth.OnDied += HandleDeath;
        } 

        if (_sharkAttacker is not null)
        {
            _sharkAttacker.OnMadeAttack += HandleAttack;
        }
    }

    private void OnDisable()
    {
        if (_sharkHealth is not null)
        {
            _sharkHealth.OnReaciaveHit -= HandleHit;
            _sharkHealth.OnDied -= HandleDeath;
        }

        if (_sharkAttacker is not null)
        {
            _sharkAttacker.OnMadeAttack -= HandleAttack;
        }
    }

    private void HandleHit()
    {
        Instantiate(_hitEffect, transform.position, Quaternion.identity);

        _audioSource.PlayOneShot(_hitClip);
    }

    private void HandleDeath()
    {
        Instantiate(_deathEffect, transform.position, Quaternion.identity);

        _audioSource.PlayOneShot(_deathClip);
    }

    private void HandleAttack()
    {
        Instantiate(_attackEffect, transform.position, Quaternion.identity);
    }
}
