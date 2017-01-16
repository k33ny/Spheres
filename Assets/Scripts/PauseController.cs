using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {

    public Transform pauseMenu;
    public Transform pauseButton;

    public Slider sfxSlider;
    public Toggle sfxToggle;

    public Slider musicSlider;
    public Toggle musicToggle;
        
    private bool isPaused = false;

    void Awake()
    {
        pauseMenu.gameObject.SetActive(false);
        sfxSlider.value = GameController.controll.sfxVolume;
        sfxToggle.isOn = GameController.controll.muteSFX;
        musicSlider.value = GameController.controll.musicVolume;
        musicToggle.isOn = GameController.controll.muteMusic;
    }    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameController.controll.inGame)
        {
            if (isPaused) Unpause();
            else Pause();
        }
    }

    public void Pause()
    {
        pauseButton.GetComponent<Button>().interactable = false;
        Fader.controll.FadeOut(0.1f, true);
        pauseMenu.gameObject.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        pauseButton.GetComponent<Button>().interactable = true;
        Fader.controll.FadeIn(0.1f, true);
        pauseMenu.gameObject.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }

    public void AdjustSFX()
    {
        float volume = sfxSlider.value;
        GameController.controll.sfxVolume = volume;
    }

    public void AdjustMusic()
    {
        float volume = musicSlider.value;
        GameController.controll.musicVolume = volume;
        GameController.controll.backgroundMusic.volume = volume;
    }

    public void MuteMusic()
    {
        bool muted = musicToggle.isOn;
        GameController.controll.muteMusic = muted;
        GameController.controll.backgroundMusic.mute = muted;
    }

    public void MuteSFX()
    {
        bool muted = sfxToggle.isOn;
        GameController.controll.muteSFX = muted;
    }

    public void SaveExit()
    {
        StartCoroutine(SaveAndExit());
    }

    IEnumerator SaveAndExit()
    {
        GameController.controll.Save();
        yield return new WaitForSecondsRealtime(0.2f);        
        Application.Quit();        
    }

    public void StartGame(int level = 1)
    {        
        level++;
        GameController.controll.sfxVolume = sfxSlider.value;
        GameController.controll.muteSFX = sfxToggle.isOn;
        GameController.controll.musicVolume = musicSlider.value;
        GameController.controll.muteMusic = musicToggle.isOn;
        GameController.controll.Load(level);
    }

    public void Load()
    {
        GameController.controll.sfxVolume = sfxSlider.value;
        GameController.controll.muteSFX = sfxToggle.isOn;
        GameController.controll.musicVolume = musicSlider.value;
        GameController.controll.muteMusic = musicToggle.isOn;
        GameController.controll.Load();
    }

    public void CallSave()
    {
        GameController.controll.Save();
    }    
}
