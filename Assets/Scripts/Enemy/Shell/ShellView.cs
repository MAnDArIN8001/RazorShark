using UnityEngine;

[RequireComponent(typeof(Shell))]
public class ShellView : MonoBehaviour
{
    [SerializeField] private GameObject _destructionEffect;

    private Shell _shell;

    private void Awake()
    {
        _shell = GetComponent<Shell>();

        if (_shell is null)
        {
            Debug.LogError($"The gameObject {gameObject} doesnt contains component Shell");
        }
    }

    private void OnEnable()
    {
        if (_shell is not null)
        {
            _shell.OnDestroyed += HandleDestruction;
        }
    }

    private void OnDisable()
    {
        if (_shell is not null)
        {
            _shell.OnDestroyed -= HandleDestruction;
        }
    }

    private void HandleDestruction()
    {
        Instantiate(_destructionEffect, transform.position, Quaternion.identity);
    }
}
