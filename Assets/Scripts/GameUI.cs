using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private Manager manager;
    [SerializeField]
    private Transform businnesParent;
    [SerializeField]
    private GameObject businnesPrefab;

    [SerializeField]
    private TMP_Text money;

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
