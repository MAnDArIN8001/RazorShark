using UnityEngine;

public class SkyController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Destructor>(out var destructor))
        {
            Destroy(gameObject);
        }
    }
}
