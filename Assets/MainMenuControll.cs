using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuControll : MonoBehaviour {    
    public PauseController pauser;
    
    public void StartWithDifficulty(float diff)
    {
        GameController.controll.diffMultiplier = diff;
        pauser.StartGame();
    }

    public void EnableMenu(GameObject menu)
    {        
        menu.SetActive(true);
    }

    public void DisableMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

    public void Quit()
    {
        Fader.controll.FadeOut();
        Invoke("Application.Quit()", 1f);
    }
}
