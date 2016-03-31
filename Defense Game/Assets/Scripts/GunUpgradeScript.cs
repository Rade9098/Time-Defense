using UnityEngine;
using System.Collections;

public class GunUpgradeScript : MonoBehaviour
{
    public Weapon weapon;
    public GameObject fireRateText;
    public GameObject damageText;
    public GameObject chargeRateText;
    public GameObject chargeCapacityText;
    public GameObject perkText;
    public GameObject fireRateCost;
    public GameObject damageCost;
    public GameObject chargeRateCost;
    public GameObject chargeCapacityCost;
    public GameObject perkCost;
    public GameObject fireRateButton;
    public GameObject damageButton;
    public GameObject chargeRateButton;
    public GameObject chargeCapacityButton;
    public GameObject perkButton;
    public int weaponType;

	// Use this for initialization
	void Start ()
    {
        weapon = GlobalDataScript.globalData.weaponList[weaponType];
        UpdateTexts();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void Upgrade(string stat)
    {
        Debug.Log("upgrading");
        weapon.Upgrade(stat);
        Debug.Log("upgrade complete");
        UpdateTexts();
    }

    void UpdateTexts()
    {
        Debug.Log("Updating Text");
        fireRateText.GetComponent<UnityEngine.UI.Text>().text = "Fire Rate: " + (weapon.fireRateLevel + 1);
        damageText.GetComponent<UnityEngine.UI.Text>().text = "Damage: " + (weapon.damageLevel + 1);
        chargeRateText.GetComponent<UnityEngine.UI.Text>().text = "Charge Rate: " + (weapon.chargeRateLevel + 1);
        chargeCapacityText.GetComponent<UnityEngine.UI.Text>().text = "Charge Capacity: " + (weapon.chargePerShotLevel + 1);
        perkText.GetComponent<UnityEngine.UI.Text>().text = "Perk Level: " + (weapon.specialPerkLevel);

        //Displays upgrade costs and displays MAX if max upgrade level has been reached.
        if (weapon.fireRateLevel != weapon.maxFireRateLevel)
        {
            fireRateCost.GetComponent<UnityEngine.UI.Text>().text = "" + (Mathf.Pow(weapon.fireRateLevel + 2, 3) * 1000);
        }
        else
        {
            fireRateCost.GetComponent<UnityEngine.UI.Text>().text = "MAX";
        }
        if (weapon.damageLevel != weapon.maxDamageLevel)
        {
            damageCost.GetComponent<UnityEngine.UI.Text>().text = "" + (Mathf.Pow(weapon.damageLevel + 2, 3) * 1000);
        }
        else
        {
            damageCost.GetComponent<UnityEngine.UI.Text>().text = "MAX";
        }
        if (weapon.chargeRateLevel != weapon.maxChargeRateLevel)
        {
            chargeRateCost.GetComponent<UnityEngine.UI.Text>().text = "" + (Mathf.Pow(weapon.chargeRateLevel + 2, 3) * 1000);
        }
        else
        {
            chargeRateCost.GetComponent<UnityEngine.UI.Text>().text = "MAX";
        }
        if (weapon.chargePerShotLevel != weapon.maxChargePerShotLevel)
        {
            chargeCapacityCost.GetComponent<UnityEngine.UI.Text>().text = "" + (Mathf.Pow(weapon.chargePerShotLevel + 2, 3) * 1000);
        }
        else
        {
            chargeCapacityCost.GetComponent<UnityEngine.UI.Text>().text = "MAX";
        }
        if (weapon.specialPerkLevel != weapon.maxSpecialPerkLevel)
        {
            perkCost.GetComponent<UnityEngine.UI.Text>().text = "" + (Mathf.Pow(weapon.specialPerkLevel + 2, 3) * 1000);
        }
        else
        {
            perkCost.GetComponent<UnityEngine.UI.Text>().text = "MAX";
        }

        //Toggles button interactivity depending on if upgrade can be afforded.
        if(GlobalDataScript.globalData.gold >= Mathf.Pow(weapon.fireRateLevel+2, 3)*1000)
        {
            fireRateButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            fireRateButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        
        if (GlobalDataScript.globalData.gold >= Mathf.Pow(weapon.damageLevel + 2, 3) * 1000)
        {
            damageButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            damageButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        if (GlobalDataScript.globalData.gold >= Mathf.Pow(weapon.chargeRateLevel + 2, 3) * 1000)
        {
            chargeRateButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            chargeRateButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        if (GlobalDataScript.globalData.gold >= Mathf.Pow(weapon.chargePerShotLevel + 2, 3) * 1000)
        {
            chargeCapacityButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            chargeCapacityButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        if (GlobalDataScript.globalData.gold >= Mathf.Pow(weapon.specialPerkLevel + 2, 3) * 1000)
        {
            perkButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            perkButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
    }
}
