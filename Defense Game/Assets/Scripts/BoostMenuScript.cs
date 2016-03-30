using UnityEngine;
using System.Collections;

public class BoostMenuScript : MonoBehaviour 
{
    bool isGoldBoostActive;
    bool isHPBoostActive;
    bool isDamageBoostActive;
    string levelName;
    
	// Use this for initialization
	void Start () 
    {
        this.gameObject.SetActive(false);
        isGoldBoostActive = false;
        isHPBoostActive = false;
        isDamageBoostActive = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void UpdateBoosts(int buffType)
    {
        switch (buffType)
        {
            case 1:
                isGoldBoostActive = !isGoldBoostActive;
                break;
            case 2:
                isHPBoostActive = !isHPBoostActive;
                break;
            case 3:
                isDamageBoostActive = !isDamageBoostActive;
                break;
        }
    }

    public void Activate(string name)
    {
        this.gameObject.SetActive(true);
        levelName = name;
    }

    public void LaunchLevel()
    {
        if(isGoldBoostActive)
        {
            if (GlobalDataScript.globalData.gold >= 6000)
            {
                GlobalDataScript.globalData.goldBonus = 1.2f;
                GlobalDataScript.globalData.gold = GlobalDataScript.globalData.gold - 6000;
            }
        }
        
        if (isDamageBoostActive)
        {
            if (GlobalDataScript.globalData.gold >= 3000)
            {
                GlobalDataScript.globalData.damageBonus = 1.5f;
                GlobalDataScript.globalData.gold = GlobalDataScript.globalData.gold - 3000;
            }
        }
        if (isHPBoostActive)
        {
            if (GlobalDataScript.globalData.gold >= 1000)
            {
                GlobalDataScript.globalData.hpBonus = 50;
                GlobalDataScript.globalData.hp = GlobalDataScript.globalData.hp + GlobalDataScript.globalData.hpBonus;
                GlobalDataScript.globalData.gold = GlobalDataScript.globalData.gold - 1000;
            }
        }
        Application.LoadLevel(levelName);
    }
}
