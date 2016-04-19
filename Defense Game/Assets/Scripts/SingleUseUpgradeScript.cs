using UnityEngine;
using System.Collections;

public class SingleUseUpgradeScript : MonoBehaviour
{
       
    public GameObject nukeText;
    public GameObject freezeText;
    public GameObject doublerText;
    public GameObject repairText;
    public GameObject nukeCost;
    public GameObject freezeCost;
    public GameObject doublerCost;
    public GameObject repairCost;
    public GameObject nukeButton;
    public GameObject freezeButton;
    public GameObject doublerButton;
    public GameObject repairButton;
    public GameObject passivePanel;
    public GameObject activePanel;

    // Use this for initialization
    void Start()
    {        
        UpdateTexts();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPowerup(string powerup)
    {
        Debug.Log("upgrading");        
        GlobalDataScript.globalData.AddPowerup(powerup);
        Debug.Log("upgrade complete");
        UpdateTexts();
        activePanel.GetComponent<ActiveUpgradeScript>().UpdateTexts();
        passivePanel.GetComponent<PassiveUpgradeScript>().UpdateTexts();
    }    

    public void Repair()
    {
        GlobalDataScript.globalData.gold += -500;
        GlobalDataScript.globalData.hp += 30;
        if(GlobalDataScript.globalData.hp > GlobalDataScript.globalData.armorAbilityLevel*35 + 100)
        {
            GlobalDataScript.globalData.hp = GlobalDataScript.globalData.armorAbilityLevel * 35 + 100;
        }
        if (GameObject.FindGameObjectWithTag("GoldCount") != null)
        {
            GameObject.FindGameObjectWithTag("GoldCount").GetComponent<UnityEngine.UI.Text>().text = GlobalDataScript.globalData.gold.ToString();
        }
        if (GameObject.FindGameObjectWithTag("HPCount") != null)
        {
            GameObject.FindGameObjectWithTag("HPCount").GetComponent<UnityEngine.UI.Text>().text = GlobalDataScript.globalData.hp.ToString();
        }
        UpdateTexts();
    }

    void OnEnable()
    {
        UpdateTexts();
    }
    public void UpdateTexts()
    {
        Debug.Log("Updating Text");
        
        nukeText.GetComponent<UnityEngine.UI.Text>().text = "Nuclear Bombs: " + (GlobalDataScript.globalData.powerups[0].count);
        freezeText.GetComponent<UnityEngine.UI.Text>().text = "Time Locks: " + (GlobalDataScript.globalData.powerups[1].count);
        doublerText.GetComponent<UnityEngine.UI.Text>().text = "Damage Doublers: " + (GlobalDataScript.globalData.powerups[2].count);
        repairText.GetComponent<UnityEngine.UI.Text>().text = "Repair Castle: HP +30";

        //Displays upgrade costs and displays MAX if max upgrade level has been reached.
        if (GlobalDataScript.globalData.powerups[0].count < GlobalDataScript.globalData.maxNukes)
        {
            nukeCost.GetComponent<UnityEngine.UI.Text>().text = "" + (15000);
        }
        else
        {
            nukeCost.GetComponent<UnityEngine.UI.Text>().text = "MAX";
        }
        if (GlobalDataScript.globalData.powerups[1].count < GlobalDataScript.globalData.maxFreezes)
        {
            freezeCost.GetComponent<UnityEngine.UI.Text>().text = "" + (5000);
        }
        else
        {
            freezeCost.GetComponent<UnityEngine.UI.Text>().text = "MAX";
        }
        if (GlobalDataScript.globalData.powerups[2].count < GlobalDataScript.globalData.maxDoublers)
        {
            doublerCost.GetComponent<UnityEngine.UI.Text>().text = "" + (8000);
        }
        else
        {
            doublerCost.GetComponent<UnityEngine.UI.Text>().text = "MAX";
        }
        if (GlobalDataScript.globalData.hp< 100+GlobalDataScript.globalData.armorAbilityLevel*35)
        {
            repairCost.GetComponent<UnityEngine.UI.Text>().text = "" + (500);
        }
        else
        {
            repairCost.GetComponent<UnityEngine.UI.Text>().text = "HP FULL";
        }

        //Toggles button interactivity depending on if upgrade can be afforded.
        if (GlobalDataScript.globalData.gold >= 15000 && GlobalDataScript.globalData.powerups[0].count < GlobalDataScript.globalData.maxNukes)
        {
            nukeButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            nukeButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        if (GlobalDataScript.globalData.gold >= 5000 && GlobalDataScript.globalData.powerups[1].count < GlobalDataScript.globalData.maxFreezes)
        {
            freezeButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            freezeButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        if (GlobalDataScript.globalData.gold >= 8000 && GlobalDataScript.globalData.powerups[2].count < GlobalDataScript.globalData.maxDoublers)
        {
            doublerButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            doublerButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        if (GlobalDataScript.globalData.gold >= 500 && GlobalDataScript.globalData.hp < 100 + GlobalDataScript.globalData.armorAbilityLevel * 35)
        {
            repairButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            repairButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        
    }
}
