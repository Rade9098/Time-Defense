using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour 
{
    public GameObject nextButton;
    public GameObject previousButton;
    public GameObject questText;
    public GameObject objective1Text;
    public GameObject objective2Text;
    public GameObject objective3Text;
    public GameObject tooltipText;
    //public GlobalDataScript globalData;
    Quest currentQuest;
    int position;
   
	// Use this for initialization
	void Start ()
    {
        
        this.gameObject.SetActive(false);
        Debug.Log(GlobalDataScript.globalData.questList[0].name);
        //globalData = GlobalDataScript.globalData;
        currentQuest = GlobalDataScript.globalData.questList[0];
        position = 0;
        
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void Open()
    {
        if (this.gameObject.activeInHierarchy)
        {
            Close();
        }
        else
        {
            this.gameObject.SetActive(true);
            //globalData = GlobalDataScript.globalData;
            //currentQuest = globalData.questList[0];
            SetQuestMenu();
        }
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }
    private void SetQuestMenu()
    {
        //Debug.Log(currentQuest.getObjective(1));
        questText.GetComponent<UnityEngine.UI.Text>().text = currentQuest.description;
        objective1Text.transform.FindChild("Label").GetComponent<UnityEngine.UI.Text>().text = currentQuest.objectiveDescriptions[0];
        objective1Text.GetComponent<UnityEngine.UI.Toggle>().isOn = currentQuest.getObjective(1);
        //Debug.Log(objective1Text.GetComponent<UnityEngine.UI.Toggle>().isOn);
        tooltipText.GetComponent<UnityEngine.UI.Text>().text = "Protip: You can fire repeatedly by holding down the mouse button.";

        if (currentQuest.objectiveDescriptions.Length >= 2)
        {
            //Debug.Log(currentQuest.getObjective(2));
            objective2Text.SetActive(true);
            objective2Text.GetComponent<UnityEngine.UI.Toggle>().isOn = currentQuest.getObjective(2);
            objective2Text.transform.FindChild("Label").GetComponent<UnityEngine.UI.Text>().text = currentQuest.objectiveDescriptions[1];
            if (currentQuest.objectiveDescriptions.Length >= 3)
            {
                //Debug.Log(currentQuest.getObjective(3));
                objective3Text.SetActive(true);
                objective3Text.transform.FindChild("Label").GetComponent<UnityEngine.UI.Text>().text = currentQuest.objectiveDescriptions[2];
                objective2Text.GetComponent<UnityEngine.UI.Toggle>().isOn = currentQuest.getObjective(3);
            }
            else
            {
                objective3Text.SetActive(false);
            }
        }
        else
        {
            objective2Text.SetActive(false);
            objective3Text.SetActive(false);
        }
    }

    
    public void NextQuest()
    {
        if(position != 9)
        {
            currentQuest = GlobalDataScript.globalData.questList[position + 1];
            position = position + 1;
        }
        else
        {
            currentQuest = GlobalDataScript.globalData.questList[0];
            position = 0;
        }
        SetQuestMenu();
    }
    public void PreviousQuest()
    {
        if (position != 0)
        {
            currentQuest = GlobalDataScript.globalData.questList[position - 1];
            position = position - 1;
        }
        else
        {
            currentQuest = GlobalDataScript.globalData.questList[9];
            position = 9;
        }
        SetQuestMenu();
    }
}
