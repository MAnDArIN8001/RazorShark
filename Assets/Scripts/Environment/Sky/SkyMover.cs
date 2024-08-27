using UnityEngine;

public class SkyMover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private Vector3 _direction;

    public void Initialize(Vector3 direction)
    {
        _direction = direction;
    }

    private void Update()
    {
        transform.Translate(_direction * _movementSpeed * Time.deltaTime);
    }
}
