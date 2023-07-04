using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSpawn : MonoBehaviour
{
    public GameObject enemy;
    public float timeLeft, origin;
    public float enemycount = 30;
    public Text EnemyLeft;
   void Update()
   {
        timeLeft -= Time.deltaTime;
        if (enemycount > 0)
        {
            if (timeLeft <= 0)
            {
                Vector3 randomSpawnPos = new Vector3(Random.Range(-120, 120), 6, Random.Range(-120, 120));
                Instantiate(enemy, randomSpawnPos, Quaternion.identity);
                timeLeft = origin;
                enemycount -= 1;
            }
        }
        EnemyLeft.text = "Enemy Left: " + enemycount;
   }


}
