using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PowerupScript : MonoBehaviour 
{
    public string type;
    bool isAdded;
	// Use this for initialization
	void Start () 
    {
        isAdded = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Cursor")
        {
            GlobalDataScript.globalData.AddPowerup(type);
            GameObject[] buttonList = GameObject.FindGameObjectsWithTag("PowerupButton");
            foreach (GameObject button in buttonList)
            {
                ExecuteEvents.Execute<IPowerUpdate>(button, null, (x, y) => x.InfoUpdate());
            }
            Destroy(this.gameObject);
        }
        
    }
}
