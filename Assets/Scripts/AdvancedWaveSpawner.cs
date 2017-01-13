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

    void SpawnWave(Wave wave)
    {
        StartCoroutine(wave.Spawn(spawnPoint.position));
    }

    private IEnumerator WaveSpawner()
    {
        while (currentWaveIndex < waves.Length)
        {
            stats.IncreaseWave();
            SpawnWave(waves[currentWaveIndex]);
            float waitTimer = waves[currentWaveIndex].enemies.Length * waves[currentWaveIndex].spawnRate;
            currentWaveIndex++;            
            yield return new WaitForSeconds(waitTimer + waitTime);
        }
        Debug.Log("Level Complete");
        stats.IncreaseLevel();
        yield return 0; //Add level transition here
    }

    void Start()
    {
        stats = GameObject.Find("Stats").GetComponent<StatMaster>();
        StartCoroutine(WaveSpawner());
    }
}
