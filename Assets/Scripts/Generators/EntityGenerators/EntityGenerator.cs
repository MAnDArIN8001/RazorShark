using UnityEngine;

public abstract class EntityGenerator<T> : MonoBehaviour
{
    [SerializeField] protected T _entityPrefab;

    protected abstract void Generate();
}
