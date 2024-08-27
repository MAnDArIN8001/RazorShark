using System;
using UnityEngine;

public class Treasure : MonoBehaviour, IPickable
{
    public event Action OnTreasurePickedUp;

    [SerializeField] private int _value;

    public int PickUp()
    {
        OnTreasurePickedUp?.Invoke();

        Destroy(gameObject);

        return _value;
    }
}
