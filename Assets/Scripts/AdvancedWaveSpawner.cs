using UnityEngine;
using System.Collections;

public class AdvancedWaveSpawner : MonoBehaviour {

    [System.Serializable]
	public class Wave
    {
        public string name;
        public float spawnRate = 0.3f;        
        public GameObject[] enemies;               
                
        public IEnumerator Spawn(Vector3 position)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                Instantiate(enemies[i], position, Quaternion.identity);
                yield return new WaitForSeconds(spawnRate);
            }
        }
    }

    public GameObject winScreen;
    public Transform spawnPoint;
    public float waitTime = 20f;
    public Wave[] waves;

    private int currentWaveIndex = 0;
    private StatMaster stats;
    private float cd;
    private int enemyCount;
    private bool isLast = false;
    private bool isEnding = false;
    
    float SpawnWave()
    {
        if (isLast) return 0;
        stats.IncreaseWave();
        Wave wave = waves[currentWaveIndex];
        StartCoroutine(wave.Spawn(spawnPoint.position));        
        float waitTimer = waves[currentWaveIndex].enemies.Length * waves[currentWaveIndex].spawnRate;
        currentWaveIndex++;
        if (currentWaveIndex >= waves.Length) isLast = true;
        return waitTimer + waitTime;            
    }
        

    void Start()
    {
        stats = GameObject.Find("Stats").GetComponent<StatMaster>();
        cd = 10f;
        winScreen.SetActive(false);    
    }

    void Update()
    {
        //if (enemyCount == 0 && isLast) ;
        if (!isLast)
        {
            cd -= Time.deltaTime;
            if (cd < 0.5f && currentWaveIndex < waves.Length) cd = SpawnWave();
            MenuController.controll.SetWaveCountdown((int)Mathf.Ceil(cd));
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemyCount == 0 && currentWaveIndex > 0 && cd > 5) cd = 5f;
        }
        else
        {
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            MenuController.controll.waveCountdown.text = "Final Wave";
            if (enemyCount == 0 && !isEnding)
            {
                isEnding = true;
                GameController.controll.inGame = false;
                StartCoroutine(MenuController.CompleteLevel(winScreen));
            }
        }
    }
}
