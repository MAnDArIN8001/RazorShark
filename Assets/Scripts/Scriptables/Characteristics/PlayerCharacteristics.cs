using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerCharacteristics", menuName = "Gameplay/PlayerCharacteristics")]
public class PlayerCharacteristics : ScriptableObject
{
    [SerializeField] private int _damageLvl;
    [SerializeField] private int _armorLvl;
    [SerializeField] private int _speedLvl;

    [SerializeField] private float _damage;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _armor;
    [SerializeField] private float _speed;

    public int DamageLvl 
    {
        get => _damageLvl;
        set
        {
            if (value <= 0 || value <= _damageLvl)
            {
                throw new Exception("value cant be less or equals to zero and last value");
            }

            _damageLvl = value;
        }
    }

    public int ArmorLvl
    {
        get => _armorLvl;
        set
        {
            if (_armor - value < 0)
            {
                throw new Exception("value cant be less to zero");
            }

            if (_armorLvl + value <= _armorLvl)
            {
                throw new Exception("value cant be less or equals to last value");
            }

            _armorLvl = value;
        }
    }

    public int SpeedLvl
    {
        get => _speedLvl;
        set
        {
            if (value <= 0 || value <= _speedLvl)
            {
                throw new Exception("value cant be less or equals to zero and last value");
            }

            _speedLvl = value;
        }
    }

    public float Damage 
    {
        get => _damage;
        set
        {
            if (value <= 0)
            {
                throw new Exception("Value canot be equals or less then zero");
            }

            _damage = value;
        }
    }

    public float MaxHealth => _maxHealth;

    public float Armor
    {
        get => _armor;
        set
        {
            if (value <= 0)
            {
                throw new Exception("Value canot be equals or less then zero");
            }

            _armor = value;
        }
    }

    public float Speed 
    {
        get => _speed;
        set
        {
            if (value <= 0)
            {
                throw new Exception("Value canot be equals or less then zero");
            }

            _speed = value;
        }
    }

    public void ResetCharactics(PlayerCharacteristics resetCharacteristics)
    {
        _armorLvl = resetCharacteristics.ArmorLvl;
        _speedLvl = resetCharacteristics.SpeedLvl;
        _damageLvl = resetCharacteristics.DamageLvl;
        _maxHealth = resetCharacteristics.MaxHealth;
        _armor = resetCharacteristics.Armor;
        _speed = resetCharacteristics.Speed;
        _damage = resetCharacteristics.Damage;
    }
}
