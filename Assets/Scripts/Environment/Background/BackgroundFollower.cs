using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BackgroundFollower : MonoBehaviour
{
    [SerializeField] private float _followingSpeed;
    [SerializeField] private float _minDistance;

    [SerializeField] private Vector3 _offset;
    private Vector3 _lastTargetPosition;

    [SerializeField] private Transform _target;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        if (_target is not null)
        {
            Vector2 direction = (_target.position + _offset) - transform.position;

            _rigidbody.velocity = direction * _followingSpeed;
        }
    }
}
