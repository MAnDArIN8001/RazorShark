using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SoundsVolumeController : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundEffectsSlider;

    private SoundEffectsConfig _soundConfig;

    [Inject] 
    private void Initialize(SoundEffectsConfig soundConfig)
    {
        _soundConfig = soundConfig;
    }

    private void Awake()
    {
        if (_soundConfig is not null)
        {
            _musicSlider.value = _soundConfig.MusicVolume;
            _soundEffectsSlider.value = _soundConfig.SoundsVolume;
        }
    }

    private void OnEnable()
    {
        if (_musicSlider is not null)
        {
            _musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
        }

        if (_soundEffectsSlider is not null)
        {
            _soundEffectsSlider.onValueChanged.AddListener(OnSoundEffectsSliderValueChanged);
        }
    }

    private void OnDisable()
    {
        if (_musicSlider is not null)
        {
            _musicSlider.onValueChanged.RemoveListener(OnMusicSliderValueChanged);
        }

        if (_soundEffectsSlider is not null)
        {
            _soundEffectsSlider.onValueChanged.RemoveListener(OnSoundEffectsSliderValueChanged);
        }
    }

    private void OnMusicSliderValueChanged(float newValue)
    {
        _soundConfig.MusicVolume = newValue;
    }

    private void OnSoundEffectsSliderValueChanged(float newValue)
    {
        _soundConfig.SoundsVolume = newValue;
    }
}
