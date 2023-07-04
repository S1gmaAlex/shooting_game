using UnityEngine;

public class GunBullet : MonoBehaviour
{
    public GameObject gunBulletEffect;
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {

        GameObject effect = Instantiate(gunBulletEffect, transform.position, transform.rotation);
        Destroy(effect, 0.5f);
        if (other.tag == "Boss")
        {
            other.GetComponent<Boss>().TakeDamgeByPlayer(damage);
        }
        if (other.tag == "Box")
        {
            other.GetComponent<Destructible>().takeDamgeBox(damage);
        }

        Destroy(gameObject);
    }
}
