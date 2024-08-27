using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class SharkAttacker : MonoBehaviour
{
    public event Action OnMadeAttack;

    private bool _isReload;

    [SerializeField] private float _damage;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _attackReloadTime;

    private EntityState _currentState;

    private Transform _target;
    [SerializeField] private Transform _attackPoint;

    private SharkStateController _stateController;

    private void Awake()
    {
        _stateController = GetComponent<SharkStateController>();

        if (_stateController is null)
        {
            Debug.LogError($"The gameObject {gameObject} doesnt contains component SharkStateController");
        }
    }

    private void OnEnable()
    {
        if (_stateController is not null)
        {
            _stateController.OnEntityStateChange += HandleStateChanged;
            _stateController.OnDetectEnemy += HandleTargetDetection;
            _stateController.OnLostEnemy += HandleTargetLost;
        }
    }

    private void OnDisable()
    {
        if (_stateController is not null)
        {
            _stateController.OnEntityStateChange -= HandleStateChanged;
            _stateController.OnDetectEnemy -= HandleTargetDetection;
            _stateController.OnLostEnemy -= HandleTargetLost;
        }
    }

    private void FixedUpdate()
    {
        if (_currentState == EntityState.Attack)
        {
            float distanceToTarget = Vector2.Distance(_attackPoint.position, _target.position);

            if (!_isReload && distanceToTarget <= _attackDistance)
            {
                _isReload = true;

                OnMadeAttack?.Invoke();

                Attack();
                StartCoroutine(MakeReload());
            }
        }
    }

    private void HandleStateChanged(EntityState newState)
    {
        _currentState = newState;
    }

    private void HandleTargetDetection(Transform target)
    {
        _target = target;
    }

    private void HandleTargetLost()
    {
        _target = null;
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
        Collider2D[] targets = Physics2D.OverlapCircleAll(_attackPoint.position, _attackDistance);

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

        RaycastHit2D[] hitInfo = Physics2D.RaycastAll(_attackPoint.position, directionToCollider, _attackDistance);

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

        _isReload = false;
    }
}
