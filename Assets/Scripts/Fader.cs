using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour {
    public GameObject fadeScreenPrefab;
    public float fadeSpeed = 0.8f;
    public float speedRate = 1f;
    public bool stable = true;

    public static Fader controll;
    private float targetAlph = 0;
    private float currentAlph = 0;
    private float dir = 1;
    private Image image;

    void Start()
    {
        if (controll == null)
        {
            controll = this;
            fadeScreenPrefab = (GameObject)Instantiate(fadeScreenPrefab, transform);
            image = fadeScreenPrefab.GetComponentInChildren<Image>();
            FadeIn(0.1f);
        }
        else Destroy(controll);        
    }

    void Update()
    {
        if (currentAlph != targetAlph)
        {
            fadeSpeed *= speedRate;
            currentAlph += Time.unscaledDeltaTime * fadeSpeed * dir;
            currentAlph = Mathf.Clamp01(currentAlph);
            image.color = new Color(0, 0, 0, currentAlph);
        }
        else
        {
            stable = true;
            if (currentAlph == 0) fadeScreenPrefab.SetActive(false);            
        }

    }

    public float FadeIn(float speed = 0)
    {
        stable = false;
        controll.image.color = new Color(0, 0, 0, 1);
        controll.fadeScreenPrefab.SetActive(!stable);
        if (speed > 0) controll.fadeSpeed = speed;        
        controll.dir = -1;
        controll.targetAlph = 0;
        controll.currentAlph = 1;
        return fadeSpeed;
    }

    public float FadeOut(float speed = 0)
    {
        stable = false;
        controll.image.color = new Color(0, 0, 0, 0);
        controll.fadeScreenPrefab.SetActive(!stable);
        if (speed > 0) controll.fadeSpeed = speed;        
        controll.dir = 1;
        controll.targetAlph = 1;
        controll.currentAlph = 0;
        return fadeSpeed;
    }

}
