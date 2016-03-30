using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PowerupButtonScript : MonoBehaviour ,IPowerUpdate
{
    public int index;
    public GameObject activationText;
    public Sprite destroyImage;
    public Sprite freezeImage;
    public Sprite damageImage;
    public Sprite transparentImage;
	// Use this for initialization
	void Start ()
    {
        this.gameObject.GetComponentInChildren<UnityEngine.UI.Image>().sprite = transparentImage;
        InfoUpdate();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(index == 0)
            {
                this.gameObject.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (index == 1)
            {
                this.gameObject.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (index == 2)
            {
                this.gameObject.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            }
        }
	}

    public void InfoUpdate()
    {
        if (GlobalDataScript.globalData.powerups.Count > index)
        {
            switch (GlobalDataScript.globalData.powerups[index].type)
            {
                case "destroy":
                    this.gameObject.GetComponentInChildren<UnityEngine.UI.Image>().sprite = destroyImage;
                    break;
                case "freeze":
                    this.gameObject.GetComponentInChildren<UnityEngine.UI.Image>().sprite = freezeImage;
                    break;
                case "damage":
                    this.gameObject.GetComponentInChildren<UnityEngine.UI.Image>().sprite = damageImage;
                    break;
                default: 
                    break;
            }
            this.gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = GlobalDataScript.globalData.powerups[index].count.ToString();
        }
        else
        {
            this.gameObject.GetComponentInChildren<UnityEngine.UI.Image>().sprite = transparentImage;
            this.gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "";
        }
    }

    public void CreateActivationText()
    {
        
        //activationText = Instantiate(activationText, new Vector3( this.gameObject.transform.position.x, this.gameObject.transform.position.y +1), Quaternion.identity) as GameObject;
        switch (GlobalDataScript.globalData.powerups[index].type)
        {
            case "destroy":
                activationText.GetComponent<UnityEngine.UI.Text>().text = "Nuke activated!";
                break;
            case "freeze":
                activationText.GetComponent<UnityEngine.UI.Text>().text = "Freezer activated!";
                break;
            case "damage":
                activationText.GetComponent<UnityEngine.UI.Text>().text ="Double Damage Activated!";
                break;
            default:
                break;
        }
        activationText.GetComponent<ConfirmationTextPopup>().Activate();
        Debug.Log("text finished");
    }
}

public interface IPowerUpdate : IEventSystemHandler
{
    // functions that can be called via the messaging system
    void InfoUpdate();
}