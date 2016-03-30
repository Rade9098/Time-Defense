using UnityEngine;
using System.Collections;

public class TooltipPanelScript : MonoBehaviour 
{
    public GameObject tooltip;
    //GameObject tooltipInstance;
    float tipTimer;
    int oldHP;

	// Use this for initialization
	void Start () 
    {
        tipTimer = 0;
        oldHP = GlobalDataScript.globalData.hp;
        //Debug.Log("spawning tooltip");
        //tooltipInstance = Instantiate(tooltipPrefab);
        //tooltipInstance.transform.SetParent(this.gameObject.transform);
	}
	
	// Update is called once per frame
	void Update () 
    {        
        tipTimer = tipTimer + Time.deltaTime;
        if (tipTimer >= 1)
        {
            if (oldHP - GlobalDataScript.globalData.hp >= 5)
            {
                //spawn powerup tooltip
                //Debug.Log("spawning tooltip");
                //tooltipInstance.transform.SetParent(this.gameObject.transform);
                //tooltipInstance =Instantiate(tooltipPrefab);
                //tooltipInstance.transform.SetParent(this.gameObject.transform, false);                
                tooltip.SetActive(true);
            }
            tipTimer = 0;
            oldHP = GlobalDataScript.globalData.hp;
        }
	}
}
