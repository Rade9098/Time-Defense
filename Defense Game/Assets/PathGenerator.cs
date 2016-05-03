using UnityEngine;
using System.Collections;

public class PathGenerator : MonoBehaviour
{
    public GameObject pathTop;
    public GameObject pathMiddle;
    public GameObject pathBottom;
    Vector2 startingTop = new Vector2(4, -.81f);
    Vector2 startingMiddleTop = new Vector2(4.3f, -1.41f);
    Vector2 startingMiddleBottom = new Vector2(4.3f, -2);
    Vector2 startingBottom = new Vector2(4, -2.31f);

    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(pathTop, new Vector2(startingTop.x - i*1.19f, startingTop.y), transform.rotation);
            Instantiate(pathMiddle, new Vector2(startingMiddleTop.x - i * 2 * .59f, startingMiddleTop.y), transform.rotation);
            Instantiate(pathMiddle, new Vector2(startingMiddleTop.x - (i * 2 + 1) * .59f, startingMiddleTop.y), transform.rotation);
            Instantiate(pathMiddle, new Vector2(startingMiddleBottom.x - i * 2 * .59f, startingMiddleBottom.y), transform.rotation);
            Instantiate(pathMiddle, new Vector2(startingMiddleBottom.x - (i * 2 + 1) * .59f, startingMiddleBottom.y), transform.rotation);
            Instantiate(pathBottom, new Vector2(startingBottom.x - i * 1.19f, startingBottom.y), transform.rotation);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
