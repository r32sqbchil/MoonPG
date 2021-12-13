using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownStage1Setting : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        gameManager.SetLimitMoveXMax("MovingAreaX2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
