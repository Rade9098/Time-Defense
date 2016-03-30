using UnityEngine;
using System.Collections;

public class PauseButtonScript : MonoBehaviour 
{
    bool paused;
    
	// Use this for initialization
	void Start () 
    {
        paused = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        }
	}

    public void Pause()
    {
        if(paused)
        {
            Time.timeScale = 1;
            paused = false;
            //this.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 0;
            paused = true;
            //this.gameObject.SetActive(false);
        }
    }
}
