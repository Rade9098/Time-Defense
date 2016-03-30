using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour 
{
    Rigidbody2D body;
    bool displayed;
    SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () 
    {	
    body = GetComponent<Rigidbody2D>();
    body.MovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //spriteRenderer = GetComponent<SpriteRenderer>()
    if (GlobalDataScript.globalData.tutorialState == 3)
    {
        displayed = true;
            GetComponent<SpriteRenderer>().enabled = true;
            Update();
    }
    else
    {
        //displayed = false;
            //GetComponent<SpriteRenderer>().enabled = false;
            Update();
    }
	}
	
	// Update is called once per frame
	void Update () 
    {
        body = GetComponent<Rigidbody2D>();
        body.MovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
	}

    public void ToggleDisplay()
    {
        if(displayed)
        {
            
            GetComponent<SpriteRenderer>().enabled = false;
            Update();
            displayed = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            Update();
            displayed = true;
        }
    }
}
