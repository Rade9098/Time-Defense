using UnityEngine;
using System.Collections;

public class TurretEquipScript : MonoBehaviour
{
    public GameObject statusText;
    public int turretPosition;
    Component[] buttonList;

    // Use this for initialization
    void Start ()
    {
        buttonList = GetComponentsInChildren<UnityEngine.UI.Button>();
        UpdateText();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void Unlock()
    {
        UpdateText();
    }

    public void Equip(int weaponType)
    {
        int priorSlot;
        priorSlot = GlobalDataScript.globalData.weaponList[weaponType].equipped;
        GlobalDataScript.globalData.weaponList[weaponType].equipped = turretPosition + 1;
        GlobalDataScript.globalData.equippedWeapons[priorSlot] = GlobalDataScript.globalData.equippedWeapons[turretPosition+1];
        GlobalDataScript.globalData.equippedWeapons[priorSlot].equipped = priorSlot;
        GlobalDataScript.globalData.equippedWeapons[turretPosition + 1] = GlobalDataScript.globalData.weaponList[weaponType];        
        foreach (GameObject turretPanel in GameObject.FindGameObjectsWithTag("TurretPanel"))
        {
            turretPanel.GetComponent<TurretEquipScript>().UpdateText();
        }
    }

    public void UpdateText()
    {
        if (GlobalDataScript.globalData.turretLevel >= turretPosition)
        {
            statusText.GetComponent<UnityEngine.UI.Text>().text = "Equipped: " + GlobalDataScript.globalData.equippedWeapons[turretPosition + 1].name;
            foreach (UnityEngine.UI.Button button in buttonList)
            {
                button.interactable = true;
            }
        }
        else
        {
            statusText.GetComponent<UnityEngine.UI.Text>().text = "Locked.";
            foreach (UnityEngine.UI.Button button in buttonList)
            {
                button.interactable = false;
            }
        }
    }
}
