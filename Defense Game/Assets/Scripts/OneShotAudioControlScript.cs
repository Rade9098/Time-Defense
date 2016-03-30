using UnityEngine;
using System.Collections;

public class OneShotAudioControlScript : MonoBehaviour 
{
    float timer;
	// Use this for initialization
	void Start () 
    {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer = timer + Time.deltaTime;
        if(timer > this.gameObject.GetComponent<AudioSource>().clip.length)
        { 
            //Debug.Log("Sound Destroyed.");
            Destroy(this.gameObject);            
        }
	}
}
