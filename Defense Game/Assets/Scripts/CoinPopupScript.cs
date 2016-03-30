using UnityEngine;
using System.Collections;

public class CoinPopupScript : MonoBehaviour {
    float timer;
	// Use this for initialization
	void Start () {
        timer = 0;
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10));
	}
	
	// Update is called once per frame
	void Update () {
        timer = timer + Time.deltaTime;
        if(timer >=2)
        {
            Destroy(this.gameObject);
        }
	}
}
