using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialScript : MonoBehaviour 
{
    int tutorialSubState;
    List<GameObject> tutorialObjectList;
    //float timer;
    

	// Use this for initialization
	void Start () 
    {        
        if (GlobalDataScript.globalData.tutorialState == 0)
        {
            this.gameObject.SetActive(true);
            tutorialObjectList = new List<GameObject>();
            tutorialObjectList.Add(GameObject.FindGameObjectWithTag("Tutorial1A"));
            tutorialObjectList.Add(GameObject.FindGameObjectWithTag("Tutorial1B"));
            tutorialObjectList.Add(GameObject.FindGameObjectWithTag("Tutorial1C"));
            tutorialObjectList.Add(GameObject.FindGameObjectWithTag("Tutorial1D"));
            tutorialObjectList.Add(GameObject.FindGameObjectWithTag("Tutorial1E"));
            tutorialObjectList.Add(GameObject.FindGameObjectWithTag("Tutorial1F"));
            tutorialObjectList.Add(GameObject.FindGameObjectWithTag("Tutorial1G"));
            tutorialObjectList.Add(GameObject.FindGameObjectWithTag("Tutorial1H"));
            tutorialObjectList.Add(GameObject.FindGameObjectWithTag("Tutorial1I"));
            tutorialObjectList.Add(GameObject.FindGameObjectWithTag("Tutorial1J"));
            tutorialObjectList.Add(GameObject.FindGameObjectWithTag("Tutorial2A"));
            Debug.Log(tutorialObjectList);
            foreach (GameObject item in tutorialObjectList)
            {
                Debug.Log(item);
                item.SetActive(false);
            }
            tutorialObjectList[0].SetActive(true);
            Time.timeScale = 0;
            tutorialSubState = 0;
            

        }
        else
        {
            this.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    public void nextTutorial()
    {
        if (tutorialSubState < tutorialObjectList.Count-2)
        {
            tutorialObjectList[tutorialSubState].SetActive(false);
            tutorialSubState = tutorialSubState + 1;
            tutorialObjectList[tutorialSubState].SetActive(true);
        }
        else
        {
            tutorialObjectList[tutorialSubState].SetActive(false);
            this.gameObject.SetActive(false);
            GlobalDataScript.globalData.tutorialState = 1;
            tutorialObjectList[tutorialSubState+1].SetActive(true);            
            Time.timeScale = 1;
        }
    }
}
