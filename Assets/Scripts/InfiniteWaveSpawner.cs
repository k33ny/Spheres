using UnityEngine;
using System.Collections;

public class InfiniteWaveSpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public Transform spawnPosition;

    private float spawnWaitWave = 5.0f;
    private float spawnWaitUnit = 0.5f;

    private float cd;    
    private StatMaster stats;

    void Start()
    {
        cd = spawnWaitWave;
        stats = GameObject.Find("Stats").GetComponent<StatMaster>();
        enemyPrefab.GetComponent<EnemyController>().hitpoints = enemyPrefab.GetComponent<EnemyController>().baseHP;
    }

    void Update()
    {
        if (cd <= 0.0f)
        {
            StartCoroutine(SpawnWave());
            cd = spawnWaitWave;
        }
        MenuController.controll.SetWaveCountdown((int)Mathf.Ceil(cd));
        cd -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {        
        stats.IncreaseWave();
        enemyPrefab.GetComponent<EnemyController>().MultiplyHP(1.2f);
        for (int i = 0; i < stats.wave * 0.8; i++)
        {
            Instantiate(enemyPrefab, spawnPosition.position, spawnPosition.rotation);
            yield return new WaitForSeconds(spawnWaitUnit);            
        }        
    }
}
