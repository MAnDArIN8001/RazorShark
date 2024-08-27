using System;
using UnityEngine;

public class CrabHealth : MonoBehaviour, IDamagable
{
    public event Action OnDied;
    public event Action OnReaciaveHit;

    [SerializeField] private float _health;

    public void ReciaveDamage(float damage)
    {
        float newHealth = _health - damage;

        if (newHealth <= 0)
        {
            _health = 0;

            OnDied?.Invoke();

            Destroy(gameObject);

            return;
        }

        OnReaciaveHit?.Invoke();

        _health = newHealth;
    }
}
