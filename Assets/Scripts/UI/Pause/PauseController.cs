using System;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private bool _isGamePaused;

    [SerializeField] private GameObject _pauseMenu;

    public void StopGame()
    {
        Time.timeScale = 0;
        _isGamePaused = true;

        ResetMenus();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _isGamePaused = false;

        ResetMenus();
    }

    private void ResetMenus()
    {
        _pauseMenu.SetActive(_isGamePaused);
    }
}
