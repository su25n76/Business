using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameSettings settings;
    private Session _session;

    public Session Session => _session;
    
    void Awake()
    {
        _session = new Session(settings);    
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Save"))
        {
            SaveGame();
        }
        else
        {
            LoadGame();
        }
        
    }

    void Update()
    {
        _session.Update(Time.deltaTime);    
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) SaveGame();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) SaveGame();
    }

    private void SaveGame()
    {
        SaveSystem.Date.Money = _session.Money;
        SaveSystem.Clear();
        foreach(Business business in _session.Businesses) 
        {
            BusinessSave Date = new BusinessSave(business.Level, business.UpgradeStatus, business.PayProgress);
            SaveSystem.SaveObject(business.Name, JsonUtility.ToJson(Date, false));
        }
        SaveSystem.Save();
    }

    private void LoadGame() 
    {
        SaveSystem.Load();
        _session.Money = SaveSystem.Date.Money;
        foreach (Business business in _session.Businesses) 
        {
            BusinessSave Date = JsonUtility.FromJson<BusinessSave>(SaveSystem.GetObject(business.Name));
            if (Date != null)
            {
                business.UpgradeStatus = Date.UpgradeStatus;
                business.PayProgress = Date.PayProgress;
                business.Level = Date.Level;
            }
        }
        SaveSystem.Clear();
    }
}
