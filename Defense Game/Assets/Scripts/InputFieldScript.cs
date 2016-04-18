using UnityEngine;
using System.Collections;

public class InputFieldScript : MonoBehaviour 
{
    public GameObject FireProjectile;
    public GameObject chronoWave;
    GameObject bulletInstance;
    public Rigidbody2D bulletBody;
    public float speed = 100f;
    public GameObject IceProjectile;
    public float iceSpeed = 50f;    
    //public GlobalDataScript globalData;
    Weapon primaryWeapon;
    Weapon secondaryWeapon;
    RectTransform primaryChargeRender;
    RectTransform secondaryChargeRender;
    RectTransform hpRender;
    public GameObject dummyCursor;
    GameObject dummyCursorInstance;
    float timer;
    float chronoWaveTimer;
    float streakTimer;
    int streakFireRateModifier;
    int streakChargeRateModifier;
    float hpRegenTimer;
    float primaryCharge;
    float secondaryCharge;
    float chargeTimer;
    int shotCount;
    float tutorialTimer;    
    bool cursorSpawned;
    bool isStreakActive;
    bool isReset;
    bool isFireable;    
    float initialXDeltaPrimary;
    float initialXDeltaSecondary;
    float initialXDeltaHP;
    //public AudioClip fireWeaponSound;
    //public AudioClip iceWeaponSound;
    

	// Use this for initialization
	void Start () 
    {
        timer = 0;
        chargeTimer = 0;
        chronoWaveTimer = 0;
        hpRegenTimer = 0;           
        //globalData = GameObject.FindGameObjectWithTag("GlobalData").GetComponent<GlobalDataScript>();
        primaryWeapon = GlobalDataScript.globalData.equippedWeapons[0];
        secondaryWeapon = GlobalDataScript.globalData.equippedWeapons[1];
        primaryCharge = 100;
        secondaryCharge = 100;
        shotCount = 0;
        tutorialTimer=0;
        cursorSpawned = false;
        isReset = false;
        isFireable = true;
        isStreakActive = false;
        streakChargeRateModifier = 1;
        streakFireRateModifier = 1;
        streakTimer = 0;
        primaryChargeRender = GameObject.FindGameObjectWithTag("PrimaryChargeRender").GetComponent<RectTransform>();
        secondaryChargeRender = GameObject.FindGameObjectWithTag("SecondaryChargeRender").GetComponent<RectTransform>();
        hpRender = GameObject.FindGameObjectWithTag("HPRender").GetComponent<RectTransform>();
        initialXDeltaPrimary = primaryChargeRender.rect.width;
        initialXDeltaSecondary = secondaryChargeRender.rect.width;
        initialXDeltaHP = hpRender.rect.width;                    
	}
	
	// Update is called once per frame
    void Update()
    {
       if(GlobalDataScript.globalData.isStreakActive)
        {
            GlobalDataScript.globalData.isStreakActive = false;
            isStreakActive = true;
            streakFireRateModifier = 2;
            streakChargeRateModifier = 3;
            streakTimer = 0;
            //Debug.Log("Streak Activated.");
        }
       if( isStreakActive)
        {
            streakTimer = streakTimer + Time.deltaTime;
            if(streakTimer > GlobalDataScript.globalData.hotStreakAbilityLevel)
            {
                isStreakActive = false;
                streakTimer = 0;
                streakFireRateModifier = 1;
                streakChargeRateModifier = 1;
                //Debug.Log("Streak Deactivated.");
            }
        }
        chargeTimer = chargeTimer + Time.deltaTime;
        
        timer = timer + Time.deltaTime;

        if(GlobalDataScript.globalData.tutorialState == 3)
        {
            chronoWaveTimer = chronoWaveTimer + Time.deltaTime;
            hpRegenTimer = hpRegenTimer + Time.deltaTime;
            if (chronoWaveTimer >= 10)
            {
                chronoWaveTimer = 0;
                bulletInstance = Instantiate(chronoWave, new Vector2(transform.position.x + 3, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 180))) as GameObject;
                bulletInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100f, 0));
            }
            if(hpRegenTimer >= 5)
            {
                hpRegenTimer = 0;
                GlobalDataScript.globalData.hp = GlobalDataScript.globalData.hp + GlobalDataScript.globalData.regenAbilityLevel;
                if(GlobalDataScript.globalData.hp > 100+GlobalDataScript.globalData.hpBonus + 35 * GlobalDataScript.globalData.armorAbilityLevel)
                {
                    GlobalDataScript.globalData.hp = 100 + GlobalDataScript.globalData.hpBonus + 35 * GlobalDataScript.globalData.armorAbilityLevel;
                }
                GameObject.FindGameObjectWithTag("HPCount").GetComponent<UnityEngine.UI.Text>().text = GlobalDataScript.globalData.hp.ToString();
            }
        }
        if(isFireable)
        {
            if (Input.GetMouseButton(0))
            {

                if (timer >= primaryWeapon.currentFireRate/streakFireRateModifier)
                {

                    if (primaryCharge >= primaryWeapon.currentChargePerShot)
                    {
                        timer = 0;
                        primaryCharge = primaryCharge - primaryWeapon.currentChargePerShot;
                        bulletInstance = Instantiate(GlobalDataScript.globalData.GetProjectile(primaryWeapon.projectile), transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                        SoundManager.singleton.playModulatedSound(GlobalDataScript.globalData.GetProjectileSound(primaryWeapon.projectile), .5f);
                        if(primaryWeapon.specialPerk == "flood of light")
                        {
                            bulletInstance.transform.localScale = new Vector3((bulletInstance.transform.localScale.x + (bulletInstance.transform.localScale.x * .4f * primaryWeapon.specialPerkLevel)), bulletInstance.transform.localScale.y*25, bulletInstance.transform.localScale.z);
                            //bulletInstance.GetComponent<BoxCollider2D>().size.Set(bulletInstance.GetComponent<BoxCollider2D>().size.x + bulletInstance.GetComponent<BoxCollider2D>().size.x * .2f * primaryWeapon.specialPerkLevel, bulletInstance.GetComponent<BoxCollider2D>().size.y);
                        }

                        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Vector3 direction = targetPosition - bulletInstance.transform.position;
                        Vector3 targetRotation = bulletInstance.transform.rotation.eulerAngles + new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg +GlobalDataScript.globalData.equippedWeapons[0].rotation);

                        //Quaternion.Euler(direction);
                        //direction.Normalize();
                        //direction = direction * speed;

                        //bulletInstance.velocity = new Vector2(direction.x, direction.y);
                        bulletInstance.transform.Rotate(targetRotation);
                        bulletBody = bulletInstance.GetComponent<Rigidbody2D>();
                        //direction = direction.normalized;
                        //Debug.Log(pineapple);
                        direction.z = 0;
                        direction = direction.normalized;
                        //Debug.Log(pineapple);
                        direction.x = direction.x * primaryWeapon.speed;
                        direction.y = direction.y * primaryWeapon.speed;
                        //Debug.Log(pineapple);
                        bulletBody.AddForce(direction);
                        //bulletInstance.velocity = new Vector2(speed, 0);
                        //bulletInstance.transform.Rotate(0, 0, Mathf.Atan2(Input.mousePosition.y, Input.mousePosition.x) * Mathf.Rad2Deg);
                    }
                }
            }
            else if (chargeTimer >= 1 && primaryCharge < 100)
            {
                if (secondaryCharge >= 100 || Input.GetMouseButton(1))
                {
                    chargeTimer = 0;
                }
                primaryCharge = primaryCharge + primaryWeapon.currentChargeRate*streakChargeRateModifier;
                if (primaryCharge > 100)
                {
                    primaryCharge = 100;
                }

            }
            if (Input.GetMouseButton(1))
            {

                if (timer >= secondaryWeapon.currentFireRate/streakFireRateModifier)
                {
                    if (secondaryCharge >= secondaryWeapon.currentChargePerShot)
                    {
                        secondaryCharge = secondaryCharge - secondaryWeapon.currentChargePerShot;
                        timer = 0;
                        bulletInstance = Instantiate(GlobalDataScript.globalData.GetProjectile(secondaryWeapon.projectile), transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                        SoundManager.singleton.playModulatedSound(GlobalDataScript.globalData.GetProjectileSound(secondaryWeapon.projectile), .5f);
                        if (secondaryWeapon.specialPerk == "flood of light")
                        {
                            bulletInstance.transform.localScale = new Vector3((bulletInstance.transform.localScale.x + (bulletInstance.transform.localScale.x * .4f * secondaryWeapon.specialPerkLevel)), bulletInstance.transform.localScale.y * 25, bulletInstance.transform.localScale.z);
                            //bulletInstance.GetComponent<BoxCollider2D>().size.Set(bulletInstance.GetComponent<BoxCollider2D>().size.x + bulletInstance.GetComponent<BoxCollider2D>().size.x * .2f * primaryWeapon.specialPerkLevel, bulletInstance.GetComponent<BoxCollider2D>().size.y);
                        }

                        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Vector3 direction = targetPosition - bulletInstance.transform.position;
                        Vector3 targetRotation = bulletInstance.transform.rotation.eulerAngles + new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + GlobalDataScript.globalData.equippedWeapons[1].rotation);

                        //Quaternion.Euler(direction);
                        //direction.Normalize();
                        //direction = direction * speed;

                        //bulletInstance.velocity = new Vector2(direction.x, direction.y);
                        bulletInstance.transform.Rotate(targetRotation);
                        bulletBody = bulletInstance.GetComponent<Rigidbody2D>();
                        //direction = direction.normalized;
                        //Debug.Log(direction);

                        direction.z = 0;
                        direction = direction.normalized;
                        //Debug.Log(direction);
                        direction.x = direction.x * secondaryWeapon.speed;
                        direction.y = direction.y * secondaryWeapon.speed;
                        //Debug.Log(direction);
                        bulletBody.AddForce(direction);
                        //bulletInstance.velocity = new Vector2(speed, 0);
                        //bulletInstance.transform.Rotate(0, 0, Mathf.Atan2(Input.mousePosition.y, Input.mousePosition.x) * Mathf.Rad2Deg);
                    }
                }
            }
            else if (chargeTimer >= 1)
            {
                chargeTimer = 0;
                secondaryCharge = secondaryCharge + secondaryWeapon.currentChargeRate*streakChargeRateModifier;
                if (secondaryCharge > 100)
                {
                    secondaryCharge = 100;
                }

            }
    }
            
            //primaryChargeRender.rect.xMin = primaryChargeRender.rect.xMax - (initialXDeltaPrimary * primaryCharge) / 100;
            //*(primaryChargeRender.anchorMax.x - primaryChargeRender.anchorMin.x)
            //(initialXDeltaPrimary-(primaryCharge * (primaryChargeRender.anchorMax.x - primaryChargeRender.anchorMin.x))/ 100f )
            primaryChargeRender.sizeDelta = new Vector2(-initialXDeltaPrimary+(initialXDeltaPrimary * primaryCharge) / 100-1, primaryChargeRender.sizeDelta.y);
            //Debug.Log("render size:" + primaryChargeRender.sizeDelta.x);
            
            secondaryChargeRender.sizeDelta = new Vector2(-initialXDeltaSecondary+ (initialXDeltaSecondary*secondaryCharge )/100-6, secondaryChargeRender.sizeDelta.y);

            hpRender.sizeDelta = new Vector2(-initialXDeltaHP + (initialXDeltaHP * GlobalDataScript.globalData.hp) / (101+GlobalDataScript.globalData.hpBonus+35*GlobalDataScript.globalData.armorAbilityLevel), hpRender.sizeDelta.y);
        
            if (GlobalDataScript.globalData.tutorialState == 1)
            {
                tutorialTimer = tutorialTimer + Time.deltaTime;
                if(!cursorSpawned)
                {
                    isFireable = false;
                    if(tutorialTimer >=2)
                    {
                        dummyCursorInstance = Instantiate(dummyCursor, new Vector3(-3, -1.5f), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                        cursorSpawned = true;
                    }
                }
                if (tutorialTimer >= 3)
                {                    
                    if(shotCount < 6)
                    {
                        timer = timer + Time.deltaTime;
                        if (timer >= primaryWeapon.baseFireRate)
                        {

                            if (primaryCharge >= primaryWeapon.baseChargePerShot)
                            {
                                timer = 0;
                                shotCount= shotCount+1;
                                primaryCharge = primaryCharge - primaryWeapon.baseChargePerShot;
                                bulletInstance = Instantiate(GlobalDataScript.globalData.GetProjectile(primaryWeapon.projectile), transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                                SoundManager.singleton.playModulatedSound(GlobalDataScript.globalData.GetProjectileSound(primaryWeapon.projectile), .5f);


                                Vector3 targetPosition = new Vector3(-3, -1.5f);
                                Vector3 direction = targetPosition - bulletInstance.transform.position;
                                Vector3 targetRotation = bulletInstance.transform.rotation.eulerAngles + new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg +45);

                                //Quaternion.Euler(direction);
                                //direction.Normalize();
                                //direction = direction * speed;

                                //bulletInstance.velocity = new Vector2(direction.x, direction.y);
                                bulletInstance.transform.Rotate(targetRotation);
                                bulletBody = bulletInstance.GetComponent<Rigidbody2D>();
                                //direction = direction.normalized;
                                //Debug.Log(pineapple);
                                direction.z = 0;
                                direction = direction.normalized;
                                //Debug.Log(pineapple);
                                direction.x = direction.x * speed;
                                direction.y = direction.y * speed;
                                //Debug.Log(pineapple);
                                bulletBody.AddForce(direction);
                                //bulletInstance.velocity = new Vector2(speed, 0);
                                //bulletInstance.transform.Rotate(0, 0, Mathf.Atan2(Input.mousePosition.y, Input.mousePosition.x) * Mathf.Rad2Deg);
                            }
                        }
                    }
                    else if(tutorialTimer >=5)
                    {
                        Destroy(dummyCursorInstance);
                        isFireable = true;
                    }
                }
            }
            if (GlobalDataScript.globalData.tutorialState == 2)
            {
                if(!isReset)
                {
                    cursorSpawned = false;
                    shotCount = 0;
                    isReset = true;
                    tutorialTimer = 0;
                    isFireable = false;
                }
                tutorialTimer = tutorialTimer + Time.deltaTime;
                if (!cursorSpawned)
                {
                    if (tutorialTimer >= 2)
                    {
                        dummyCursorInstance = Instantiate(dummyCursor, new Vector3(-2, -.5f), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                        cursorSpawned = true;
                    }
                }
                if (tutorialTimer >= 3)
                {
                    if (shotCount < 2)
                    {
                        timer = timer + Time.deltaTime;
                        if (timer >= secondaryWeapon.baseFireRate)
                        {
                            if (secondaryCharge >= secondaryWeapon.baseChargePerShot)
                            {
                                secondaryCharge = secondaryCharge - secondaryWeapon.baseChargePerShot;
                                timer = 0;
                                shotCount = shotCount + 1;
                                bulletInstance = Instantiate(GlobalDataScript.globalData.GetProjectile(secondaryWeapon.projectile), transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                                SoundManager.singleton.playModulatedSound(GlobalDataScript.globalData.GetProjectileSound(secondaryWeapon.projectile), 1);


                                Vector3 targetPosition = new Vector3(-2, -.5f);
                                Vector3 direction = targetPosition - bulletInstance.transform.position;
                                Vector3 targetRotation = bulletInstance.transform.rotation.eulerAngles + new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg +90);

                                //Quaternion.Euler(direction);
                                //direction.Normalize();
                                //direction = direction * speed;

                                //bulletInstance.velocity = new Vector2(direction.x, direction.y);
                                bulletInstance.transform.Rotate(targetRotation);
                                bulletBody = bulletInstance.GetComponent<Rigidbody2D>();
                                //direction = direction.normalized;
                                //Debug.Log(pineapple);
                                direction.z = 0;
                                direction = direction.normalized;
                                //Debug.Log(pineapple);
                                direction.x = direction.x * speed;
                                direction.y = direction.y * speed;
                                //Debug.Log(pineapple);
                                bulletBody.AddForce(direction);
                                //bulletInstance.velocity = new Vector2(speed, 0);
                                //bulletInstance.transform.Rotate(0, 0, Mathf.Atan2(Input.mousePosition.y, Input.mousePosition.x) * Mathf.Rad2Deg);
                            }
                        }
                    }
                    else if (tutorialTimer >= 5)
                    {
                        Destroy(dummyCursorInstance);
                        isFireable = true;
                    }
                }
            }
    }

    void OnMouseDown()
    {
        
    }
}
