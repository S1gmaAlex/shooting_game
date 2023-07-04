using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static int PlayerHP = 500;
    public TextMeshProUGUI playerHPText;

    public static bool isGameOver;
    void Start()
    {
        PlayerHP = 500;
        isGameOver = false;
    }

    void Update()
    {   
        if(PlayerHP <= 0)
        {
            isGameOver = true;
        }
        playerHPText.text = "+" + PlayerHP;
        if (isGameOver)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    public static void takeDamgeByAi(int damageAmount)
    {
        PlayerHP -= damageAmount;
        if (PlayerHP <= 0)
            isGameOver = true;
    }

    
}
