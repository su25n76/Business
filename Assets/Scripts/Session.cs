using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session 
{
    private float _money;
    private List<Business> _businesses;

    public IReadOnlyList<Business> Businesses => _businesses;

    public float Money 
    {
        get { return _money; }
        set { _money = value; }
    }

    public Session(GameSettings settings) 
    {
        _businesses = new List<Business>();
        foreach(GameSettings.BusinessSettings _business in settings.businesses) 
        {
            _businesses.Add(new Business(_business));
        }
        _businesses[0].LevelUp();
    }

    public void Update(float deltaTime) 
    { 
        foreach(Business business in _businesses) 
        {
            _money += business.GetIncomeByTime(deltaTime);
        }
    }

    public void LevelUp(Business business) 
    {
        if (business.UpdateCost <= Money) 
        {
            _money -= business.UpdateCost;
            business.LevelUp();
        }    
    }

    public void UpgradeBusiness(Business business,int index) 
    { 
        if (business.Level>0 && !business.UpgradeStatus[index] && business.UpgradeList[index].cost <= Money) 
        {
            _money -= business.UpgradeList[index].cost;
            business.UpgradeBusiness(index);
        }
    }
}
