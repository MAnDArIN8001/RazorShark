using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    public event Action<Vector2> OnInputVelocityCompute;

    private bool _isInWater;
    private bool _isHandlingInput;
    [SerializeField] private bool _isFacingRight;

    private Vector2 _lastVelocity;

    private Rigidbody2D _rigidbody;

    private Joystick _joystick;

    private PlayerJumper _playerJumper;
    private PlayerCharacteristics _playerCharacteristics;

    [Inject]
    private void Initialize(PlayerCharacteristics playerCharacterisics, Joystick joystick)
    {
        _joystick = joystick;
        _playerCharacteristics = playerCharacterisics;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_rigidbody is null)
        {
            Debug.LogError($"The gameObject {gameObject} has missed component Rigidbody2D");
        }

        if (_joystick is null)
        {
            Debug.LogError($"the gameObject {gameObject} has missed inportant field Joystick");
        }

        _playerJumper = GetComponent<PlayerJumper>();

        if (_playerJumper is null)
        {
            Debug.LogError($"the gameObject {gameObject} has missed component PlayerJumper");
        }
    }

    private void Update()
    {
        if (_joystick is not null)
        {
            Vector2 joystickInputValues = _joystick.Direction.normalized;

            if (_lastVelocity != Vector2.zero)
            {
                Move(joystickInputValues);
            }

            if (joystickInputValues != _lastVelocity)
            {
                OnInputVelocityCompute?.Invoke(joystickInputValues);
            }

            _lastVelocity = joystickInputValues;
        }
    }

    private void OnEnable()
    {
        if (_playerJumper is not null)
        {
            _playerJumper.OnWateringStateChanged += HandleWateringStateChangings;
        }
    }

    private void OnDisable()
    {
        if (_playerJumper is not null)
        {
            _playerJumper.OnWateringStateChanged -= HandleWateringStateChangings;
        }
    }

    private void HandleWateringStateChangings(bool isInWater)
    {
        _isInWater = isInWater;
    }

    private void Move(Vector2 direction)
    {
        Vector2 newVelocity = _isInWater
            ? new Vector2(direction.x, direction.y) * _playerCharacteristics.Speed
            : new Vector2(direction.x * _playerCharacteristics.Speed, _rigidbody.velocity.y);

        _rigidbody.velocity = newVelocity;

        if ((_isFacingRight && direction.x < 0) 
            || (!_isFacingRight && direction.x > 0))
        {
            transform.Flip();

            _isFacingRight = !_isFacingRight;
        }
    }
}
