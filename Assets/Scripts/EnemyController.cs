using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {    

    [Header("Attributes")]
    public float speed = 10f;
    public float baseHP = 100f;
    public int bounty = 10;

    private Transform target;
    private int targetIndex = 0;
    private Image hpBar;
    private float maxHP;
    private float maxSpeed;    

    [Header("Do not change")]
    public float hitpoints = 100f;
    public bool invincible = false;
    public GameObject invincibilityLight;

    void Start()
    {
        target = Waypoints.points[targetIndex];
        hpBar = transform.FindChild("EnemyCanvas").FindChild("HealthBar").GetComponent<Image>();        
        maxHP = hitpoints;
        maxSpeed = speed;        
    }

    void Update()
    {        
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextTarget();
        }        
    }

    void GetNextTarget()
    {
        if (targetIndex >= Waypoints.points.Length)
        {
            Destroy(gameObject);
            StatMaster.controll.LoseHP();
            return;

        }
        
        target = Waypoints.points[targetIndex];        
        targetIndex++;
    }

    public void MultiplyHP(float multiplier)
    {
        maxHP *= multiplier;
        hitpoints *= multiplier;
    }

    public void UpdateHealthBar()
    {
        hpBar.fillAmount = hitpoints / maxHP;
    }

    public void SlowDown(float scale, float duration)
    {        
        StartCoroutine(StaySlowed(scale, duration));
    }

    public void MakeInvincible(float duration)
    {
        StartCoroutine(StayInvincible(duration));
    }

    public void RegenHP(float totalScale, float duration)
    {
        float tickCount = duration / 0.5f;
        float tickScale = (maxHP - hitpoints) / maxHP * totalScale / tickCount;
        StartCoroutine(Regenerate(tickScale, tickCount));
    }

    private IEnumerator Regenerate(float scale, float cycleCount)
    {
        while (cycleCount > 0)
        {
            hitpoints += maxHP * scale;
            if (hitpoints > maxHP) hitpoints = maxHP;
            UpdateHealthBar();
            cycleCount--;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator StayInvincible(float duration)
    {
        invincible = true;
        GameObject halo = (GameObject)Instantiate(invincibilityLight, transform, false);        
        yield return new WaitForSeconds(duration - 0.5f);        
        halo.GetComponent<Animation>().Play("Shrink");
        Destroy(halo, 1f);
        yield return new WaitForSeconds(0.5f);
        invincible = false;
    }

    private IEnumerator StaySlowed(float scale, float duration)
    {
        speed = maxSpeed * scale;        
        yield return new WaitForSeconds(duration);
        speed = maxSpeed;
    }
}
