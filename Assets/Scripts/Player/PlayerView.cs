using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class PlayerView : MonoBehaviour
{
    [SerializeField] private GameObject _attackEffect;
    [SerializeField] private GameObject _hitEffect;

    [SerializeField] private AudioClip _hitClip;
    [SerializeField] private AudioClip _deathClip;

    private Animator _animator;

    private AudioSource _audioSource;

    private SoundEffectsConfig _soundConfig;

    private PlayerMover _playerMover;
    private PlayerHealth _playerHealth;
    private PlayerAttacker _playerAttacker;

    [Inject]
    private void Initialize(SoundEffectsConfig soundConfig)
    {
        _soundConfig = soundConfig;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _playerMover = GetComponent<PlayerMover>();

        if (_playerMover is null)
        {
            Debug.LogError($"The gameObject {gameObject} has missed component PlayerMover");
        }

        _playerHealth = GetComponent<PlayerHealth>();

        if (_playerHealth is null)
        {
            Debug.LogError($"The gameObject {gameObject} has missed component PlayerHealth");
        }

        _playerAttacker = GetComponent<PlayerAttacker>();

        if (_playerAttacker is null)
        {
            Debug.LogError($"The gameObject {gameObject} has missed component PlayerAttacker");
        }

        _audioSource = GetComponent<AudioSource>();

        if (_soundConfig is not null)
        {
            _audioSource.volume = _soundConfig.SoundsVolume;
        }
    }

    private void OnEnable()
    {
        if (_playerMover is not null)
        {
            _playerMover.OnInputVelocityCompute += HandleInputVelocityComputation;
        }

        if (_playerHealth is not null)
        {
            _playerHealth.OnReciaveDamage += HandleHit;
            _playerHealth.OnDied += HandleDeath;
        }

        if (_playerAttacker is not null)
        {
            _playerAttacker.OnMadeAttack += HandleAttack;
        }
    }

    private void OnDisable()
    {
        if (_playerMover is not null)
        {
            _playerMover.OnInputVelocityCompute -= HandleInputVelocityComputation;
        }

        if (_playerHealth is not null)
        {
            _playerHealth.OnReciaveDamage -= HandleHit;
            _playerHealth.OnDied -= HandleDeath;
        }

        if (_playerAttacker is not null)
        {
            _playerAttacker.OnMadeAttack -= HandleAttack;
        }
    }

    private void HandleInputVelocityComputation(Vector2 velocity)
    {
        _animator.SetFloat(PlayerAnimationConsts.VelocityParamName, velocity.magnitude);
    }

    private void HandleHit()
    {
        Instantiate(_hitEffect, transform.position, Quaternion.identity);
    }

    private void HandleDeath()
    {
        _animator.SetTrigger(PlayerAnimationConsts.DiedParamName);
    }

    private void HandleAttack()
    {
        _animator.SetTrigger(PlayerAnimationConsts.AttackParamName);

        GameObject effectInstance = Instantiate(_attackEffect, transform.position, Quaternion.identity);
        
        if (transform.localScale.x < 0)
        {
            effectInstance.transform.Flip();
        }
    }
}
