using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {
    public GameObject[] mode; //Default:0 NodeEmpty:1 NodeFilled:2
    public Text waveCountdown;
    public static MenuController controll;

    private int currentMode = 0;

    
    void Awake()
    {     
        waveCountdown.text = "";
        controll = this;
        transform.GetChild(0).gameObject.SetActive(true);
        for (int i = 0; i < mode.Length; i++) mode[i].SetActive(false);
    }

    public void ChangeMenu(int _mode)
    {
        transform.GetChild(0).gameObject.SetActive(false);
        mode[currentMode].SetActive(false);
        mode[_mode].SetActive(true);
        currentMode = _mode;
    }

    public void SetWaveCountdown(int time)
    {
        waveCountdown.text = "Next wave in: " + time;
    }

    public static IEnumerator CompleteLevel(GameObject endScreen)
    {
        GameController.controll.hp = StatMaster.controll.hp;
        GameController.controll.diffMultiplier *= 1.2f;    
        Fader.controll.FadeOut(0.5f);
        while (!Fader.controll.stable) yield return null;
        endScreen.SetActive(true);
        Time.timeScale = 0;
        Fader.controll.FadeIn(0.5f);
        while (!Fader.controll.stable) yield return null;
        Debug.Log("Transition Complete");
    }    

    public void Test(GameObject s)
    {
        StartCoroutine(CompleteLevel(s));
    }
}
