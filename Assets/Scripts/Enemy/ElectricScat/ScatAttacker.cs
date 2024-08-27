using System;
using System.Linq;
using System.Collections;
using UnityEngine;

public class ScatAttacker : MonoBehaviour
{
    public event Action OnMadeAttack;

    [SerializeField] private bool _isReload;

    [SerializeField] private float _damage;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _reloadTime;

    private EntityState _currentState;

    private ScatStateController _stateController;

    private void Awake()
    {
        _stateController = GetComponent<ScatStateController>();

        if (_stateController is null)
        {
            Debug.LogError($"The gameObject {gameObject} doesnt contains component ScatStateController");
        }
    }

    private void FixedUpdate()
    {
        if (_currentState == EntityState.Attack
            && !_isReload)
        {
            _isReload = true;

            OnMadeAttack?.Invoke();

            Attack();

            StartCoroutine(MakeReload());
        }
    }

    private void OnEnable()
    {
        if (_stateController is not null)
        {
            _stateController.OnEntityStateChange += HandleStateChangings;
        }
    }

    private void OnDisable()
    {
        if (_stateController is not null)
        {
            _stateController.OnEntityStateChange -= HandleStateChangings;
        }
    }

    private void HandleStateChangings(EntityState newState)
    {
        _currentState = newState;

        Console.WriteLine(newState);
    }

    private void Attack()
    {
        IDamagable[] targets = GetTargetsToHit();

        foreach (var target in targets)
        {
            target.ReciaveDamage(_damage);
        }
    }

    private IDamagable[] GetTargetsToHit()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, _attackRange);

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

        RaycastHit2D[] hitInfo = Physics2D.RaycastAll(transform.position, directionToCollider, _attackRange);

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
        yield return new WaitForSeconds(_reloadTime);

        _isReload = false;
    }
}
