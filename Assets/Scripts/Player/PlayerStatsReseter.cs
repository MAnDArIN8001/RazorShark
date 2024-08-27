using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class PlayerStatsReseter : MonoBehaviour
{
    private PlayerCharacteristics _mainPlayerCharacteristcs;
    private PlayerCharacteristics _defaultPlayerCharacteristcs;

    private PlayerHealth _health;

    [Inject]
    private void Initialize(PlayerCharacteristics mainCharacteristics, [Inject(Id = CharacteristicType.Default)] PlayerCharacteristics defaultCharacteristics)
    {
        _mainPlayerCharacteristcs = mainCharacteristics;
        _defaultPlayerCharacteristcs = defaultCharacteristics;
    }

    private void Awake()
    {
        _health = GetComponent<PlayerHealth>();

        if (_health is null)
        {
            Debug.LogError($"The gameObject {gameObject} doesnt contains component PlayerHealth");
        }
    }

    private void OnEnable()
    {
        if (_health is not null)
        {
            _health.OnDied += HandleDeath;
        }   
    }

    private void OnDisable()
    {
        if (_health is not null)
        {
            _health.OnDied -= HandleDeath;
        }
    }

    private void HandleDeath()
    {
        _mainPlayerCharacteristcs.ResetCharactics(_defaultPlayerCharacteristcs);
    }
}
