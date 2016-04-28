using UnityEngine;
using System.Collections;

public class AttackSpriteScript : MonoBehaviour 
{
    float timer;
    public float decayTime;
    public bool ranged = false;
    public bool decays;
    public int killCount = 0;
	// Use this for initialization
	void Start () 
    {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer = timer + Time.deltaTime;
        if (timer >= decayTime && decays)
        {
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Castle" && ranged)
        {
            Destroy(this.gameObject);
        }
    }
}
