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
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        gameManager.Action(playerGameObject);
    }
}
