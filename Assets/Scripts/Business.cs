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
    public float PayProgress => _payTime / _settings.delay;
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

    public float Incom
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

    public bool[] UpgradeStatus => _upgradeStatus; 
    public List<GameSettings.BusinessUpgrade> UpgradeList => _settings.upgrades;

    public Business(GameSettings.BusinessSettings settings) 
    {
        _settings = settings;
        _upgradeStatus = new bool[settings.upgrades.Count];
        SaveSystem.Manager.SaveStartEvent += Save;
        SaveSystem.Manager.LoadSaveEvent += Load;
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public float Update(float deltaTime)
    {
        if (_level != 0)
        {
            _payTime += deltaTime;
            if (_payTime > _settings.delay)
            {
                _payTime -= _settings.delay;
                return Incom;
            }
            else
            {
                return 0;
            }
        }
        return 0;
    }

    public void LevelUp() 
    {
        Level++;
    }

    public void UpgradeBuisnes(int index)
    {
        _upgradeStatus[index] = true;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UpgradeStatus"));
    }

    public void Save()
    {
        BuisnesSave Date = new BuisnesSave(Level, UpgradeStatus, _payTime);
        SaveSystem.SaveObject(Name, JsonUtility.ToJson(Date, false));
    }

    public void Load()
    {
        BuisnesSave Date = JsonUtility.FromJson<BuisnesSave>(SaveSystem.GetObject(Name));
        if (Date != null)
        {
            _upgradeStatus = Date.UpgradeStatus;
            _payTime = Date.PayTime;
            Level = Date.Level;
        }
    }
}

public class BuisnesSave
{
    public int Level;
    public bool[] UpgradeStatus;
    public float PayTime;

    public BuisnesSave(int level, bool[] upgradeStatus, float payTime)
    {
        Level = level;
        UpgradeStatus = upgradeStatus;
        PayTime = payTime;
    }
}
