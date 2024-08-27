using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class Upgrade<T> : MonoBehaviour
{
    [SerializeField] protected int _lvlsCount;
    [SerializeField] protected int _price;
    [SerializeField] protected int _startPrice;
    [SerializeField] protected int _priceIncreasValue;

    [SerializeField] protected float _valueIncreasment;

    [SerializeField] protected T _controllableConfig;

    [SerializeField] protected Button _upgradeButton;

    [SerializeField] protected Slider _upgradeSlider;

    protected MoneyController _moneyController;

    [Inject]
    protected virtual void Initialize(MoneyController moneyController)
    {
        _moneyController = moneyController;

        if (_upgradeButton is not null)
        {
            _upgradeButton.onClick.AddListener(UpgradeStat);
        }
    }

    public abstract void UpgradeStat();
}
