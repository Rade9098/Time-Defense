using UnityEngine;
using System.Collections;

public class TooltipScript : MonoBehaviour 
{
    float timer;
	// Use this for initialization
	void Start () 
    {
        timer = 0;
        this.gameObject.SetActive(false);
        //this.GetComponent<RectTransform>().anchorMax = new Vector2(.35f, .1f);
        //this.GetComponent<RectTransform>().anchorMin = new Vector2(.35f, .1f);
        //this.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 0,0);
        //this.GetComponent<RectTransform>().sizeDelta = new Vector2(1.5f, 1);
        //this.GetComponentInChildren<Transform>().SetParent(this.gameObject.transform, false);
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer = timer + Time.deltaTime;
        if(timer >= 5)
        {
            this.gameObject.SetActive(false);
            timer = 0;
        }
	}
}
