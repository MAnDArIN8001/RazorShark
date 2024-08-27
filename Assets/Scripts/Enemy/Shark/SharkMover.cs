using ModestTree;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SharkMover : MonoBehaviour
{
    [SerializeField] private bool _isFacingRight;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _reachingDistance;

    private EntityState _currentState;

    [SerializeField] private Transform _currentPoint;
    [SerializeField] private Transform[] _routePoints;

    private Rigidbody2D _rigidbody;

    private SharkStateController _stateController;

    public void Initialize(Transform[] routePoints)
    {
        _routePoints = routePoints;

        _currentPoint = _routePoints[0];
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_rigidbody is null)
        {
            Debug.LogError($"The gameObject {gameObject} doesnt contains Rigidbody2D component");
        }

        _stateController = GetComponent<SharkStateController>();

        if (_stateController is null)
        {
            Debug.LogError($"The gameObject {gameObject} doesnt contains SharkStateCointroller component");
        }
    }

    private void OnEnable()
    {
        if (_stateController is not null)
        {
            _stateController.OnEntityStateChange += HandleStateChangings;
            _stateController.OnDetectEnemy += HandleDetectedTarget;
            _stateController.OnLostEnemy += HandleLostTarget;
        }
    }

    private void OnDisable()
    {
        if (_stateController is not null)
        {
            _stateController.OnEntityStateChange -= HandleStateChangings;
            _stateController.OnDetectEnemy -= HandleDetectedTarget;
            _stateController.OnLostEnemy -= HandleLostTarget;
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
        else if (_currentState == EntityState.Attack)
        {
            float distanceToPoint = Vector2.Distance(transform.position, _currentPoint.position);

            if (distanceToPoint >= _reachingDistance)
            {
                Move();
            }
        }
    }

    private void HandleStateChangings(EntityState state)
    {
        _currentState = state;
    }

    private void HandleDetectedTarget(Transform newTarget)
    {
        _currentPoint = newTarget;
    }

    private void HandleLostTarget()
    {
        _currentPoint = GetRandomPoint();
    }

    private Transform GetRandomPoint()
    {
        int randomIndex = UnityEngine.Random.Range(0, _routePoints.Length);

        return _routePoints[randomIndex];
    }

    private Transform GetNextRoutePoint()
    {
        int currentIndex = _routePoints.IndexOf(_currentPoint);

        if (currentIndex == _routePoints.Length - 1)
        {
            return _routePoints[0];
        }

        return _routePoints[currentIndex + 1];
    }

    private void Move()
    {
        Vector2 moveDirection = _currentPoint.position - transform.position;

        if ((_isFacingRight && moveDirection.x < 0)
            || (!_isFacingRight && moveDirection.x > 0))
        {
            transform.Flip();

            _isFacingRight = !_isFacingRight;
        }

        _rigidbody.velocity = moveDirection.normalized * _movementSpeed;
    }
}
