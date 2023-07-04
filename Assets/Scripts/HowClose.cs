using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowClose : MonoBehaviour
{
    public GameObject Boss;
    public GameObject Player;
    public static float Distance;

    void Update()
    {
        Distance = Vector3.Distance(Boss.transform.position, Player.transform.position);
    }
}
