using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    private SceneManagment _sceneManager;

    private PlayerHealth _playerHealth;

    [Inject]
    private void Initialize(SceneManagment sceneManagment, PlayerHealth playerHealth)
    {
        _sceneManager = sceneManagment;
        _playerHealth = playerHealth;
    }

    private void OnEnable()
    {
        if (_playerHealth is not null)
        {
            _playerHealth.OnDied += HandlePlayerDeath;
        }
    }

    private void OnDisable()
    {
        if (_playerHealth is not null)
        {
            _playerHealth.OnDied -= HandlePlayerDeath;
        }
    }

    private void HandlePlayerDeath()
    {
        _sceneManager.LoadSceneAsync(ScenesConsts.MenuSceneId);
    }
}
