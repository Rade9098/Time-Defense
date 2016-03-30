using UnityEngine;
using System.Collections;

public class ConfirmationTextPopup : MonoBehaviour
{
    float timer;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer = timer + Time.deltaTime;
        if(timer >=3)
        {
            this.gameObject.SetActive(false);
        }
	}

    public void Activate()
    {
        this.gameObject.SetActive(true);
        timer = 0;
    }
}
