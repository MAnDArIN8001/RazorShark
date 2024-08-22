using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerAttacker : MonoBehaviour
{
    public event Action OnMadeAttack;

    private bool _isAttacking;

    [SerializeField] private float _attackReloadTime;
    [SerializeField] private float _attackRadius;

    [SerializeField] private Transform _attackPoint;

    private Button _attackButton;

    private PlayerCharacteristics _characteristics;

    [Inject]
    private void Initialize(PlayerCharacteristics playerCharacteristics, [Inject(Id = ControlsButton.Attack)] Button attackButton)
    {
        _characteristics = playerCharacteristics;
        _attackButton = attackButton;
    }

    private void OnEnable()
    {
        if (_attackButton is not null)
        {
            _attackButton.onClick.AddListener(HandleAttack);
        }
    }

    private void OnDisable()
    {
        if (_attackButton is not null)
        {
            _attackButton.onClick.RemoveListener(HandleAttack);
        }
    }

    private void HandleAttack()
    {
        if (_isAttacking)
        {
            return;
        }

        _isAttacking = true;

        OnMadeAttack?.Invoke();

        Attack();

        StartCoroutine(MakeReload());
    }

    private void Attack()
    {
        IDamagable[] targets = GetTargetsToHit();

        foreach (var target in targets)
        {
            target.ReciaveDamage(_characteristics.Damage);
        }
    }

    private IDamagable[] GetTargetsToHit()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRadius);

        return targets
            .Where(IsValidTarget)
            .Select(GetDamagable)
            .ToArray();
    }

    private IDamagable GetDamagable(Collider2D collider) => collider.GetComponent<IDamagable>();

    private bool IsValidTarget(Collider2D collider)
    {
        return collider.gameObject != gameObject
            && IsColliderReachable(collider)
            && collider.TryGetComponent<IDamagable>(out var damagable);
    }

    private bool IsColliderReachable(Collider2D collider)
    {
        bool isReachable = true;

        Vector2 directionToCollider = collider.transform.position - transform.position;

        RaycastHit2D[] hitInfo = Physics2D.RaycastAll(_attackPoint.position, directionToCollider, _attackRadius);

        foreach (var info in hitInfo)
        {
            if (info.collider.gameObject == gameObject)
            {
                continue;
            }
            else if (info.collider == collider)
            {
                break;
            }
            else if (info.collider.TryGetComponent<Obstacle>(out Obstacle obstacle))
            {
                isReachable = false;

                break;
            }
        }

        return isReachable;
    }


    private IEnumerator MakeReload()
    {
        yield return new WaitForSeconds(_attackReloadTime);

        _isAttacking = false;
    }
}
