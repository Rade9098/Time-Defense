using UnityEngine;
using System.Collections;

public class ActiveUpgradeScript : MonoBehaviour
{


    public GameObject rewindText;
    public GameObject spikeText;
    public GameObject precisionText;
    public GameObject streakText;
    public GameObject rewindCost;
    public GameObject spikeCost;
    public GameObject precisionCost;
    public GameObject streakCost;
    public GameObject rewindButton;
    public GameObject spikeButton;
    public GameObject precisionButton;
    public GameObject streakButton;
    public GameObject passivePanel;
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
        passivePanel.GetComponent<PassiveUpgradeScript>().UpdateTexts();
        singleUsePanel.GetComponent<SingleUseUpgradeScript>().UpdateTexts();
    }
  

    void OnEnable()
    {
        UpdateTexts();
    }
    public void UpdateTexts()
    {
        Debug.Log("Updating Text");

        rewindText.GetComponent<UnityEngine.UI.Text>().text = "Time Rewind: " + (GlobalDataScript.globalData.timeAbilityLevel);
        spikeText.GetComponent<UnityEngine.UI.Text>().text = "Energy Spike: " + (GlobalDataScript.globalData.spikeAbilityLevel);
        precisionText.GetComponent<UnityEngine.UI.Text>().text = "Precision: " + (GlobalDataScript.globalData.precisionAbilityLevel);
        streakText.GetComponent<UnityEngine.UI.Text>().text = "Hot Streak: " + (GlobalDataScript.globalData.hotStreakAbilityLevel);

        //Displays upgrade costs and displays MAX if max upgrade level has been reached.
        if (GlobalDataScript.globalData.timeAbilityLevel < GlobalDataScript.globalData.maxAbilityLevel)
        {
            rewindCost.GetComponent<UnityEngine.UI.Text>().text = "" + (Mathf.Pow(GlobalDataScript.globalData.timeAbilityLevel + 2, 3) * 500);
        }
        else
        {
            rewindCost.GetComponent<UnityEngine.UI.Text>().text = "MAX";
        }
        if (GlobalDataScript.globalData.spikeAbilityLevel < GlobalDataScript.globalData.maxAbilityLevel)
        {
            spikeCost.GetComponent<UnityEngine.UI.Text>().text = "" + (Mathf.Pow(GlobalDataScript.globalData.spikeAbilityLevel + 2, 3) * 500);
        }
        else
        {
            spikeCost.GetComponent<UnityEngine.UI.Text>().text = "MAX";
        }
        if (GlobalDataScript.globalData.precisionAbilityLevel < GlobalDataScript.globalData.maxAbilityLevel)
        {
            precisionCost.GetComponent<UnityEngine.UI.Text>().text = "" + (Mathf.Pow(GlobalDataScript.globalData.precisionAbilityLevel + 2, 3) * 500);
        }
        else
        {
            precisionCost.GetComponent<UnityEngine.UI.Text>().text = "MAX";
        }
        if (GlobalDataScript.globalData.hotStreakAbilityLevel < GlobalDataScript.globalData.maxAbilityLevel)
        {
            streakCost.GetComponent<UnityEngine.UI.Text>().text = "" + (Mathf.Pow(GlobalDataScript.globalData.hotStreakAbilityLevel + 2, 3) * 500);
        }
        else
        {
            streakCost.GetComponent<UnityEngine.UI.Text>().text = "MAX";
        }

        //Toggles button interactivity depending on if upgrade can be afforded.
        if (GlobalDataScript.globalData.gold >= Mathf.Pow(GlobalDataScript.globalData.timeAbilityLevel + 2, 3) * 500 && (GlobalDataScript.globalData.timeAbilityLevel < GlobalDataScript.globalData.maxAbilityLevel))
        {
            rewindButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            rewindButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        if (GlobalDataScript.globalData.gold >= Mathf.Pow(GlobalDataScript.globalData.spikeAbilityLevel + 2, 3) * 500 && (GlobalDataScript.globalData.spikeAbilityLevel < GlobalDataScript.globalData.maxAbilityLevel))
        {
            spikeButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            spikeButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        if (GlobalDataScript.globalData.gold >= Mathf.Pow(GlobalDataScript.globalData.precisionAbilityLevel + 2, 3) * 500 && (GlobalDataScript.globalData.precisionAbilityLevel < GlobalDataScript.globalData.maxAbilityLevel))
        {
            precisionButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            precisionButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        if (GlobalDataScript.globalData.gold >= Mathf.Pow(GlobalDataScript.globalData.hotStreakAbilityLevel + 2, 3) * 500 && (GlobalDataScript.globalData.hotStreakAbilityLevel < GlobalDataScript.globalData.maxAbilityLevel))
        {
            streakButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            streakButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

    }
}
