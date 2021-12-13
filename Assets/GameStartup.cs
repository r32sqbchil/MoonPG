using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartup : MonoBehaviour
{
    void Start()
    {
        Invoke("BeginThisGame", 1.0f);
    }

    void BeginThisGame() {
        GameManager gameManager = GetComponent<GameManager>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        gameManager.Action(player);
    }
}
