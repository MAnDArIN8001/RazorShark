using System;
using UnityEngine;

public class MoneyController
{
    public event Action<int> OnMoneyCountChanged;

    public bool TryAddMoney(int count)
    {
        if (count <= 0)
        {
            Debug.LogError("Money count cant be less than zero");

            return false;
        }

        int lastMoneyCount = PlayerPrefs.GetInt(PlayerPrefsConsts.MoneyStorageKey);
        int newMoneyCount = lastMoneyCount + count;

        PlayerPrefs.SetInt(PlayerPrefsConsts.MoneyStorageKey, newMoneyCount);

        OnMoneyCountChanged?.Invoke(newMoneyCount);

        return true;
    }

    public bool TryRemoveMoney(int count)
    {
        if (count <= 0)
        {
            Debug.LogError("Money count cant be less than zero");

            return false;
        }

        int lastMoneyCount = PlayerPrefs.GetInt(PlayerPrefsConsts.MoneyStorageKey);
        int newMoneyCount = lastMoneyCount - count;

        if (newMoneyCount < 0)
        {
            Debug.LogError("You cant remove more money than u already heave");

            return false;
        }

        PlayerPrefs.SetInt(PlayerPrefsConsts.MoneyStorageKey, newMoneyCount);

        OnMoneyCountChanged?.Invoke(newMoneyCount);

        return true;
    }

    public bool IsEnoughMoney(int moneyCount)
    {
        int currentCount = GetMoneyCount();

        return currentCount >= moneyCount;
    }

    public int GetMoneyCount() => PlayerPrefs.GetInt(PlayerPrefsConsts.MoneyStorageKey);
}
