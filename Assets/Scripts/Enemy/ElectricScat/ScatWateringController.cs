using UnityEngine;

public class ScatWateringController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_rigidbody is null)
        {
            Debug.LogError($"The gameObject {gameObject} doesnt contains component Rigidbody2D");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Water>(out var water))
        {
            _rigidbody.gravityScale = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Water>(out var water))
        {
            _rigidbody.gravityScale = 1f;
        }
    }
}
