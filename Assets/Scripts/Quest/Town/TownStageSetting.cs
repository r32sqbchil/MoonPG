using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownStageSetting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("BeginThisGame", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BeginThisGame() {
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        Player player = GameObject.FindObjectOfType<Player>();
        gameManager.Action(player.gameObject);
    }
}
