using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MenuUIController : MonoBehaviour
{
    [SerializeField] private string _isActiveKey;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ShowMenu()
    {
        _animator.SetBool(_isActiveKey, true);
    }

    public void HideMenu()
    {
        _animator.SetBool(_isActiveKey, false);
    }
}
