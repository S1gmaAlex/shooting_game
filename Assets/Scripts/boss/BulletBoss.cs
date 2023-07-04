using UnityEngine;

public class BulletBoss : MonoBehaviour
{
    
    public GameObject bulletEffect;
    //public int damage = 25;
    //public int maxdamage = 100;

    /*private void OnCollisionEnter(Collision collision)
    {
        GameObject effect = Instantiate(bulletEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        if (collision.gameObject.tag == "Player")
        {
            if (HowClose.Distance < 200)
            {
                
            }
        
            if (HowClose.Distance > 200)
            {
                PlayerManager.takeDamageByBoss(50);
            }
        }
        Destroy(gameObject);
    }*/
    private void OnTriggerEnter(Collider other)
    {
        GameObject effect = Instantiate(bulletEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        if (other.tag == "Player")
        {
            if (HowClose.Distance < 200)
            {
                PlayerManager.PlayerHP -= 100;
            }

            if (HowClose.Distance > 200)
            {
                PlayerManager.PlayerHP -= 50;
            }
        }
        Destroy(gameObject);
    }
}
