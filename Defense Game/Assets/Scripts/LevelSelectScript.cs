using UnityEngine;
using System.Collections;

public class LevelSelectScript : MonoBehaviour
{
    public int levelNumber;

	// Use this for initialization
	void Start ()
    {
        if(levelNumber>=2)
        {
            Debug.Log(GlobalDataScript.globalData.completedLevels[levelNumber - 2]);
            if (!GlobalDataScript.globalData.completedLevels[levelNumber - 2])
            {
                this.gameObject.SetActive(false);                
            }
        }
        
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
