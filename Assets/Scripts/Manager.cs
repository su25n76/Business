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
            SaveSystem.Save();
        }
        else
        {
            SaveSystem.Load();
        }
        _session.Money = SaveSystem.Date.Money;
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
        SaveSystem.Save();
    }
}
