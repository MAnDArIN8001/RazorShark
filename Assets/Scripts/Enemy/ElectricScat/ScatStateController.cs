using System;
using UnityEngine;

public class ScatStateController : MonoBehaviour
{
    public event Action<EntityState> OnEntityStateChange;
    public event Action<Transform> OnDetectEnemy;
    public event Action OnLostEnemy;

    [SerializeField] private float _detectDistance;

    private EntityState _currentState;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHealth>(out var playerHealth))
        {
            if (IsColliderReachable(collision))
            {
                _currentState = EntityState.Attack;

                OnEntityStateChange?.Invoke(_currentState);
                OnDetectEnemy?.Invoke(collision.transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHealth>(out var playerHealth))
        {
            _currentState = EntityState.Patrol;

            OnEntityStateChange?.Invoke(_currentState);
            OnLostEnemy?.Invoke();
        }
    }

    private bool IsColliderReachable(Collider2D collider)
    {
        bool isReachable = true;

        Vector2 directionToCollider = collider.transform.position - transform.position;

        RaycastHit2D[] hitInfo = Physics2D.RaycastAll(transform.position, directionToCollider, _detectDistance);

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
}
