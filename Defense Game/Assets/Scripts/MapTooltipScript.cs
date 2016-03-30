using UnityEngine;
using System.Collections;

public class MapTooltipScript : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	    if(GlobalDataScript.globalData.tutorialState!=0)
        {
            this.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
