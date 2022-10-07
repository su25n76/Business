using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

public class Business : INotifyPropertyChanged
{
    private int _level;
    private bool[] _upgradeStatus;
    private GameSettings.BusinessSettings _settings;

    private float _payTime;

    public event PropertyChangedEventHandler PropertyChanged;

    public string Name => _settings.name;
    public float PayProgress
    {
        get { return _payTime / _settings.delay; }
        set { _payTime = value * _settings.delay; }
    }
    public int Level
    {
        get { return _level; }
        set
        {
            if (value != _level)
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }
    }

    public float Income
    {
        get 
        { 
            float incomeMultiplier = 1;
            for (int i = 0; i < _upgradeStatus.Length; i++)
            {
                if (_upgradeStatus[i]) incomeMultiplier += _settings.upgrades[i].incomeMultiplier;
            }
            return _level * _settings.income * incomeMultiplier;
        }
    }

    public int UpdateCost
    {
        get 
        {
            return (_level+1) * _settings.cost;
        }
    }

    public bool[] UpgradeStatus 
    {
        get { return _upgradeStatus; }
        set { _upgradeStatus = value; }
    } 
    public List<GameSettings.BusinessUpgrade> UpgradeList => _settings.upgrades;

    public Business(GameSettings.BusinessSettings settings) 
    {
        _settings = settings;
        _upgradeStatus = new bool[settings.upgrades.Count];
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public float GetIncomeByTime(float deltaTime)
    {
        if (_level != 0)
        {
            _payTime += deltaTime;
            if (_payTime > _settings.delay)
            {
                _payTime -= _settings.delay;
                return Income;
            }
        }
        return 0;
    }

    public void LevelUp() 
    {
        Level++;
    }

    public void UpgradeBusiness(int index)
    {
        _upgradeStatus[index] = true;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UpgradeStatus"));
    }
}

public class BusinessSave
{
    public int Level;
    public bool[] UpgradeStatus;
    public float PayProgress;

    public BusinessSave(int level, bool[] upgradeStatus, float payProgress)
    {
        Level = level;
        UpgradeStatus = upgradeStatus;
        PayProgress = payProgress;
    }
}
