using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 1f;
    public float speed = 20f;
    Rigidbody rigid;
    GameObject player;
    public int damage = 30;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody>();
        Invoke("Register", 0 );
    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.LookAt(player.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerManager.takeDamgeByAi(damage);
            Destroy(this.gameObject);
        }
        if(other.tag == "GunBullet")
        {
            Destroy(this.gameObject);
        }
    }

    /*public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f && gameObject != null)
        {
            Destroy(gameObject);
        }
    }*/
    void Register()
    {
        if (!AttackSystem.CheckIfObjectInSight(this.transform))
        {
            AttackSystem.CreateIndicator(this.transform);
        }
    }

}
