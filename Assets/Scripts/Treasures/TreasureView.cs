using UnityEngine;

[RequireComponent(typeof(Treasure))]
public class TreasureView : MonoBehaviour
{
    [SerializeField] private GameObject _pickUpEffect;

    private Treasure _treasure;

    private void Awake()
    {
        _treasure = GetComponent<Treasure>();

        if (_treasure is null)
        {
            Debug.LogError($"The gameObject {gameObject} doesnt contains a component Treasure");
        }
    }

    private void OnEnable()
    {
        if (_treasure is not null)
        {
            _treasure.OnTreasurePickedUp += HandlePickingUp;
        }
    }

    private void OnDisable()
    {
        if (_treasure is not null)
        {
            _treasure.OnTreasurePickedUp -= HandlePickingUp;
        }
    }

    private void HandlePickingUp()
    {
        Instantiate(_pickUpEffect, transform.position, Quaternion.identity);
    }
}
