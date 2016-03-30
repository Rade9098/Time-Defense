using UnityEngine;
using System.Collections;

public class AttackSpriteScript : MonoBehaviour 
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
        if (timer >= .5f)
        {
            Destroy(this.gameObject);
        }
	}
}
