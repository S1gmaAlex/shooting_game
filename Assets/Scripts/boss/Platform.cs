using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject boss;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == boss)
        {
            boss.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == boss)
        {
            boss.transform.parent = null;
        }
    }
}
