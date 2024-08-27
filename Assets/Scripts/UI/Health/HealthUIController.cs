using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthUIController : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;

    private PlayerCharacteristics _characteristics;

    private PlayerHealth _playerHealth;

    [Inject] 
    private void Initialize(PlayerHealth playerHealth, PlayerCharacteristics characteristics)
    {
        _playerHealth = playerHealth;
        _characteristics = characteristics;
    }

    private void OnEnable()
    {
        if (_playerHealth is not null)
        {
            _playerHealth.OnReciaveDamage += HandleHit;
        }
    }

    private void OnDisable()
    {
        if (_playerHealth is not null)
        {
            _playerHealth.OnReciaveDamage -= HandleHit;
        }
    }

    private void HandleHit()
    {
        float healthBarValue = _playerHealth.Health / _characteristics.MaxHealth;

        _healthSlider.value = healthBarValue;
    }
}
