using Common;
using Common.GameModes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Unit : MonoBehaviour
{

    [SerializeField] public int hpCount = 10;
    [SerializeField] public TextMeshProUGUI hpUi;
    [SerializeField] private int unitType = -1;

    public string unitTypeString;

    // Start is called before the first frame update
    void Start()
    {
        unitTypeString= getUiBasedOnUnitType(unitType);
        hpUi.text = unitTypeString + hpCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addHpCount(int newHp, int unit_type)
    {
        hpCount += newHp;
        if (hpCount < 0)
        {
            hpUi.text = unitTypeString + hpCount;
            FindObjectOfType<GameUiCanvas>().setLoseText();
            endGame();
        }
        else { 
            hpUi.text = unitTypeString + hpCount;
        }
    }

    private string getUiBasedOnUnitType(int unit_type) {
        switch (unit_type) {
            case 0: return "Horsmen: ";
            case 1: return "Spearmen: ";
            case 2: return "Archers: ";
            default: return "Unknown";
        }
    }

    public int getType()
    {
        return unitType;
    }

    public void endGame() {
        FindObjectOfType<App>().ActivateGameMode(3);
        foreach (var u in FindObjectsOfType<Unit>())
        {
            u.GetComponent<Animator>().Play("Idle");
            u.enabled = false;
        }
        FindObjectOfType<Enemies>().setIdleAnim();
    }
}
