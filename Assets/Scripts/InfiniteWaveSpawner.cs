using UnityEngine;
using System.Collections;

public class InfiniteWaveSpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public Transform spawnPosition;   
    public float spawnWaitWave = 10.0f;

    private float spawnWaitUnit = 0.3f;
    private float cd;    
    private StatMaster stats;
    private bool lost = false;

    void Start()
    {
        cd = 5f;
        stats = GameObject.Find("Stats").GetComponent<StatMaster>();        
    }

    void Update()
    {
        if (stats.hp <= 0) lost = true;
        if (cd <= 0.0f && !lost)
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
        enemyPrefab.GetComponent<EnemyController>().MultiplyHP(1.05f);
        for (int i = 0; i < stats.wave * 0.8; i++)
        {
            Instantiate(enemyPrefab, spawnPosition.position, spawnPosition.rotation);
            yield return new WaitForSeconds(spawnWaitUnit);            
        }        
    }
}
