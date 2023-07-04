using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject Instruction;
    public GameObject BossHealth;
    private Animator anim;
    private bool IsOpen = false;
    private bool IsClose = true;
    private playerController keys;

    void Start()
    {
        Instruction.SetActive(false);
        BossHealth.SetActive(false);
        keys = FindObjectOfType<playerController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
        if (IsOpen == true && (Input.GetKey(KeyCode.E)) && keys.KeyAmount >= 1)
        {
            BossHealth.SetActive(true);
            Instruction.SetActive(false);
            //keys.KeyAmount -= 1;
            anim.SetTrigger("OpenDoor");
            IsOpen = false;
            IsClose = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        BossHealth.SetActive(false);
        if (other.tag == "Player")
        {
            Instruction.SetActive(true);
            IsOpen = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        
        Instruction.SetActive(false);
        if (IsClose == false)
        {
            BossHealth.SetActive(true);
            anim.SetTrigger("CloseDoor");
        }
    }
}
