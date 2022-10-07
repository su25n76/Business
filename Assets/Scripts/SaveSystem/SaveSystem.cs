using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem
{
    public static GameDate Date = new GameDate();
    //public static LoadManager Manager = new LoadManager();
    private static Dictionary<string, string> SaveBusinness = new Dictionary<string, string>();

    public static void Save()
    {
        //Date.SaveBusinessesDate.Clear();
        //Manager.SaveStartEvent.Invoke();
        PlayerPrefs.SetString("Save", JsonUtility.ToJson(Date, false));
        Debug.Log("****** SAVE  ******* = " + JsonUtility.ToJson(Date, true));
    }

    public static void Load()
    {
        Date = JsonUtility.FromJson<GameDate>(PlayerPrefs.GetString("Save"));
        foreach (var business in Date.SaveBusinessesDate)
        {
            SaveBusinness.Add(business.Id, business.Date);
        }
        //Manager.LoadSaveEvent.Invoke();
        //SaveBusinness.Clear();
        Debug.Log("****** LOAD  ******* = " + JsonUtility.ToJson(Date, true));
    }

    public static void SaveObject(string id, string business_date)
    {
        Date.SaveBusinessesDate.Add(new BusinessDate(id, business_date));
    }

    public static string GetObject(string ID)
    {
        if (SaveBusinness.ContainsKey(ID))
            return SaveBusinness[ID];
        else
            return null;
    }

    public static void Clear()
    {
        Date.SaveBusinessesDate.Clear();
        SaveBusinness.Clear();
        //Date = new GameDate();
        //Manager.Clear();
    }
}

/*public class LoadManager
{
    public Action SaveStartEvent;
    public Action LoadSaveEvent;

    public void Clear()
    {
        SaveStartEvent = null;
        LoadSaveEvent = null;
    }
}*/

[Serializable]
public class GameDate
{
    public float Money = 0;
    public List<BusinessDate> SaveBusinessesDate = new List<BusinessDate>();
}

[Serializable]
public class BusinessDate
{
    public string Id;
    public string Date;

    public BusinessDate(string key, string date)
    {
        Id = key;
        Date = date;
    }
}
