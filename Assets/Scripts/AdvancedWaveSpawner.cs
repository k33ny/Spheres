using UnityEngine;
using System.Collections;

public class AdvancedWaveSpawner : MonoBehaviour {

    [System.Serializable]
	public class Wave
    {
        public string name;
        public float spawnRate;        
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

    public Transform spawnPoint;
    public float waitTime = 5f;
    public Wave[] waves;

    private int currentWaveIndex = 0;
    private StatMaster stats;
    private float cd;
    
    float SpawnWave()
    {
        stats.IncreaseWave();
        Wave wave = waves[currentWaveIndex];
        StartCoroutine(wave.Spawn(spawnPoint.position));        
        float waitTimer = waves[currentWaveIndex].enemies.Length * waves[currentWaveIndex].spawnRate;
        currentWaveIndex++;
        return waitTimer + waitTime;            
    }
        

    void Start()
    {
        stats = GameObject.Find("Stats").GetComponent<StatMaster>();
        cd = waitTime;      
    }

    void Update()
    {
        cd -= Time.deltaTime;
        if (cd < 0.5f) cd = SpawnWave();
        MenuController.controll.SetWaveCountdown((int)Mathf.Ceil(cd));        
    }
}
