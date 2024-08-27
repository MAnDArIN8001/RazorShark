using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/SoundsConfig", fileName = "SoundConfig")]
public class SoundEffectsConfig : ScriptableObject
{
    [SerializeField, Range(0, 1)] private float _musicVolume;
    [SerializeField, Range(0, 1)] private float _soundsVolume;

    public float MusicVolume 
    {
        get => _musicVolume;
        set
        {
            if (value < 0)
            {
                throw new Exception("Value cannot be less than zero");
            }

            _musicVolume = value;
        }
    }

    public float SoundsVolume
    {
        get => _soundsVolume;
        set
        {
            if (value < 0)
            {
                throw new Exception("Value cannot be less than zero");
            }

            _soundsVolume = value;
        }
    }
}
