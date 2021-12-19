using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MountainStage6Setting : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        gameManager.SetLimitMoveXMax("MovingAreaX2");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void HideWall()
    {
        gameManager.SetLimitMoveXMax("MovingAreaX9");
    }

}
