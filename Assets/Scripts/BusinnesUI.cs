using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BusinnesUI : MonoBehaviour
{
    private Business _businnes;
    private Session _session;

    public TMP_Text name;
    public Slider progress;
    public TMP_Text level;
    public TMP_Text income;
    public TMP_Text levelUpButtonText;
    public TMP_Text upgrade1ButtonText;
    public TMP_Text upgrade2ButtonText;

    public Button levelUpButton;
    public Button upgrade1Button;
    public Button upgrade2Button;

    void Start()
    {
        levelUpButton.onClick.AddListener(LevelUp);
        upgrade1Button.onClick.AddListener(Upgrade1);
        upgrade2Button.onClick.AddListener(Upgrade2);
    }

    void Update()
    {
        progress.value = _businnes.PayProgress;
    }

    public void BindUI(Business business, Session session) 
    {
        _businnes = business;
        _session = session;
        business.PropertyChanged += UpdateUI;

        name.text = _businnes.Name;
        UpdateUI(null,null);
    }

    private void UpdateUI(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        level.text = "LVL\n" + _businnes.Level;
        income.text = "Доход\n" + _businnes.Incom;
        levelUpButtonText.text = "LVL UP\nЦена " + _businnes.UpdateCost + "$";
        if(_businnes.UpgradeStatus[0])
        {
            upgrade1ButtonText.text = _businnes.UpgradeList[0].name + "\nДоход: +" + _businnes.UpgradeList[0].incomeMultiplier * 100 + "%\nКуплено";
        }
        else
        {
            upgrade1ButtonText.text = _businnes.UpgradeList[0].name + "\nДоход: +" + _businnes.UpgradeList[0].incomeMultiplier * 100 + "%\nЦена: " + _businnes.UpgradeList[0].cost + "$";
        }
        if (_businnes.UpgradeStatus[1])
        {
            upgrade2ButtonText.text = _businnes.UpgradeList[1].name + "\nДоход: +" + _businnes.UpgradeList[1].incomeMultiplier * 100 + "%\nКуплено";
        }
        else
        {
            upgrade2ButtonText.text = _businnes.UpgradeList[1].name + "\nДоход: +" + _businnes.UpgradeList[1].incomeMultiplier * 100 + "%\nЦена: " + _businnes.UpgradeList[1].cost + "$";
        }
    }

    public void LevelUp() 
    {
        _session.LevelUp(_businnes);   
    }

    public void Upgrade1() 
    {
        _session.UpgradeBuisnes(_businnes, 0);    
    }

    public void Upgrade2() 
    {
        _session.UpgradeBuisnes(_businnes, 1);
    }
}
