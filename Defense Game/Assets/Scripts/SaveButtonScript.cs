using UnityEngine;
using System.Collections;

public class SaveButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Save()
    {
        GlobalDataScript.globalData.Save();
    }

    public void Load()
    {
        GlobalDataScript.globalData.Load();
    }
}
