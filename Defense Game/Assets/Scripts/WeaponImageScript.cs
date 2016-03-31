using UnityEngine;
using System.Collections;

public class WeaponImageScript : MonoBehaviour 
{    
    public bool isPrimary = true;
	// Use this for initialization
	void Start () 
    {
        if (isPrimary)
        {
            this.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = GlobalDataScript.globalData.GetProjectile(GlobalDataScript.globalData.currentPrimary.projectile).GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            this.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = GlobalDataScript.globalData.GetProjectile(GlobalDataScript.globalData.currentSecondary.projectile).GetComponent<SpriteRenderer>().sprite;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
