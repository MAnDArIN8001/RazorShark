using UnityEngine;

public class WaveScroller : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed;

    [SerializeField] private Vector2 _scrollDirection;

    private Material _material;

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        _material.mainTextureOffset += _scrollDirection.normalized * _scrollSpeed;
    }
}
