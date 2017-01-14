using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
    public GameObject explosionPrefab;
    public float speed = 70f;
    public float explosionRadius = 0f;    

    private GameObject target;
    private float damage;   

    public void SetTarget (GameObject _target, float _damage)
    {
        target = _target;
        damage = _damage;        
    }    

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 direction = target.transform.position - transform.position;      
        
        if (direction.magnitude <= 0.5)
        {            
            if (explosionRadius == 0) Hit();
            else Explode();          
        }
        else
        {
            float distThisFrame = speed * Time.deltaTime;
            transform.Translate(direction.normalized * distThisFrame, Space.World);
            Transform rotator = transform;
            rotator.LookAt(target.transform.position);
            transform.up = rotator.forward;
        }
    } 
    
    private void Hit()
    {
        
        GameObject explosion = (GameObject)Instantiate(explosionPrefab, transform.position, transform.rotation);        
        Destroy(explosion, 1.0f);
        Destroy(gameObject);
        if (target.GetComponent<EnemyController>().invincible) return;
        if (target.GetComponent<EnemyController>().hitpoints > damage)
        {
            target.GetComponent<EnemyController>().hitpoints -= damage;
            target.GetComponent<EnemyController>().UpdateHealthBar();
        }
        else
        {
            Destroy(target);
            int bounty = target.GetComponent<EnemyController>().bounty;
            GameObject.Find("Stats").GetComponent<StatMaster>().AddGold(bounty);
        }        
    }

    private void Explode()
    {
        GameObject explosion = (GameObject)Instantiate(explosionPrefab, transform.position, transform.rotation);
        if (GameController.controll.muteSFX) explosion.GetComponent<AudioSource>().volume = 0;
        else explosion.GetComponent<AudioSource>().volume = GameController.controll.sfxVolume;        
        Destroy(explosion, 1.0f);
        Destroy(gameObject);
        Collider[] detection = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider unit in detection)
        {
            if (unit.tag == "Enemy")
            {
                if (unit.GetComponent<EnemyController>().invincible) return;
                if (unit.GetComponent<EnemyController>().hitpoints > damage)
                {
                    unit.GetComponent<EnemyController>().hitpoints -= damage;
                    unit.GetComponent<EnemyController>().UpdateHealthBar();
                }
                else
                {
                    Destroy(unit.gameObject);
                    int bounty = unit.GetComponent<EnemyController>().bounty;
                    GameObject.Find("Stats").GetComponent<StatMaster>().AddGold(bounty);
                }
            }
        }        
    }      
}
