using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestFieldStage2Setting : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        gameManager.SetLimitMoveXMin("MovingAreaX2");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void HideWall()
    {
        gameManager.SetLimitMoveXMin("MovingAreaX1");
    }

}
