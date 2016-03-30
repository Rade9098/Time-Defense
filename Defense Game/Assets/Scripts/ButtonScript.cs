using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {
    public BoostMenuScript menu;
	public void NextLevelButton(string name)
    {
        menu.Activate(name);
    }
}
