using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InGameMenuHandler : MonoBehaviour 
{
	//bug workaround
	public Button selectable;

    private bool isShowing = false;

    public void BackToMainMenuBtnPressed ()
    {
        Game.Instance.GameController.BackToMenu();
    }

    public void ResumeGamePressed () 
    {
        ToggleState();
    }

    public void ToggleState ()
    {
        isShowing = !isShowing;
        gameObject.SetActive(isShowing);
		if (isShowing) {
			selectable.Select ();
		}
        Time.timeScale = (isShowing) ? 0.0f : 1.0f;
    }
}
