using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EnemyUpdate : MonoBehaviour, ICustomMessageTarget {
    Rigidbody2D rigidBody;
    //public GlobalDataScript globalData;
    public int hp;
    public int threatValue;
    public int speed;
    public int attack;
    float precisionMultiplier;
    int burnStack;
    bool isSlowed;
    bool isPoisoned;
    int poisonCounter;
    GameObject[] chainArray;
    public GameObject chainBolt;
    GameObject boltInstance;
    public GameObject iceAoE;
    public GameObject radiationAoE;
    public float attackDistance;
    public GameObject levelCompleteScreen;
    public GameObject coinPopup;
    public GameObject powerup1;
    public GameObject powerup2;
    public GameObject powerup3;
    GameObject coinPopupInstance;
    float timer;
    bool freezeActive;
    static bool damageActive;
    float freezeTimer;
    float slowTimer;
    float poisonTimer;
    static float damageTimer;    
    public static int damageModifier = 1;
    public AudioClip attackSound;
    public GameObject attackSprite;
	// Use this for initialization
	void Start () 
    {
        //damageModifier = 1;
        speed = 75;
        rigidBody = this.GetComponent<Rigidbody2D>();
        rigidBody.AddForce(new Vector2(speed, 0));
        
        //levelCompleteScreen = GameObject.FindGameObjectWithTag("LevelCompleteScreen");        
        hp = 30;
        if(GlobalDataScript.globalData.isHardModeActive)
        {
            hp *= 10;
        }
        threatValue = 1;
        attack = 1;
        attackDistance = 1.6f;
        timer = 0;

        //chainArray = new GameObject[GlobalDataScript.globalData.weaponList[4].maxSpecialPerkLevel];
        burnStack = 0;
        isSlowed = false;
        isPoisoned = false;
        precisionMultiplier = 1;
        //globalData = GameObject.FindGameObjectWithTag("GlobalData").GetComponent<GlobalDataScript>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Debug.Log(rigidbody.velocity.x);
        //Debug.Log(transform.position.x);
        if(!freezeActive)
        { 
           if (transform.position.x >= attackDistance)
            {
                timer = timer + Time.deltaTime;
               if(timer >= 1)
               {
                    int random = Random.Range(0,100);
                    timer = 0;
                    SoundManager.singleton.playModulatedSound(attackSound, 1);
                    Instantiate(attackSprite, this.gameObject.transform.position, this.gameObject.transform.rotation);
                    if (random > GlobalDataScript.globalData.timeAbilityLevel)
                    {
                        GlobalDataScript.globalData.hp = GlobalDataScript.globalData.hp - attack;
                    }
                    else
                    {
                        Debug.Log("Shield ability proc");
                    }
                    if (GlobalDataScript.globalData.hp <0)
                    {
                       GlobalDataScript.globalData.hp = 0;
                    }
                    GameObject.FindGameObjectWithTag("HPCount").GetComponent<UnityEngine.UI.Text>().text = GlobalDataScript.globalData.hp.ToString(); 
                    //Debug.Log(globalData.hp);
                    if(GlobalDataScript.globalData.hp <=0)
                    {
                       GlobalDataScript.globalData.ResetBuffs();
                       levelCompleteScreen.SetActive(true);
                       levelCompleteScreen.gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "GAME OVER";
                       Time.timeScale = 0;
                       //Application.LoadLevel("Overworld Map");
                    }
                    random = Random.Range(0, 100);
                    if (attackDistance >= 1.6f && random <= GlobalDataScript.globalData.spikeAbilityLevel)
                    {
                        Debug.Log("Spike ability proc");
                        hp = (int)(hp - GlobalDataScript.globalData.spikeAbilityLevel * GlobalDataScript.globalData.damageBonus);

                        if (hp <= 0)
                        {
                            int gold = Random.Range(25, 100);
                            GlobalDataScript.globalData.gold = GlobalDataScript.globalData.gold + gold;
                            GlobalDataScript.globalData.levelGoldTracker = GlobalDataScript.globalData.levelGoldTracker + gold;
                            coinPopupInstance = Instantiate(coinPopup, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                            coinPopupInstance.GetComponentInChildren<TextMesh>().text = "+" + gold;
                            GameObject.FindGameObjectWithTag("GoldCount").GetComponent<UnityEngine.UI.Text>().text = GlobalDataScript.globalData.gold.ToString();
                            Destroy(this.gameObject);
                        }
                    }
               }
               if (rigidBody.velocity.x != 0)
               {

                   rigidBody.drag = 1000;

               }
            }
        }
        else
        {
            freezeTimer = freezeTimer - Time.deltaTime;
            if(freezeTimer <= 0)
            {
                Debug.Log("Unfreezing.");
                freezeActive = false;
                if (transform.position.x < attackDistance)
                {
                    rigidBody.drag = 0;
                    rigidBody.AddForce(new Vector2(speed, 0));
                }
            }
        }
        if(damageActive)
        {
            float updatedTime = Time.time;            
            if(updatedTime - damageTimer >= 10)
            {
                damageActive = false;
                damageModifier = 1;
            }
        }
        if (isSlowed)
        {
            slowTimer = slowTimer + Time.deltaTime;
            if (slowTimer >= GlobalDataScript.globalData.weaponList[1].specialPerkLevel)
            {
                isSlowed = false;
                if (GlobalDataScript.globalData.weaponList[1].equipped < 2)
                {
                    rigidBody.AddForce(new Vector2(speed * .7f, 0));
                }
                else
                {
                    rigidBody.AddForce(new Vector2(speed * .5f, 0));
                }
                if (isPoisoned)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                }
                else
                {
                    this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
        if(isPoisoned)
        {
            poisonTimer = poisonTimer + Time.deltaTime;
            if(poisonTimer >=1)
            {
                poisonTimer = 0;
                poisonCounter += 1;
                if (GlobalDataScript.globalData.weaponList[5].equipped < 2)
                {
                    hp = hp - GlobalDataScript.globalData.weaponList[5].specialPerkLevel * 2;
                }
                else
                {
                    hp = hp - GlobalDataScript.globalData.weaponList[5].specialPerkLevel * 2/3;
                }
                CheckDeath();
                if(poisonCounter >=5)
                {
                    isPoisoned = false;
                    if (isSlowed)
                    {
                        this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                    }
                    else
                    {
                        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }

            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
        //Debug.Log(other.tag);
        if (other.tag == "FireProjectile")
        {
            if(GlobalDataScript.globalData.weaponList[0].equipped<2)
            {
                if (Random.Range(0, 100) < GlobalDataScript.globalData.precisionAbilityLevel)
                {
                    precisionMultiplier = (float)GlobalDataScript.globalData.precisionAbilityLevel / 2 + 1;
                    Debug.Log("Critical hit.");
                }
                else
                {
                    precisionMultiplier = 1;
                }
                hp = (int)(hp - (GlobalDataScript.globalData.weaponList[0].currentDamage + burnStack * GlobalDataScript.globalData.weaponList[0].currentDamage / 2) * precisionMultiplier * damageModifier * GlobalDataScript.globalData.damageBonus);
            }
            else
            {
                hp = (int)(hp - (GlobalDataScript.globalData.weaponList[0].currentDamage/3 + burnStack * GlobalDataScript.globalData.weaponList[0].currentDamage / 6) * damageModifier * GlobalDataScript.globalData.damageBonus);
            }
            if (burnStack < GlobalDataScript.globalData.weaponList[0].specialPerkLevel)
            {
                burnStack = burnStack + 1;
            }
            Destroy(other.gameObject);
            CheckDeath();
        }
        else if (other.tag == "IceProjectile")
        {
            Instantiate(iceAoE, other.gameObject.transform.position, other.gameObject.transform.rotation);
            //OnTriggerEnter2D(iceAoE.GetComponent<Collider2D>());
            Destroy(other.gameObject);
            
        }
        else if (other.tag == "Light Projectile")
        {
            if (GlobalDataScript.globalData.weaponList[2].equipped < 2)
            {
                if (Random.Range(0, 100) < GlobalDataScript.globalData.precisionAbilityLevel)
                {
                    precisionMultiplier = (float)GlobalDataScript.globalData.precisionAbilityLevel / 2 + 1;
                    Debug.Log("Critical hit.");
                }
                else
                {
                    precisionMultiplier = 1;
                }
                hp = (int)(hp - GlobalDataScript.globalData.weaponList[2].currentDamage * precisionMultiplier * damageModifier * GlobalDataScript.globalData.damageBonus);
            }
            else
            {
                hp = (int)(hp - GlobalDataScript.globalData.weaponList[2].currentDamage/3 * damageModifier * GlobalDataScript.globalData.damageBonus);
            }
            
            //Destroy(other.gameObject);
            CheckDeath();
        }
        else if (other.tag == "Dark Projectile")
        {
            if (GlobalDataScript.globalData.weaponList[3].equipped < 2)
            {
                if (Random.Range(0, 100) < GlobalDataScript.globalData.precisionAbilityLevel)
                {
                    precisionMultiplier = (float)GlobalDataScript.globalData.precisionAbilityLevel / 2 + 1;
                    Debug.Log("Critical hit.");
                }
                else
                {
                    precisionMultiplier = 1;
                }
                hp = (int)(hp - GlobalDataScript.globalData.weaponList[3].currentDamage * precisionMultiplier * damageModifier * GlobalDataScript.globalData.damageBonus);
                other.GetComponent<DarkProjectileScript>().collisionCounter = other.GetComponent<DarkProjectileScript>().collisionCounter + 1;
                if (other.GetComponent<DarkProjectileScript>().collisionCounter >= GlobalDataScript.globalData.weaponList[3].specialPerkLevel + 1)
                {
                    Destroy(other.gameObject);
                }
            }
            else
            {
                hp = (int)(hp - GlobalDataScript.globalData.weaponList[3].currentDamage/3 * damageModifier * GlobalDataScript.globalData.damageBonus);
                other.GetComponent<DarkProjectileScript>().collisionCounter = other.GetComponent<DarkProjectileScript>().collisionCounter + 1;
                if (other.GetComponent<DarkProjectileScript>().collisionCounter >= GlobalDataScript.globalData.weaponList[3].specialPerkLevel - 1)
                {
                    Destroy(other.gameObject);
                }
            }
            
            CheckDeath();
        }
        else if (other.tag == "Tesla Projectile")
        {
            if (GlobalDataScript.globalData.weaponList[4].equipped < 2)
            {
                if (Random.Range(0, 100) < GlobalDataScript.globalData.precisionAbilityLevel)
                {
                    precisionMultiplier = (float)GlobalDataScript.globalData.precisionAbilityLevel / 2 + 1;
                    Debug.Log("Critical hit.");
                }
                else
                {
                    precisionMultiplier = 1;
                }
                hp = (int)(hp - GlobalDataScript.globalData.weaponList[4].currentDamage * precisionMultiplier * damageModifier * GlobalDataScript.globalData.damageBonus);
                Destroy(other.gameObject);
                chainArray = GameObject.FindGameObjectsWithTag("Enemy");
                GameObject priorEnemy = this.gameObject;
                GameObject currentEnemy;
                if (chainArray.Length > 1)
                {
                    for (int i = 0; i < GlobalDataScript.globalData.weaponList[4].specialPerkLevel; i++)
                    {
                        int randomAddress = Random.Range(0, chainArray.Length - 1);
                        currentEnemy = chainArray[randomAddress];
                        if (currentEnemy == priorEnemy)
                        {
                            if (randomAddress >= chainArray.Length - 1)
                            {
                                currentEnemy = chainArray[randomAddress - 1];
                            }
                            else
                            {
                                currentEnemy = chainArray[randomAddress + 1];
                            }
                        }                        
                        currentEnemy.GetComponent<EnemyUpdate>().hp = (int)(hp - GlobalDataScript.globalData.weaponList[4].currentDamage/2 * precisionMultiplier * damageModifier * GlobalDataScript.globalData.damageBonus);
                        Vector3 direction = currentEnemy.transform.position - priorEnemy.transform.position;
                        direction.z = 0;
                        boltInstance = Instantiate(chainBolt, priorEnemy.transform.position + direction / 2, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;

                        Vector3 targetRotation = boltInstance.transform.rotation.eulerAngles + new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                        boltInstance.transform.Rotate(targetRotation);
                        boltInstance.transform.localScale += new Vector3(direction.magnitude, 0, 0);
                        priorEnemy.GetComponent<EnemyUpdate>().CheckDeath();
                        priorEnemy = currentEnemy;
                    }
                }
            }
            else
            {
                hp = (int)(hp - GlobalDataScript.globalData.weaponList[4].currentDamage/3 * damageModifier * GlobalDataScript.globalData.damageBonus);
                Destroy(other.gameObject);
                chainArray = GameObject.FindGameObjectsWithTag("Enemy");
                GameObject priorEnemy = this.gameObject;
                GameObject currentEnemy;
                if (chainArray.Length > 1)
                {
                    for (int i = 0; i < GlobalDataScript.globalData.weaponList[4].specialPerkLevel-2; i++)
                    {
                        int randomAddress = Random.Range(0, chainArray.Length - 1);
                        currentEnemy = chainArray[randomAddress];
                        if (currentEnemy == priorEnemy)
                        {
                            if (randomAddress >= chainArray.Length - 1)
                            {
                                currentEnemy = chainArray[randomAddress - 1];
                            }
                            else
                            {
                                currentEnemy = chainArray[randomAddress + 1];
                            }
                        }
                        currentEnemy.GetComponent<EnemyUpdate>().hp = (int)(hp - GlobalDataScript.globalData.weaponList[4].currentDamage/6 * damageModifier * GlobalDataScript.globalData.damageBonus);
                        Vector3 direction = currentEnemy.transform.position - priorEnemy.transform.position;
                        direction.z = 0;
                        boltInstance = Instantiate(chainBolt, priorEnemy.transform.position + direction / 2, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;

                        Vector3 targetRotation = boltInstance.transform.rotation.eulerAngles + new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                        boltInstance.transform.Rotate(targetRotation);
                        boltInstance.transform.localScale += new Vector3(direction.magnitude, 0, 0);
                        priorEnemy.GetComponent<EnemyUpdate>().CheckDeath();
                        priorEnemy = currentEnemy;
                    }
                }
            }
            
            CheckDeath();
        }
        else if (other.tag == "Radiation Projectile")
        {
            Instantiate(radiationAoE, other.gameObject.transform.position, other.gameObject.transform.rotation);
            //OnTriggerEnter2D(radiationAoE.GetComponent<Collider2D>());
            Destroy(other.gameObject);
            
        }
        else if (other.tag == "IceAoE")
        {
            
            slowTimer = 0;
            
            if (GlobalDataScript.globalData.weaponList[1].equipped < 2)
            {
                if (Random.Range(0, 100) < GlobalDataScript.globalData.precisionAbilityLevel)
                {
                    precisionMultiplier = (float)GlobalDataScript.globalData.precisionAbilityLevel / 2 + 1;
                    Debug.Log("Critical hit.");
                }
                else
                {
                    precisionMultiplier = 1;
                }
                hp = (int)(hp - GlobalDataScript.globalData.weaponList[1].currentDamage * precisionMultiplier * damageModifier * GlobalDataScript.globalData.damageBonus);
                if (!isSlowed)
                {
                    isSlowed = true;
                    rigidBody.AddForce(new Vector2(-speed * .7f, 0));
                    this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                }
            }
            else
            {
                hp = (int)(hp - GlobalDataScript.globalData.weaponList[1].currentDamage/3 * damageModifier * GlobalDataScript.globalData.damageBonus);
                if (!isSlowed)
                {
                    isSlowed = true;
                    rigidBody.AddForce(new Vector2(-speed * .5f, 0));
                    this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                }
            }
            CheckDeath();
        }
        else if (other.tag == "RadiationAoE")
        {            
            poisonCounter = 0;
            if (!isPoisoned)
            {
                isPoisoned = true;
                poisonTimer = 0;
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            }
            if (GlobalDataScript.globalData.weaponList[5].equipped < 2)
            {
                if (Random.Range(0, 100) < GlobalDataScript.globalData.precisionAbilityLevel)
                {
                    precisionMultiplier = (float)GlobalDataScript.globalData.precisionAbilityLevel / 2 + 1;
                    Debug.Log("Critical hit.");
                }
                else
                {
                    precisionMultiplier = 1;
                }
                hp = (int)(hp - GlobalDataScript.globalData.weaponList[5].currentDamage * precisionMultiplier * damageModifier * GlobalDataScript.globalData.damageBonus);
            }
            else
            {
                hp = (int)(hp - GlobalDataScript.globalData.weaponList[5].currentDamage/3 * damageModifier * GlobalDataScript.globalData.damageBonus);
            }
            CheckDeath();
        }
        else if (other.tag == "ChronoWave")
        {
            hp = (int)(hp - GlobalDataScript.globalData.chronoWaveAbilityLevel * 10);
            //Destroy(other.gameObject);
            if (hp <= 0)
            {
                int gold = Random.Range(25, 100);
                GlobalDataScript.globalData.gold = GlobalDataScript.globalData.gold + gold;
                GlobalDataScript.globalData.levelGoldTracker = GlobalDataScript.globalData.levelGoldTracker + gold;
                coinPopupInstance = Instantiate(coinPopup, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                coinPopupInstance.GetComponentInChildren<TextMesh>().text = "+" + gold;
                GameObject.FindGameObjectWithTag("GoldCount").GetComponent<UnityEngine.UI.Text>().text = GlobalDataScript.globalData.gold.ToString();
                //int random = Random.Range(0, 100);
                //if (random <= 3)
                //{
                //    Instantiate(powerup1, this.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                //}
                //else if (random <= 7)
                //{
                //    Instantiate(powerup2, this.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                //}
                //else if (random <= 11)
                //{
                //    Instantiate(powerup3, this.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                //}
                Destroy(this.gameObject);
            }
        }
        precisionMultiplier = 1;
    }

    public void CheckDeath()
    {
        if (hp <= 0)
        {
            int gold = Random.Range(25, 100);
            GlobalDataScript.globalData.gold = GlobalDataScript.globalData.gold + gold;
            GlobalDataScript.globalData.levelGoldTracker = GlobalDataScript.globalData.levelGoldTracker + gold;
            coinPopupInstance = Instantiate(coinPopup, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            coinPopupInstance.GetComponentInChildren<TextMesh>().text = "+" + gold;
            GameObject.FindGameObjectWithTag("GoldCount").GetComponent<UnityEngine.UI.Text>().text = GlobalDataScript.globalData.gold.ToString();
            int random = Random.Range(0, 100);
            if (random <= GlobalDataScript.globalData.hotStreakAbilityLevel)
            {
                GlobalDataScript.globalData.isStreakActive = true;
            }
            //else if (random <= 7)
            //{
            //    Instantiate(powerup2, this.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            //}
            //else if (random <= 11)
            //{
            //    Instantiate(powerup3, this.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            //}
            Destroy(this.gameObject);
        }
    }

    void UseDestroyPowerup()
    {
        Destroy(this.gameObject);
    }
    void UseFreezePowerup()
    {
        this.gameObject.GetComponent<Rigidbody2D>().drag = 1000;
        freezeTimer = 5;
        freezeActive = true;
    }
    void UseDamagePowerup()
    {
        damageModifier = 2;
        damageTimer = Time.time;
        damageActive = true;
    }

    public void UsePowerup(string type)
    {
        switch (type)
        {
            case "freeze":
                UseFreezePowerup();
                break;
            case "damage":
                UseDamagePowerup();
                break;
            case "destroy":
                UseDestroyPowerup();
                break;  
            default:
                break;
        }
    }
}

public interface ICustomMessageTarget : IEventSystemHandler
{
    // functions that can be called via the messaging system
    void UsePowerup(string type);
}