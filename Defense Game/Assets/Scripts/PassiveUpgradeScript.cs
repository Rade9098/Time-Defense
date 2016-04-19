using UnityEngine;
using System.Collections;

public class PassiveUpgradeScript : MonoBehaviour
{


    public GameObject armorText;
    public GameObject regenText;
    public GameObject waveText;
    public GameObject turretText;
    public GameObject armorCost;
    public GameObject regenCost;
    public GameObject waveCost;
    public GameObject turretCost;
    public GameObject armorButton;
    public GameObject regenButton;
    public GameObject waveButton;
    public GameObject turretButton;
    public GameObject activePanel;
    public GameObject singleUsePanel;

    // Use this for initialization
    void Start()
    {
        UpdateTexts();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Upgrade(string ability)
    {
        Debug.Log("upgrading");
        GlobalDataScript.globalData.UpgradeAbility(ability);
        Debug.Log("upgrade complete");
        UpdateTexts();
        activePanel.GetComponent<ActiveUpgradeScript>().UpdateTexts();
        singleUsePanel.GetComponent<SingleUseUpgradeScript>().UpdateTexts();
    }


    void OnEnable()
    {
        UpdateTexts();
    }
    public void UpdateTexts()
    {
        Debug.Log("Updating Text");

        armorText.GetComponent<UnityEngine.UI.Text>().text = "Armor Plating: " + (GlobalDataScript.globalData.armorAbilityLevel);
        regenText.GetComponent<UnityEngine.UI.Text>().text = "Regeneration: " + (GlobalDataScript.globalData.regenAbilityLevel);
        waveText.GetComponent<UnityEngine.UI.Text>().text = "Chrono Wave: " + (GlobalDataScript.globalData.chronoWaveAbilityLevel);
        turretText.GetComponent<UnityEngine.UI.Text>().text = "Turrets: " + (GlobalDataScript.globalData.turretLevel);

        //Displays upgrade costs and displays MAX if max upgrade level has been reached.
        if (GlobalDataScript.globalData.armorAbilityLevel < GlobalDataScript.globalData.maxAbilityLevel)
        {
            armorCost.GetComponent<UnityEngine.UI.Text>().text = "" + (Mathf.Pow(GlobalDataScript.globalData.armorAbilityLevel + 2, 3) * 500);
        }
        else
        {
            armorCost.GetComponent<UnityEngine.UI.Text>().text = "MAX";
        }
        if (GlobalDataScript.globalData.regenAbilityLevel < GlobalDataScript.globalData.maxAbilityLevel)
        {
            regenCost.GetComponent<UnityEngine.UI.Text>().text = "" + (Mathf.Pow(GlobalDataScript.globalData.regenAbilityLevel + 2, 3) * 500);
        }
        else
        {
            regenCost.GetComponent<UnityEngine.UI.Text>().text = "MAX";
        }
        if (GlobalDataScript.globalData.chronoWaveAbilityLevel < GlobalDataScript.globalData.maxAbilityLevel)
        {
            waveCost.GetComponent<UnityEngine.UI.Text>().text = "" + (Mathf.Pow(GlobalDataScript.globalData.chronoWaveAbilityLevel + 2, 3) * 500);
        }
        else
        {
            waveCost.GetComponent<UnityEngine.UI.Text>().text = "MAX";
        }
        if (GlobalDataScript.globalData.turretLevel < GlobalDataScript.globalData.maxTurretLevel)
        {
            turretCost.GetComponent<UnityEngine.UI.Text>().text = "" + (Mathf.Pow(GlobalDataScript.globalData.turretLevel + 2, 3) * 5000);
        }
        else
        {
            turretCost.GetComponent<UnityEngine.UI.Text>().text = "MAX";
        }

        //Toggles button interactivity depending on if upgrade can be afforded.
        if (GlobalDataScript.globalData.gold >= Mathf.Pow(GlobalDataScript.globalData.armorAbilityLevel + 2, 3) * 500 && (GlobalDataScript.globalData.armorAbilityLevel < GlobalDataScript.globalData.maxAbilityLevel))
        {
            armorButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            armorButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        if (GlobalDataScript.globalData.gold >= Mathf.Pow(GlobalDataScript.globalData.regenAbilityLevel + 2, 3) * 500 && (GlobalDataScript.globalData.regenAbilityLevel < GlobalDataScript.globalData.maxAbilityLevel))
        {
            regenButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            regenButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        if (GlobalDataScript.globalData.gold >= Mathf.Pow(GlobalDataScript.globalData.chronoWaveAbilityLevel + 2, 3) * 500 && (GlobalDataScript.globalData.chronoWaveAbilityLevel < GlobalDataScript.globalData.maxAbilityLevel))
        {
            waveButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            waveButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        if (GlobalDataScript.globalData.gold >= Mathf.Pow(GlobalDataScript.globalData.turretLevel + 2, 3) * 5000 && (GlobalDataScript.globalData.turretLevel < GlobalDataScript.globalData.maxTurretLevel))
        {
            turretButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            turretButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

    }
}
