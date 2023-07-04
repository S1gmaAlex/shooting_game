using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAi : MonoBehaviour
{ 
    public float health = 1f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f && gameObject != null)
        {
            Destroy(gameObject);
        }
    }
    

}


