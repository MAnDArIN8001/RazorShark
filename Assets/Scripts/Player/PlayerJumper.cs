using System;
using UnityEngine;

public class PlayerJumper : MonoBehaviour
{
    public event Action<bool> OnWateringStateChanged;

    private bool _isInWater;

    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Water>(out var water))
        {
            _rigidbody.gravityScale = 1f;
            _rigidbody.velocity = Vector3.up * _jumpForce;

            _isInWater = false;

            OnWateringStateChanged?.Invoke(_isInWater);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Water>(out var water))
        {
            _rigidbody.gravityScale = 0f;
            _rigidbody.velocity = Vector2.zero;

            _isInWater = true;

            OnWateringStateChanged?.Invoke(_isInWater);
        }
    }
}
