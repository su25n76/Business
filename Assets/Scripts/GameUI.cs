using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public Manager manager;
    public Transform businnesParent;
    public GameObject businnesPrefab;

    public TMP_Text money;

    void Start()
    {
        foreach(Business businnes in manager.Session.Businesses) 
        {
            Instantiate(businnesPrefab, businnesParent).GetComponent<BusinnesUI>().BindUI(businnes,manager.Session);
        }  
    }

    void Update()
    {
        money.text = "Баланс: " + manager.Session.Money + "$";
    }
}
