using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Restarter : MonoBehaviour {    

    public void Restart()
    {
        GameController.controll.level = 1;   
        StartCoroutine(ReloadMainMenu());        
    }

    IEnumerator ReloadMainMenu()
    {
        GameController.controll.inGame = false;
        if (Fader.controll.currentAlph != 1)
            Fader.controll.FadeOut(0.1f, true);
        while (!Fader.controll.stable) yield return null;       
        SceneManager.LoadScene(0, LoadSceneMode.Single);        
        Fader.controll.FadeIn(0.1f, true);
        Time.timeScale = 1;
    }      

    public void Quit()
    {
        StartCoroutine(QuitGame());
    } 
    
    IEnumerator QuitGame()
    {
        Fader.controll.FadeOut(0.2f);
        float vol = GameController.controll.GetComponent<AudioSource>().volume;        
        while (!Fader.controll.stable)
        {
            float delta = vol * Mathf.Abs(Fader.controll.currentAlph - 1);            
            GameController.controll.GetComponent<AudioSource>().volume = delta;
            yield return null;
        }
        Application.Quit();
    } 
    
    public void GoToLevel(int level)
    {
        GameController.controll.inGame = false;
        GameController.controll.level = level;
        level++;            
        GameController.controll.Load(level);
    }   
}
