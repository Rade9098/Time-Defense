using UnityEngine;
using System.Collections;

public class PopupScript : MonoBehaviour 
{
    bool isTextChanged;
    float timer;
    bool isTextFinished;
	// Use this for initialization
	void Start () 
    {
        isTextChanged = false; //this.gameObject.SetActive(false);
        isTextFinished = false;
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(GlobalDataScript.globalData.tutorialState==2)
        {
            if (!isTextChanged)
            {
                this.gameObject.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>().text = "Looks like there's more on the way. With the way they're all grouped up, a few ice beam shots should do the trick. Aim and fire with the right mouse button. Here's a demonstration.";
                isTextChanged = true;
            }
        }
        if(GlobalDataScript.globalData.tutorialState ==3)
        {
            if(!isTextFinished)
            {
                this.gameObject.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>().text = "Looks like you've got the hang of this. Clear them out and don't give up!";
                isTextFinished = true;
            }
            timer = timer + Time.deltaTime;
            if(timer >= 5)
            {
                Destroy(this.gameObject);
            }
        }
	}
}
