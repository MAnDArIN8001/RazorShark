using UnityEngine;
using Zenject;
using TMPro;
using System.Runtime.CompilerServices;

public class MoneyCounterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private MoneyController _moneyController;

    [Inject] 
    private void Initialize(MoneyController moneyController)
    {
        _moneyController = moneyController;
    }

    private void Awake()
    {
        if (_moneyController is not null)
        {
            _text.text = _moneyController.GetMoneyCount().ToString();
        }
    }

    private void OnEnable()
    {
        if (_moneyController is not null)
        {
            _moneyController.OnMoneyCountChanged += HandleMoneyCountChangings;
        }
    }

    private void OnDisable()
    {
        if (_moneyController is not null)
        {
            _moneyController.OnMoneyCountChanged -= HandleMoneyCountChangings;
        }
    }

    private void HandleMoneyCountChangings(int newMoneyCount)
    {
        _text.text = newMoneyCount.ToString();
    }
}
