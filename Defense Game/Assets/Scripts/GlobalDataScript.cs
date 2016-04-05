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
    public GameObject LightProjectile;
    public GameObject DarkProjectile;
    public GameObject TeslaProjectile;
    public GameObject RadiationProjectile;
    public List<Weapon> equippedWeapons;
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
    public int turretLevel;
    public int maxAbilityLevel = 5;
    public int maxTurretLevel = 4;
    public int maxNukes = 3;
    public int maxFreezes = 10;
    public int maxDoublers = 5;
    public AudioClip fireWeaponSound;
    public AudioClip iceWeaponSound;
    public AudioClip lightWeaponSound;
    public AudioClip darkWeaponSound;
    public AudioClip teslaWeaponSound;
    public AudioClip radiationWeaponSound;
    public EventSystem eventHandler;
    bool isHardModeActive;
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
        turretLevel = 4;
        isHardModeActive = false;
        powerups = new List<Powerup>();
        powerups.Add(new Powerup("destroy"));
        powerups.Add(new Powerup("freeze"));
        powerups.Add(new Powerup("damage"));
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
        weaponList.Add(new Weapon("Plasma blast", true, "FireProjectile", .09f, .01f, 5, 2, 10, 1.5f, .5f, .05f, "burn stack", 150f, 45));
        weaponList.Add(new Weapon("Ice shot", true, "IceProjectile", .7f, .05f, 10, 3, 5, 1, 2.5f, .2f, "freeze", 100f));
        weaponList.Add(new Weapon("Light beam", true, "LightProjectile", 3, .3f, 50, 20, 2, 1f, 80, 7, "flood of light", 000f));
        weaponList.Add(new Weapon("Dark wave", true, "DarkProjectile", .35f, .02f, 15, 5, 5, 1, 5f, .3f, "pierce", 100f));
        weaponList.Add(new Weapon("Tesla bolt", true, "TeslaProjectile", .23f, .012f, 10, 3, 2, .5f, 2.5f, .2f, "chain lightning", 100f));
        weaponList.Add(new Weapon("Radiation slug", true, "RadiationProjectile", .23f, .012f, 5, 2, 7, 1.2f, .5f, .05f, "poison", 70f));
        equippedWeapons = new List<Weapon>(weaponList);
        equippedWeapons[0].equipped = 0;
        equippedWeapons[1].equipped = 1;
        equippedWeapons[2].equipped = 2;
        equippedWeapons[3].equipped = 3;
        equippedWeapons[4].equipped = 4;
        equippedWeapons[5].equipped = 5;
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
            globalData.gold = globalData.gold + 100000000;
            if (GameObject.FindGameObjectWithTag("GoldCount") != null)
            {
                GameObject.FindGameObjectWithTag("GoldCount").GetComponent<UnityEngine.UI.Text>().text = GlobalDataScript.globalData.gold.ToString();
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Tutorial skipped.");
            globalData.tutorialState = 3;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Hard mode toggled.");
            isHardModeActive = !isHardModeActive;
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
        data.equippedWeapons = equippedWeapons;
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
            equippedWeapons = data.equippedWeapons;
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
        else if (projectileType == "LightProjectile")
        {
            return LightProjectile;
        }
        else if (projectileType == "DarkProjectile")
        {
            return DarkProjectile;
        }
        else if (projectileType == "TeslaProjectile")
        {
            return TeslaProjectile;
        }
        else if (projectileType == "RadiationProjectile")
        {
            return RadiationProjectile;
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
        else if (projectileType == "LightProjectile")
        {
            return lightWeaponSound;
        }
        else if (projectileType == "DarkProjectile")
        {
            return darkWeaponSound;
        }
        else if (projectileType == "TeslaProjectile")
        {
            return teslaWeaponSound;
        }
        else if (projectileType == "RadiationProjectile")
        {
            return radiationWeaponSound;
        }
        else
        {
            return null;
        }
    }

    public void UpgradeAbility(string abilityType)
    {
        switch(abilityType)
        {
            case "Armor":
                if(armorAbilityLevel<maxAbilityLevel)
                {
                    gold = gold - (int)(Mathf.Pow(armorAbilityLevel + 2, 3) * 500);
                    armorAbilityLevel += 1;
                }
                break;
            case "Regen":
                if (regenAbilityLevel < maxAbilityLevel)
                {
                    gold = gold - (int)(Mathf.Pow(regenAbilityLevel + 2, 3) * 500);
                    regenAbilityLevel += 1;
                }
                break;
            case "Wave":
                if (chronoWaveAbilityLevel < maxAbilityLevel)
                {
                    gold = gold - (int)(Mathf.Pow(chronoWaveAbilityLevel + 2, 3) * 500);
                    chronoWaveAbilityLevel += 1;
                }
                break;
            case "Turret":
                if (turretLevel < maxTurretLevel)
                {
                    gold = gold - (int)(Mathf.Pow(turretLevel + 2, 3) * 5000);
                    turretLevel += 1;
                }
                break;
            case "Rewind":
                if (timeAbilityLevel < maxAbilityLevel)
                {
                    gold = gold - (int)(Mathf.Pow(timeAbilityLevel + 2, 3) * 500);
                    timeAbilityLevel += 1;
                }
                break;
            case "Spike":
                if (spikeAbilityLevel < maxAbilityLevel)
                {
                    gold = gold - (int)(Mathf.Pow(spikeAbilityLevel + 2, 3) * 500);
                    spikeAbilityLevel += 1;
                }
                break;
            case "Streak":
                if (hotStreakAbilityLevel < maxAbilityLevel)
                {
                    gold = gold - (int)(Mathf.Pow(hotStreakAbilityLevel + 2, 3) * 500);
                    hotStreakAbilityLevel += 1;
                }
                break;
            case "Precision":
                if (precisionAbilityLevel < maxAbilityLevel)
                {
                    gold = gold - (int)(Mathf.Pow(precisionAbilityLevel + 2, 3) * 500);
                    precisionAbilityLevel += 1;
                }
                break;

        }
    }
    public void AddPowerup(string powerupType)
    {        
        for (int i = 0; i < powerups.Count; i++)
        {
            if (powerupType == powerups[i].type)
            {
                powerups[i].editCount(1);
                
            }
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
        if (powerups[index].count > 0)
        {
            powerups[index].editCount(-1);
            Debug.Log("Count deducted");
            Debug.Log(powerups[index].count);
            GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemyList)
            {
                ExecuteEvents.Execute<ICustomMessageTarget>(enemy, null, (x, y) => x.UsePowerup(powerups[index].type));
            }

            GameObject[] buttonList = GameObject.FindGameObjectsWithTag("PowerupButton");
            foreach (GameObject button in buttonList)
            {
                ExecuteEvents.Execute<IPowerUpdate>(button, null, (x, y) => x.InfoUpdate());
            }
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
    public int rotation;
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
    public float deltaFireRate;
    public int deltaDamage;
    public float deltaChargeRate;
    public float deltaChargePerShot;
    public int maxLevel;
    public string weaponSoundType;
    public float speed;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="weaponName"> Name of the weapon. </param>
    /// <param name="startUnlocked"> Indicates whether the weapon must be purchased before being usable. </param>
    /// <param name="projectileType"> The name of the projectile this weapon uses. </param>
    /// <param name="fireRate"> The initial time between shots fired. </param>
    /// <param name="maxFireRate">The maximum fire rate level.</param>
    /// <param name="damage">The initial damage dealt by the weapon.</param>
    /// <param name="maxDamage">The maximum damage level.</param>
    /// <param name="chargeRate"> The initial amount of charge restored per tick. </param>
    /// <param name="maxChargeRate">The maximum charge rate level.</param>
    /// <param name="chargePerShot">The initial amount of charge consumed per shot.</param>
    /// <param name="maxChargePerShot">The maximum charge consumption level.</param>
    /// <param name="perk">The special ability of the weapon.</param>
    /// <param name="maxPerk">The maximum level of the special ability.</param>
    /// <param name="speed">The speed of the weapon's projectile.</param>
    public Weapon(string weaponName, bool startUnlocked, string projectileType, float fireRate, float fireRateIncrement, int damage, int damageIncrement,
        float chargeRate, float chargeRateIncrement, float chargePerShot, float chargePerShotIncrement, string perk, float projectileSpeed, int rotationOffset = 90)
    {
        name = weaponName;
        maxLevel = 7;
        unlocked = startUnlocked;
        projectile = projectileType;
        baseFireRate = fireRate;
        currentFireRate = baseFireRate;
        fireRateLevel = 0;
        deltaFireRate = fireRateIncrement;        
        baseDamage = damage;
        currentDamage = baseDamage;
        damageLevel = 0;
        deltaDamage = damageIncrement;
        baseChargeRate = chargeRate;
        currentChargeRate = baseChargeRate;
        chargeRateLevel = 0;
        deltaChargeRate = chargeRateIncrement;
        baseChargePerShot = chargePerShot;
        currentChargePerShot = baseChargePerShot;
        chargePerShotLevel = 0;
        deltaChargePerShot = chargePerShotIncrement;
        specialPerk = perk;
        specialPerkLevel = 0;
        speed = projectileSpeed;
        rotation = rotationOffset;
    }

    public void Upgrade(string stat)
    {
        switch (stat)
        {
            case "fireRate":
                if(fireRateLevel<maxLevel)
                {
                    GlobalDataScript.globalData.gold = GlobalDataScript.globalData.gold - (int)(Mathf.Pow(fireRateLevel + 2, 3) * 1000);
                    fireRateLevel = fireRateLevel+1;
                    currentFireRate = baseFireRate - fireRateLevel*deltaFireRate;
                    
                }
                break;
            case "damage":
                if (damageLevel < maxLevel)
                {
                    GlobalDataScript.globalData.gold = GlobalDataScript.globalData.gold - (int)(Mathf.Pow(damageLevel + 2, 3) * 1000);
                    damageLevel = damageLevel+ 1;
                    currentDamage = baseDamage + damageLevel*deltaDamage;
                }
                break;
            case "chargeRate":
                if (chargeRateLevel < maxLevel)
                {
                    GlobalDataScript.globalData.gold = GlobalDataScript.globalData.gold - (int)(Mathf.Pow(chargeRateLevel + 2, 3) * 1000);
                    chargeRateLevel = chargeRateLevel + 1;
                    currentChargeRate = baseChargeRate + chargeRateLevel * deltaChargeRate;
                }
                break;
            case "chargePerShot":
                if (chargePerShotLevel < maxLevel)
                {
                    GlobalDataScript.globalData.gold = GlobalDataScript.globalData.gold - (int)(Mathf.Pow(chargePerShotLevel + 2, 3) * 1000);
                    chargePerShotLevel = chargePerShotLevel+ 1;
                    currentChargePerShot = baseChargePerShot - chargePerShotLevel * deltaChargePerShot;
                }
                break;
            case "perk":
                if (specialPerkLevel < maxLevel)
                {
                    GlobalDataScript.globalData.gold = GlobalDataScript.globalData.gold - (int)(Mathf.Pow(specialPerkLevel + 2, 3) * 1000);
                    specialPerkLevel = specialPerkLevel + 1;
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
    public List<Weapon> equippedWeapons;
    public int gold;    
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