using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatMaster : MonoBehaviour { 
    
    public Text hpText, goldText, levelText, waveText;    
    public Text hpValue, goldValue, levelValue, waveValue;

    public int hp, gold, level, wave;
    public GameObject gameOverScreen;
    public static StatMaster controll;

    void Awake()
    {
        hpText.text = "Health:";
        goldText.text = "Gold:";
        levelText.text = "Level:";
        waveText.text = "Wave:";

        hpValue.text = hp.ToString();
        goldValue.text = gold.ToString();
        levelValue.text = level.ToString();
        waveValue.text = wave.ToString();
        gameOverScreen.SetActive(false);
        if (controll == null) controll = this;
        else Destroy(this);
        Debug.Log("Stats Working");        
    }

    public void LoseHP()
    {
        hp--;
        hpValue.text = hp.ToString();
        if (hp <= 0)
        {
            gameOverScreen.SetActive(true);
            gameOverScreen.GetComponentInChildren<Text>().text = "You've managed to reach wave " + wave;
            Time.timeScale = 0;
        }
    }

    public bool CanAfford(int price)
    {
        if (gold >= price) return true;
        else return false;
    }

    public void AddGold(int ammount)
    {        
            gold += ammount;
            goldValue.text = gold.ToString();      
    }

    public void IncreaseLevel()
    {
        level++;
        levelValue.text = level.ToString();
    }

    public void IncreaseWave()
    {
        wave++;
        waveValue.text = wave.ToString();                    
    }
}
