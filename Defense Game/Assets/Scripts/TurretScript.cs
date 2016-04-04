using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour
{
    public int position;
    Weapon weapon;
    GameObject bulletInstance;
    Rigidbody2D bulletBody;
    float timer;
    float charge;
    float chargeTimer;
    float angle;
	// Use this for initialization
	void Start ()
    {
	if(GlobalDataScript.globalData.turretLevel < position)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            weapon = GlobalDataScript.globalData.equippedWeapons[position + 1];
            angle = this.gameObject.transform.rotation.eulerAngles.z;
            timer = 0;
            charge = 100;
            chargeTimer = 0;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        chargeTimer = chargeTimer + Time.deltaTime;
        timer = timer + Time.deltaTime;

        if (timer >= weapon.currentFireRate*10)
        {            
            if (charge >= weapon.currentChargePerShot*1.2f)
            {
                timer = 0;
                charge = charge - weapon.currentChargePerShot;
                bulletInstance = Instantiate(GlobalDataScript.globalData.GetProjectile(weapon.projectile), transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                SoundManager.singleton.playModulatedSound(GlobalDataScript.globalData.GetProjectileSound(weapon.projectile), .5f);
                if (weapon.specialPerk == "flood of light")
                {
                    bulletInstance.transform.localScale = new Vector3((bulletInstance.transform.localScale.x + (bulletInstance.transform.localScale.x * .2f * weapon.specialPerkLevel)), bulletInstance.transform.localScale.y * 25, bulletInstance.transform.localScale.z);
                    //bulletInstance.GetComponent<BoxCollider2D>().size.Set(bulletInstance.GetComponent<BoxCollider2D>().size.x + bulletInstance.GetComponent<BoxCollider2D>().size.x * .2f * primaryWeapon.specialPerkLevel, bulletInstance.GetComponent<BoxCollider2D>().size.y);
                }

                Vector3 direction = -new Vector3((float)Mathf.Cos(angle), (float)Mathf.Sin(angle), 0);                
                Vector3 targetRotation = bulletInstance.transform.rotation.eulerAngles + new Vector3(0, 0, angle + 180 + GlobalDataScript.globalData.equippedWeapons[position + 1].rotation);

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
                direction.x = direction.x * weapon.speed;
                direction.y = direction.y * weapon.speed;
                //Debug.Log(pineapple);
                bulletBody.AddForce(direction);
                //bulletInstance.velocity = new Vector2(speed, 0);
                //bulletInstance.transform.Rotate(0, 0, Mathf.Atan2(Input.mousePosition.y, Input.mousePosition.x) * Mathf.Rad2Deg);
            }
            if (chargeTimer >= 1 && charge < 100)
            {                
                    chargeTimer = 0;
                
                charge = charge + weapon.currentChargeRate/30;
                if (charge > 100)
                {
                    charge = 100;
                }

            }
        }
    }
}
