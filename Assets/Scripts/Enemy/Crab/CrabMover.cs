using ModestTree;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CrabMover : MonoBehaviour
{
    private bool _isFacingRight;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _reachingDistance;

    private EntityState _currentState;

    [SerializeField] private Transform _currentPoint;
    [SerializeField] private Transform[] _rotePoints;

    private Rigidbody2D _rigidbody;

    private CrabStateController _stateController;

    public void Initialize(Transform[] routePoints) 
    {
        _rotePoints = routePoints;

        _currentPoint = _rotePoints[0];
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_rigidbody is null)
        {
            Debug.LogError($"The gameObject {gameObject} doesnt contains Rigidbody2D component");
        }

        _stateController = GetComponent<CrabStateController>();

        if (_stateController is null)
        {
            Debug.LogError($"The gameObject {gameObject} doesnt contains CrabStateController component");
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

    private void FixedUpdate()
    {
        if (_currentState == EntityState.Patrol)
        {
            float distanceToPoint = Vector2.Distance(transform.position, _currentPoint.position);

            if (distanceToPoint <= _reachingDistance)
            {
                _currentPoint = GetNextRoutePoint();
            }

            Move();
        }
    }

    private void HandleStateChangings(EntityState state)
    {
        _currentState = state;
    }

    private Transform GetNextRoutePoint()
    {
        int currentIndex = _rotePoints.IndexOf(_currentPoint);

        if (currentIndex == _rotePoints.Length - 1)
        {
            return _rotePoints[0];
        }

        return _rotePoints[currentIndex + 1];
    }

    private void Move() 
    {
        Vector2 moveDirection = _currentPoint.position - transform.position;

        moveDirection.x = moveDirection.normalized.x * _movementSpeed;
        moveDirection.y = _rigidbody.velocity.y;

        if ((_isFacingRight && moveDirection.x < 0)
            || (!_isFacingRight && moveDirection.x > 0))
        {
            transform.Flip();

            _isFacingRight = !_isFacingRight;
        }

        _rigidbody.velocity = moveDirection;
    }
}
