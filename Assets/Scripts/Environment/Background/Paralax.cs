using UnityEngine;
using Zenject;

public class Paralax : MonoBehaviour
{
    [SerializeField] private bool _freezYAxis;

    [SerializeField] private float _paralaxScale;

    private Vector3 _lastTargetPosition;

    private Material _material;

    private Transform _target;

    [Inject]
    private void Initialize(Transform playerTransform)
    {
        _target = playerTransform;
    }

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;

        if (_material is null)
        {
            Debug.LogError($"The gameObject {gameObject} doesnt contains a material");
        } 
    }

    private void Update()
    {
        if (_target is not null
            && _material is not null)
        {
            Vector2 direction = _target.position - _lastTargetPosition;
            direction.y = _freezYAxis ? 0 : direction.y;

            _material.mainTextureOffset += direction.normalized * _paralaxScale;
            _lastTargetPosition = _target.position;
        }
    }
}
