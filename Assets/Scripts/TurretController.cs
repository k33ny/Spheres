using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurretController : MonoBehaviour {
    [Header("Attributes")]
    public float range;    
    public float fireRate;
    public float damage;
    public bool useLaser = false;

    [Header("Shop Info")]
    public string type;
    public int price;
    public Sprite sprite;
    public int index;
    public GameObject[] upgrades;

    [Header("Developer's Properties")]
    public Vector3 offset = new Vector3(0, 0.5f, 0);
    public Transform rotator;    
    public Transform bulletSpawn;
    public GameObject bulletPrefab;

    public float turnSpeed = 10;
    private AudioSource shotAudio;
    private GameObject target;
    private float fireCountd;
    private LineRenderer laser = null;
    private GameObject laserParticles = null;

    void Start()
    {        
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
        if (GetComponent<AudioSource>()) shotAudio = GetComponent<AudioSource>();
        if (useLaser) laser = bulletSpawn.GetComponent<LineRenderer>();
    }

    void Update()
    {        
        if (target == null)
        {
            if (laser)
            {
                laser.enabled = false;
                Destroy(laserParticles, 0.5f);
            }
            return;
        }
        Vector3 targetDirection = target.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Vector3 rotation = Quaternion.Lerp(rotator.rotation, targetRotation, Time.deltaTime * turnSpeed).eulerAngles;
        rotator.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);

        if (useLaser)
        {
            laser.enabled = true;
            laser.SetPosition(0, bulletSpawn.position);
            laser.SetPosition(1, target.transform.position);
            target.GetComponent<EnemyController>().SlowDown(damage, 1f);

            if (laserParticles != null)
            {
                laserParticles.transform.position = target.transform.position;
            }
            else laserParticles = (GameObject)Instantiate(bulletPrefab, target.transform.position, bulletSpawn.rotation);            
        }
        else if (fireCountd <= 0f)
        {
            Shoot();
            fireCountd = 1f / fireRate;
        }
        fireCountd -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject currentBullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletPrefab.transform.rotation);
        BulletController bulletControll = currentBullet.GetComponent<BulletController>();
        if (bulletControll != null) bulletControll.SetTarget(target, damage);
        if (shotAudio) shotAudio.Play();
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float minDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (enemyDistance < minDistance)
            {
                minDistance = enemyDistance;
                closestEnemy = enemy;
            }
            else target = null;
        }
        if (closestEnemy != null && minDistance <= range)
        {
            target = closestEnemy;
            if (laserParticles) laserParticles.transform.position = target.transform.position;
        }

        if (shotAudio != null)
        {
            if (GameController.controll.muteSFX) shotAudio.volume = 0;
            else shotAudio.volume = GameController.controll.sfxVolume;
        }    
    }

    void OnDrawGizmosSelected()
    {//Shows turret range for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public bool HasUpgrade()
    {//Returns true if the turret has any possible upgrades
        if (upgrades != null) return true;
        else return false;
    }

    void OnDestroy()
    {
        if (laserParticles) Destroy(laserParticles);
    }
}
