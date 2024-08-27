using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    [SerializeField] private SoundType _soundType;

    private AudioSource _audioSource;

    private SoundEffectsConfig _soundConfig;

    [Inject]
    private void Initialize(SoundEffectsConfig soundConfig)
    {
        _soundConfig = soundConfig;
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        if (_soundConfig is not null)
        {
            float volume = 0;

            switch (_soundType)
            {
                case SoundType.Music:
                    volume = _soundConfig.MusicVolume;
                    break;
                case SoundType.SoundEffect:
                    volume = _soundConfig.SoundsVolume;
                    break;
                default:
                    volume = 1;

                    Debug.LogError("Uncknow soud type");
                    break;
            }

            _audioSource.volume = volume;
        }
    }
}
