using System;
using System.Collections;
using UnityEngine;

public class CrabAttacker : MonoBehaviour
{
    public event Action OnMakeAttack;

    private bool _isRealod;

    [SerializeField] private float _throwingForce;
    [SerializeField] private float _attackReloadTime;

    private EntityState _currentState;

    private Transform _target;
    [SerializeField] private Transform _attackPoint;

    private CrabStateController _stateController;

    [SerializeField] private Shell _shell;

    private void Awake()
    {
        _stateController = GetComponent<CrabStateController>();

        if (_stateController is null)
        {
            Debug.LogError($"The gameObject {gameObject} doesnt contains component CrabStateController");
        }
    }

    private void OnEnable()
    {
        if (_stateController is not null)
        {
            _stateController.OnEntityStateChange += HandleStateChangings;
            _stateController.OnDetectEnemy += HandleTargetDetection;
            _stateController.OnLostEnemy += HandleTargetLost;
        }
    }

    private void OnDisable()
    {
        if (_stateController is not null)
        {
            _stateController.OnEntityStateChange -= HandleStateChangings;
            _stateController.OnDetectEnemy -= HandleTargetDetection;
            _stateController.OnLostEnemy -= HandleTargetLost;
        }
    }

    private void FixedUpdate()
    {
        if (_currentState == EntityState.Attack)
        {
            if (!_isRealod)
            {
                Attack();

                StartCoroutine(Reload());
            }  
        }
    }

    private void Attack()
    {
        Vector2 direction = _target.position - _attackPoint.position;

        Shell shellInstance = Instantiate(_shell.gameObject, _attackPoint.position, Quaternion.identity).GetComponent<Shell>();

        shellInstance.Throw(direction, _throwingForce);
    }

    private void HandleStateChangings(EntityState newState)
    {
        _currentState = newState;
    }

    private void HandleTargetDetection(Transform newTarget)
    {
        _target = newTarget;
    }

    private void HandleTargetLost()
    {
        _target = null;
    }

    private IEnumerator Reload()
    {
        _isRealod = true;

        yield return new WaitForSeconds(_attackReloadTime);

        _isRealod = false;
    }
}