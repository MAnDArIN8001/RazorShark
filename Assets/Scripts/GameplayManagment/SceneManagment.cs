using System.Collections;
using UnityEngine;

public class SceneManagment : MonoBehaviour
{
    private bool _isAnimationDone = false;
    private bool _isInLoading;

    [SerializeField] private string _fadeInKey;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void LoadSceneAsync(int sceneIndex)
    {
        if (_isInLoading)
        {
            return;
        }

        _isInLoading = true;
        _animator.SetTrigger(_fadeInKey);

        StartCoroutine(LoadingSceneAsync(sceneIndex));
    }

    private void CloseFadeAnimation()
    {
        _isAnimationDone = true;
    }

    private IEnumerator LoadingSceneAsync(int sceneIndex)
    {
        AsyncOperation scene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);

        scene.allowSceneActivation = false;

        yield return new WaitUntil(() => _isAnimationDone);

        scene.allowSceneActivation = true;
    }
}
