using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    public event Action<Vector2> OnInputVelocityCompute;

    private bool _isHandlingInput;
    [SerializeField] private bool _isFacingRight;

    [SerializeField] private float _movementSpeed;

    private Vector2 _lastVelocity;

    private Rigidbody2D _rigidbody;

    private Joystick _joystick;

    [Inject]
    private void Initialize(Joystick joystick)
    {
        _joystick = joystick;
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
    }

    private void Update()
    {
        if (_joystick is not null)
        {
            Vector2 joystickInputValues = _joystick.Direction.normalized;

            Move(joystickInputValues);

            if (joystickInputValues != _lastVelocity)
            {
                OnInputVelocityCompute?.Invoke(joystickInputValues);
            }

            _lastVelocity = joystickInputValues;
        }
    }

    private void Move(Vector2 direction)
    {
        _rigidbody.velocity = direction * _movementSpeed;

        if ((_isFacingRight && direction.x < 0) 
            || (!_isFacingRight && direction.x > 0))
        {
            transform.Flip();

            _isFacingRight = !_isFacingRight;
        }
    }
}
