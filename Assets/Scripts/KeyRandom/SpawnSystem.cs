using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject key;
    void Start()
    {
        spawnObjects();
    }

    void spawnObjects()
    {
        List<GameObject> tempSpawnPoints = new List<GameObject>();
        tempSpawnPoints.AddRange(spawnPoints);
        tempSpawnPoints.Shuffle();

        for (int i = 0; i < 1; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(tempSpawnPoints[i].transform.position, -Vector3.up, out hit))
            {
                Vector3 location = new Vector3(hit.point.x, hit.point.y + 3f, hit.point.z);
                key.transform.position = location;
                
            }
        }
    }


}
