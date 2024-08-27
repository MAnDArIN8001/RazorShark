using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Shell : MonoBehaviour
{
    public event Action OnDestroyed;

    [SerializeField] private float _lifeTime;
    [SerializeField] private float _damage;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        Destroy(gameObject, _lifeTime);
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamagable>(out var damagable))
        {
            damagable.ReciaveDamage(_damage);
        }

        Destroy(gameObject);

        OnDestroyed?.Invoke();
    }

    public void Throw(Vector2 direction, float velocity)
    {
        _rigidbody.velocity = direction.normalized * velocity;
    }
}
