using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/GameSettingsObject", order = 1)]
public class GameSettings : ScriptableObject
{
    [System.Serializable]
    public class BusinessUpgrade 
    {
        public string name;
        public int cost;
        public float incomeMultiplier;
    }
    
    [System.Serializable]
    public class BusinessSettings 
    {
        public string name;
        public float delay;
        public int cost;
        public int income;
        public List<BusinessUpgrade> upgrades;
    }

    public List<BusinessSettings> businesses;
}
