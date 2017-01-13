using UnityEngine;
using System.Collections;

public class PickupSpawner : MonoBehaviour {

    public GameObject[] pickups;
    public float spawnInterval = 60f;
    public int maxPickups = 3;
    public static int spawnedPickups = 0;

    private Transform[] pathNodes;
    private Vector3 offset;
    
	
    void Start()
    {
        int nodeCount = transform.FindChild("Path").childCount;        
        pathNodes = new Transform[nodeCount];
        for (int i = 0; i < nodeCount; i++)
        {
            pathNodes[i] = transform.FindChild("Path").GetChild(i);
        }
        offset = new Vector3(0, 2f, 0);
        StartCoroutine(SpawnPickups(spawnInterval));
    }

    void Update()
    {
                
    }

    IEnumerator SpawnPickups(float interval)
    {
        while (true)
        {
            InstantiatePickup();
            yield return new WaitForSeconds(interval);
        }
    }

    public void InstantiatePickup()
    {
        if (spawnedPickups >= maxPickups) return;        
        int randomPack = Random.Range(0, pickups.Length);
        int randomNode = Random.Range(0, pathNodes.Length - 1);
        Vector3 spawnPos = pathNodes[randomNode].position + offset;
        Instantiate(pickups[randomPack], spawnPos, pickups[randomPack].transform.rotation);
        spawnedPickups++;
    }
}
