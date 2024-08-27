using UnityEngine;

public class ParticleView : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectsToOptimize;

    private void Awake()
    {
        SetObjectsState(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHealth>(out var playerHealth))
        {
            SetObjectsState(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHealth>(out var playerHealth))
        {
            SetObjectsState(false);
        }
    }

    private void SetObjectsState(bool newState)
    {
        foreach (var item in _objectsToOptimize)
        {
            item.gameObject.SetActive(newState);
        }
    }
}
