using UnityEngine;
using System.Collections;

public class ProgressionButtonScript : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Input.GetKeyDown(KeyCode.Space))
        {
            this.gameObject.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        }
	}
}
