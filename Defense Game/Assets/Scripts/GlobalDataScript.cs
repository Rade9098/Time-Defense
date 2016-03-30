using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
//using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GlobalDataScript : MonoBehaviour
{
    public static GlobalDataScript globalData;
    public List<Quest> questList;
    public int hp;
    public bool[] completedLevels;
    public List<Weapon> weaponList;
    public int gold;
    public GameObject FireProjectile;
    public GameObject IceProjectile;
    public GameObject RadiantProjectile;
    public Weapon currentPrimary;
    public Weapon currentSecondary;
    public int tutorialState;
    public List<Powerup> powerups;
    public int levelGoldTracker;
    public float goldBonus;
    public int hpBonus;
    public float damageBonus;
    public int timeAbilityLevel;
    public int spikeAbilityLevel;
    public int precisionAbilityLevel;
    public int hotStreakAbilityLevel;
    public int regenAbilityLevel;
    public int armorAbilityLevel;
    public int chronoWaveAbilityLevel;
    public AudioClip fireWeaponSound;
    public AudioClip iceWeaponSound;
    public EventSystem eventHandler;
    void Awake()
    {
        if (globalData == null)
        {
            globalData = this;
            DontDestroyOnLoad(this);
            Start();
        }
        else if (globalData != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        eventHandler.gameObject.SetActive(true);
        //Weapon fireWeapon = new Weapon("Plasma shot", true, FireProjectile, .09f, 7, 5, 7, 10, 7, .5f, 7, "basic", 1);
        //Debug.Log(fireWeapon);
        levelGoldTracker = 0;
        hpBonus = 0;
        goldBonus = 1;
        damageBonus = 1;
        tutorialState = 0;
        timeAbilityLevel = 1;
        spikeAbilityLevel =1;
        precisionAbilityLevel=1;
        hotStreakAbilityLevel=1;
        regenAbilityLevel=1;
        armorAbilityLevel=1;
        chronoWaveAbilityLevel=1;
        powerups = new List<Powerup>();
        questList = new List<Quest>(10);
        questList.Add(new Quest("Quest 1", 3, true, "Beat levels 1, 2, and 3.", new string[3] { "Beat level 1.", "Beat level 2.", "Beat level 3." }));
        questList.Add(new Quest("Quest 2", 1, true, "Buy a new weapon.", new string[1] { "Buy a Weapon." }));
        questList.Add(new Quest("Quest 3", 1, true, "Kill 2 or more enemies with one projectile 30 times.", new string[1] { "Get 30 multikills" }));
        questList.Add(new Quest("Quest 4", 1, true, "Defeat a boss monster.", new string[1] { "Defeat a boss monster." }));
        questList.Add(new Quest("Quest 5", 1, true, "Find 3 secrets hidden on the world map.", new string[1] { "Find 3 secrets" }));
        questList.Add(new Quest("Quest 6", 1, true, "Collect 10,000 gold.", new string[1] { "Get 10,000 gold" }));
        questList.Add(new Quest("Quest 7", 1, true, "Purchase 3 support upgrades.", new string[1] { "Buy 3 support upgrades or repairs." }));
        questList.Add(new Quest("Quest 8", 1, true, "Fully upgrade one weapon.", new string[1] { "Purchase all available upgrades on one weapon." }));
        questList.Add(new Quest("Quest 9", 3, true, "Beat levels 1, 2, and 3 with full health.", new string[3] { "Beat level 1 with full health", "Beat level 2 with full health", "Beat level 3 with full health" }));
        questList.Add(new Quest("Quest 10", 1, true, "Beat level 1 on hard mode.", new string[1] { "Beat level 1 on hard mode." }));
        Debug.Log(questList[0].name);
        weaponList = new List<Weapon>(3);
        //weaponList.Add(fireWeapon);
        weaponList.Add(new Weapon("Plasma shot", true, "FireProjectile", .09f, 7, 5, 7, 10, 7, .5f, 7, "basic", 1));
        weaponList.Add(new Weapon("Ice shot", true, "IceProjectile", .35f, 7, 15, 7, 5, 7, 2.5f, 7, "pierce", 7));
        currentPrimary = weaponList[0];
        currentPrimary.equipped = 0;
        currentSecondary = weaponList[1];
        currentSecondary.equipped = 1;
        completedLevels = new bool[10];
        for (int i = 0; i < completedLevels.Length; i++)
        {
            completedLevels[i] = false;
        }
        if (GameObject.FindGameObjectWithTag("GoldCount") != null)
        {
            GameObject.FindGameObjectWithTag("GoldCount").GetComponent<UnityEngine.UI.Text>().text = GlobalDataScript.globalData.gold.ToString();
        }
        if (GameObject.FindGameObjectWithTag("HPCount") != null)
        {
            GameObject.FindGameObjectWithTag("HPCount").GetComponent<UnityEngine.UI.Text>().text = GlobalDataScript.globalData.hp.ToString();
        }
        //Debug.Log(questList[0].isActive);
        //Debug.Log(questList[0].isCompleted);
        //questList[0].UpdateObjective(1);
        //Debug.Log(questList[0].name);
        //Debug.Log(questList[0].isActive);
        //Debug.Log(questList[0].isCompleted);
        //questList[0].UpdateObjective(2);
        //Debug.Log(questList[0].name);
        //Debug.Log(questList[0].isActive);
        //Debug.Log(questList[0].isCompleted);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("HP Added.");
            globalData.hp = globalData.hp + 10;
            if (GameObject.FindGameObjectWithTag("HPCount") != null)
            {
                GameObject.FindGameObjectWithTag("HPCount").GetComponent<UnityEngine.UI.Text>().text = GlobalDataScript.globalData.hp.ToString();
            }
            if (globalData.hp > 100+globalData.hpBonus)
            {
                globalData.hp = 100 + hpBonus;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Free gold given.");
            if (GameObject.FindGameObjectWithTag("GoldCount") != null)
            {
                GameObject.FindGameObjectWithTag("GoldCount").GetComponent<UnityEngine.UI.Text>().text = GlobalDataScript.globalData.gold.ToString();
            }
            globalData.gold = globalData.gold + 100000000;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Tutorial skipped.");
            globalData.tutorialState = 3;
        }
        //Debug.Log(questList[0].name);
        //Debug.Log(questList);
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerStats.dat");
        SaveData data = new SaveData();
        data.completedLevels = completedLevels;
        data.currentPrimary = currentPrimary;
        data.currentSecondary = currentSecondary;
        data.questList = questList;
        data.weaponList = weaponList;
        data.hp = hp;
        data.gold = gold;
        data.tutorialState = tutorialState;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("data saved");

    }
    public void LoadLevel(string name)
    {
        Application.LoadLevel(name);
    }

    void OnLevelWasLoaded(int level)
    {
        GameObject.FindGameObjectWithTag("GoldCount").GetComponent<UnityEngine.UI.Text>().text = GlobalDataScript.globalData.gold.ToString();
        GameObject.FindGameObjectWithTag("HPCount").GetComponent<UnityEngine.UI.Text>().text = GlobalDataScript.globalData.hp.ToString();
    }

    public void Quit()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerStats.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerStats.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            completedLevels = data.completedLevels;
            currentPrimary = data.currentPrimary;
            currentSecondary = data.currentSecondary;
            questList = data.questList;
            weaponList = data.weaponList;
            hp = data.hp;
            gold = data.gold;
            tutorialState = data.tutorialState;
            Debug.Log("data loaded");
            if (GameObject.FindGameObjectWithTag("GoldCount") != null)
            {
                GameObject.FindGameObjectWithTag("GoldCount").GetComponent<UnityEngine.UI.Text>().text = GlobalDataScript.globalData.gold.ToString();
                GameObject.FindGameObjectWithTag("HPCount").GetComponent<UnityEngine.UI.Text>().text = GlobalDataScript.globalData.hp.ToString();
            }
        }
        else
        {
            Debug.Log("data not found");
        }
    }
    public GameObject GetProjectile(string projectileType)
    {
        if (projectileType == "FireProjectile")
        {
            return FireProjectile;
        }
        else if (projectileType == "IceProjectile")
        {
            return IceProjectile;
        }
        else
        {
            return null;
        }
    }

    public AudioClip GetProjectileSound(string projectileType)
    {
        if (projectileType == "FireProjectile")
        {
            return fireWeaponSound;
        }
        else if (projectileType == "IceProjectile")
        {
            return iceWeaponSound;
        }
        else
        {
            return null;
        }
    }

    public void AddPowerup(string powerupType)
    {
        bool found = false;
        for (int i = 0; i < powerups.Count; i++)
        {
            if (powerupType == powerups[i].type)
            {
                powerups[i].editCount(1);
                found = true;
            }
        }
        if (!found)
        {
            powerups.Add(new Powerup(powerupType));
            Debug.Log("powerup added");
        }
    }

    public void UsePowerup(int index)
    {
        if (this != GlobalDataScript.globalData)
        {
            GlobalDataScript.globalData.UsePowerup(index);
            return;
        }
        Debug.Log("Powerup used");
        Debug.Log(powerups);
        Debug.Log(powerups[index].count);
        powerups[index].editCount(-1);
        Debug.Log("Count deducted");
        Debug.Log(powerups[index].count);
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemyList)
        {
            ExecuteEvents.Execute<ICustomMessageTarget>(enemy, null, (x, y) => x.UsePowerup(powerups[index].type));
        }
        if (powerups[index].count <= 0)
        {
            powerups.RemoveAt(index);
        }
        GameObject[] buttonList = GameObject.FindGameObjectsWithTag("PowerupButton");
        foreach (GameObject button in buttonList)
        {
            ExecuteEvents.Execute<IPowerUpdate>(button, null, (x, y) => x.InfoUpdate());
        }
    }
    public void ResetBuffs()
    {
        if (hp > 0)
        {
            gold = (int)(gold + (goldBonus - 1) * levelGoldTracker);
        }
        levelGoldTracker = 0;
        hpBonus = 0;
        goldBonus = 1;
        damageBonus = 1;
        if (hp > 100)
        {
            hp = 100;
        }
    }
}


[Serializable]
public class Weapon
{
    public string name;
    public bool unlocked;
    public int equipped = -1;
    public string projectile;
    public float baseFireRate;
    public float currentFireRate;
    public int baseDamage;
    public int currentDamage;
    public float baseChargeRate;
    public float currentChargeRate;
    public float baseChargePerShot;
    public float currentChargePerShot;
    public string specialPerk;
    public int fireRateLevel;
    public int damageLevel;
    public int chargeRateLevel;
    public int chargePerShotLevel;
    public int specialPerkLevel;
    public int maxFireRateLevel;
    public int maxDamageLevel;
    public int maxChargeRateLevel;
    public int maxChargePerShotLevel;
    public int maxSpecialPerkLevel;
    public string weaponSoundType;


    public Weapon(string weaponName, bool startUnlocked, string projectileType, float fireRate, int maxFireRate, int damage, int maxDamage,
        float chargeRate, int maxChargeRate, float chargePerShot, int maxChargePerShot, string perk, int maxPerk)
    {
        name = weaponName;
        unlocked = startUnlocked;
        projectile = projectileType;
        baseFireRate = fireRate;
        currentFireRate = baseFireRate;
        fireRateLevel = 0;
        maxFireRateLevel = maxFireRate;
        baseDamage = damage;
        currentDamage = baseDamage;
        damageLevel = 0;
        maxDamageLevel = maxDamage;
        baseChargeRate = chargeRate;
        currentChargeRate = baseChargeRate;
        chargeRateLevel = 0;
        maxChargeRateLevel = maxChargeRate;
        baseChargePerShot = chargePerShot;
        currentChargePerShot = baseChargePerShot;
        chargePerShotLevel = 0;
        maxChargePerShotLevel = maxChargePerShot;
        specialPerk = perk;
        specialPerkLevel = 0;
        maxSpecialPerkLevel = maxPerk;
    }

    public void Upgrade(string stat)
    {
        switch (stat)
        {
            case "fireRate":
                if(fireRateLevel<maxFireRateLevel)
                {
                    GlobalDataScript.globalData.gold = GlobalDataScript.globalData.gold - (int)(Mathf.Pow(fireRateLevel + 2, 3) * 1000);
                    fireRateLevel = fireRateLevel+1;
                    currentFireRate = baseFireRate - fireRateLevel*.01f;
                    
                }
                break;
            case "damage":
                if (damageLevel < maxDamageLevel)
                {
                    GlobalDataScript.globalData.gold = GlobalDataScript.globalData.gold - (int)(Mathf.Pow(damageLevel + 2, 3) * 1000);
                    damageLevel = damageLevel+ 1;
                    currentDamage = baseDamage + damageLevel*2;
                }
                break;
            case "chargeRate":
                if (chargeRateLevel < maxChargeRateLevel)
                {
                    GlobalDataScript.globalData.gold = GlobalDataScript.globalData.gold - (int)(Mathf.Pow(chargeRateLevel + 2, 3) * 1000);
                    chargeRateLevel = chargeRateLevel + 1;
                    currentChargeRate = baseChargeRate + chargeRateLevel * 1.5f;
                }
                break;
            case "chargePerShot":
                if (chargePerShotLevel < maxChargePerShotLevel)
                {
                    GlobalDataScript.globalData.gold = GlobalDataScript.globalData.gold - (int)(Mathf.Pow(chargePerShotLevel + 2, 3) * 1000);
                    chargePerShotLevel = chargePerShotLevel+ 1;
                    currentChargePerShot = baseChargePerShot - chargePerShotLevel * .01f;
                }
                break;
            case "perk":
                if (specialPerkLevel < maxSpecialPerkLevel)
                {
                    GlobalDataScript.globalData.gold = GlobalDataScript.globalData.gold - (int)(Mathf.Pow(specialPerkLevel + 2, 3) * 1000);
                    specialPerkLevel = specialPerkLevel+ 1;
                }
                break;
                
        }
        GameObject.FindGameObjectWithTag("GoldCount").GetComponent<UnityEngine.UI.Text>().text = GlobalDataScript.globalData.gold.ToString();
    }

}

[Serializable]
public class SaveData
{
    public List<Quest> questList;
    public int hp;
    public bool[] completedLevels;
    public List<Weapon> weaponList;
    public int gold;
    public Weapon currentPrimary;
    public Weapon currentSecondary;
    public int tutorialState;
}

public class Powerup
{
    public string type;
    public int count;

    public Powerup(string powerType)
    {
        type = powerType;
        count = 1;
    }

    public void editCount(int change)
    {
        count = count + change;
    }

}