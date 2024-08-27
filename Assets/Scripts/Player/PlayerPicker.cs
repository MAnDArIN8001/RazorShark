using UnityEngine;
using Zenject;

public class PlayerPicker : MonoBehaviour
{
    private MoneyController _moneyController;

    [Inject]
    private void Initialize(MoneyController moneyController)
    {
        _moneyController = moneyController;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Treasure>(out var treasure))
        {
            int value = treasure.PickUp();

            _moneyController.TryAddMoney(value);
        }
    }
}
