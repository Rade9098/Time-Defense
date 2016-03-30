using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EnemyUpdate : MonoBehaviour, ICustomMessageTarget {
    Rigidbody2D rigidbody;
    //public GlobalDataScript globalData;
    public int hp;
    public int threatValue;
    public int speed;
    public int attack;
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
    static float damageTimer;    
    public static int damageModifier = 1;
    public AudioClip attackSound;
    public GameObject attackSprite;
	// Use this for initialization
	void Start () 
    {
        //damageModifier = 1;
        speed = 75;
        rigidbody = this.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(new Vector2(speed, 0));
        //levelCompleteScreen = GameObject.FindGameObjectWithTag("LevelCompleteScreen");        
        hp = 30;
        threatValue = 1;
        attack = 1;
        attackDistance = 1.6f;
        timer = 0;
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
               if (rigidbody.velocity.x != 0)
               {

                   rigidbody.drag = 1000;

               }
            }
        }
        else
        {
            freezeTimer = freezeTimer - Time.deltaTime;
            if(freezeTimer <= 0)
            {
                freezeActive = false;
                if (transform.position.x < attackDistance)
                {
                    rigidbody.drag = 0;
                    rigidbody.AddForce(new Vector2(speed, 0));
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
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.tag);
        if (other.tag == "FireProjectile")
        {
            hp = (int)(hp - GlobalDataScript.globalData.weaponList[0].currentDamage *damageModifier*GlobalDataScript.globalData.damageBonus);
            Destroy(other.gameObject);
            if(hp<=0)
            {
                int gold = Random.Range(25, 100);
                GlobalDataScript.globalData.gold = GlobalDataScript.globalData.gold + gold;
                GlobalDataScript.globalData.levelGoldTracker = GlobalDataScript.globalData.levelGoldTracker + gold;
                coinPopupInstance = Instantiate(coinPopup, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                coinPopupInstance.GetComponentInChildren<TextMesh>().text= "+" + gold;
                GameObject.FindGameObjectWithTag("GoldCount").GetComponent<UnityEngine.UI.Text>().text = GlobalDataScript.globalData.gold.ToString();
                int random = Random.Range(0,100);
                if(random <=3)
                {
                    Instantiate(powerup1, this.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                }
                else if (random <=7)
                {
                    Instantiate(powerup2, this.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                }
                else if (random <=11)
                {
                    Instantiate(powerup3, this.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                }
                Destroy(this.gameObject);
                
            }
        }
        else if (other.tag == "IceProjectile")
        {
            hp = (int)(hp - GlobalDataScript.globalData.weaponList[1].currentDamage*damageModifier*GlobalDataScript.globalData.damageBonus);
            //Destroy(other.gameObject);
            if (hp <= 0)
            {
                int gold = Random.Range(25, 100);
                GlobalDataScript.globalData.gold = GlobalDataScript.globalData.gold + gold;
                GlobalDataScript.globalData.levelGoldTracker = GlobalDataScript.globalData.levelGoldTracker + gold;
                coinPopupInstance = Instantiate(coinPopup, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                coinPopupInstance.GetComponentInChildren<TextMesh>().text = "+" + gold;
                GameObject.FindGameObjectWithTag("GoldCount").GetComponent<UnityEngine.UI.Text>().text = GlobalDataScript.globalData.gold.ToString();
                int random = Random.Range(0, 100);
                if (random <= 3)
                {
                    Instantiate(powerup1, this.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                }
                else if (random <= 7)
                {
                    Instantiate(powerup2, this.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                }
                else if (random <= 11)
                {
                    Instantiate(powerup3, this.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                }
                Destroy(this.gameObject);
            }
        }
        else if (other.tag == "ChronoWave")
        {
            hp = (int)(hp - GlobalDataScript.globalData.chronoWaveAbilityLevel*10);
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
    }
    void UseDestroyPowerup()
    {
        Destroy(this.gameObject);
    }
    void UseFreezePowerup()
    {
        this.gameObject.GetComponent<Rigidbody2D>().drag = 1000;
        freezeTimer = 5;
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