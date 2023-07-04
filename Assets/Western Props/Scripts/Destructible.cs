using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

	public GameObject destroyedVersion;
    public float health = 10f;
	public float timeCanDestroy;

	void Update()
	{
		timeCanDestroy -= Time.deltaTime;

	}
	public void takeDamgeBox(float dmg)
	{
		health -= dmg;
        if (health <= 0 && timeCanDestroy <= 0)
        {
		Instantiate(destroyedVersion, transform.position, transform.rotation);
		Destroy(gameObject);

        }
	}

}
