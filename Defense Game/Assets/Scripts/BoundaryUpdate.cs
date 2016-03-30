using UnityEngine;
using System.Collections;

public class BoundaryUpdate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="FireProjectile")
        {
            Destroy(other.gameObject);
        }
        else if (other.tag == "IceProjectile")
        {
            Destroy(other.gameObject);
        }
        else if (other.tag == "ChronoWave")
        {
            if (this.gameObject.transform.position.x < -1)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
