using System;
using UnityEngine;
using Zenject;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    public event Action OnReciaveDamage;
    public event Action OnDied;

    [SerializeField] private float _health;

    private PlayerCharacteristics _characteristics;

    public float Health => _health;

    [Inject]
    private void Initialize(PlayerCharacteristics characteristics)
    {
        _characteristics = characteristics;
    }

    public void ReciaveDamage(float damage)
    {
        float newHealth = _health - damage * _characteristics.Armor;

        if (newHealth <= 0)
        {
            _health = 0;

            OnDied?.Invoke();
        }

        _health = newHealth;

        OnReciaveDamage?.Invoke();
    }
}
