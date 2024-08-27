using UnityEngine;

public class SpeedUpgrade : Upgrade<PlayerCharacteristics>
{
    private void Awake()
    {
        if (_moneyController is not null)
        {
            int moneyCount = _moneyController.GetMoneyCount();

            _price = _startPrice + _priceIncreasValue * _controllableConfig.SpeedLvl;
            _upgradeButton.enabled = moneyCount > _price;
        }

        if (_controllableConfig is not null)
        {
            UpdateSliderValue();
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

    private void HandleMoneyCountChangings(int newCount)
    {
        _upgradeButton.enabled = newCount > _price;
    }

    private void UpdateSliderValue()
    {
        float sliderValue = (float)_controllableConfig.SpeedLvl / (float)_lvlsCount;

        _upgradeSlider.value = sliderValue;
    }

    public override void UpgradeStat()
    {
        if (_moneyController.IsEnoughMoney(_price))
        {
            _controllableConfig.SpeedLvl += 1;
            _controllableConfig.Speed += _valueIncreasment;

            _price = _startPrice + _priceIncreasValue * _controllableConfig.SpeedLvl;

            _moneyController.TryRemoveMoney(_price);

            UpdateSliderValue();
        }
    }
}
