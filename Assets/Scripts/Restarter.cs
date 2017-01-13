using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Restarter : MonoBehaviour {
    public GameObject gameOverScreen;

    public void Restart()
    {
        SceneManager.LoadScene("Scene_1");        
        Time.timeScale = 1;
    }
}
