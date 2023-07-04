using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Transform Target;
    public Transform player;
    float dist;
    public float howClose;
    public Transform head, firePoint;
    public GameObject bullet;
    public float fireRate, nextFire;

    [Range(0,360)] public float angle;
    public LayerMask targetMask;
    public LayerMask obstruction;
    public bool canSeePlayer;

    public float bossHP, maxBossHP;
    public GameObject healthBarUi;
    public Slider slider;

    void Start()
    {
        bossHP = maxBossHP;
        slider.value = CalculateHealth();
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(FOVRoutine());
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        slider.value = CalculateHealth();
        if (dist <= howClose && canSeePlayer == true)
        {
            head.LookAt(Target);
            if (nextFire <= 0f)
            {
            nextFire = fireRate;
            startShoot();
            }
            nextFire -= Time.deltaTime;
        }
    }

    private IEnumerator FOVRoutine ()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, howClose, targetMask);
        if (rangeCheck.Length != 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstruction))
                {
                    canSeePlayer = true;
                    transform.LookAt(player);
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
            canSeePlayer = false;
        
    }

    void startShoot()
    {
        Debug.Log("boss shooted");
        GameObject clone = Instantiate(bullet, firePoint.position, head.rotation);
        clone.GetComponent<Rigidbody>().AddForce(head.forward * 40000f);      
    }

    public void TakeDamgeByPlayer(float damageAmount)
    {
        bossHP -= damageAmount;
        if(bossHP < maxBossHP)
        {
            healthBarUi.SetActive(true);
        }

        if(bossHP <= 0)
        {
            Destroy(gameObject);
        }
         
        if (bossHP > maxBossHP)
        {
            bossHP = maxBossHP;
        }
    }
    float CalculateHealth()
    {
        return bossHP / maxBossHP;
    }
}
