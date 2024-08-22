using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour
{
    [SerializeField] private GameObject _attackEffect;

    private Animator _animator;

    private PlayerMover _playerMover;
    private PlayerAttacker _playerAttacker;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        if (_animator is null)
        {
            Debug.LogError($"The gameObject {gameObject} has missed component Animator");
        }

        _playerMover = GetComponent<PlayerMover>();

        if (_playerMover is null)
        {
            Debug.LogError($"The gameObject {gameObject} has missed component PlayerMover");
        }

        _playerAttacker = GetComponent<PlayerAttacker>();

        if (_playerAttacker is null)
        {
            Debug.LogError($"The gameObject {gameObject} has missed component PlayerAttacker");
        }
    }

    private void OnEnable()
    {
        if (_playerMover is not null)
        {
            _playerMover.OnInputVelocityCompute += HandleInputVelocityComputation;
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

        if (_playerAttacker is not null)
        {
            _playerAttacker.OnMadeAttack -= HandleAttack;
        }
    }

    private void HandleInputVelocityComputation(Vector2 velocity)
    {
        _animator.SetFloat(PlayerAnimationConsts.VelocityParamName, velocity.magnitude);
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
