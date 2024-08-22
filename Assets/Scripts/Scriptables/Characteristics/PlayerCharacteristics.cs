using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerCharacteristics", menuName = "Gameplay/PlayerCharacteristics")]
public class PlayerCharacteristics : ScriptableObject
{
    [SerializeField] private float _damage;
    [SerializeField] private float _rainforcement;
    [SerializeField] private float _energyRegeneration;

    public float Damage => _damage;
    public float Rainforcement => _rainforcement;
    public float EnergyRegeneration => _energyRegeneration;
}
