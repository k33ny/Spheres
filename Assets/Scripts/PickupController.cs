using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public float lifetime = 14f;

    [Header("Pickup Properties")]
    public float hpProc = 0;
    public float speedProc = 0;
    public bool invincibility = false;
    public float duration = 2f;
    
    void Awake()
    {
        transform.localScale = new Vector3(0, 0, 0);
        transform.FindChild("Point light").GetComponent<Light>().intensity = 0;
    }

    void Update()
    {
        Rotate();
        CheckTime();
    } 
    
    void Rotate()
    {
        float rate = rotationSpeed * Time.deltaTime;
        Vector3 rotation = new Vector3(rate, rate, rate);
        transform.Rotate(rotation);
    }  
    
    void CheckTime()
    {
        if (lifetime <= 0)
        {
            PickupSpawner.spawnedPickups--;
            transform.GetComponent<Animation>().Play("Shrink");
            Destroy(gameObject, 1f);
        }
        else lifetime -= Time.deltaTime;
    } 

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (invincibility) other.GetComponent<EnemyController>().MakeInvincible(duration);
            if (speedProc > 0) other.GetComponent<EnemyController>().SlowDown(1+(speedProc/100), duration);
            if (hpProc > 0) other.GetComponent<EnemyController>().RegenHP(hpProc / 100, duration);

            PickupSpawner.spawnedPickups--;
            Destroy(gameObject);
        }
    }
}
