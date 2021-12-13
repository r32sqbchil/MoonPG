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

        SetLimitMoveXMin("MovingAreaX1");
        SetLimitMoveXMax("MovingAreaX2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetLimitMoveXMin(string gameObjectName){
        GameObject movingAreaX = GameObject.Find(gameObjectName);
        if(movingAreaX!=null){
            Transform transform = movingAreaX.transform;
            gameManager.limitMoveXMin = transform.position.x + transform.localScale.x;
        } else {
            Debug.LogWarning("not found game object - " + gameObjectName);
        }
    }

    public void SetLimitMoveXMax(string gameObjectName){
        GameObject movingAreaX = GameObject.Find(gameObjectName);
        if(movingAreaX!=null){
            gameManager.limitMoveXMax = movingAreaX.transform.position.x;
        } else {
            Debug.LogWarning("not found game object - " + gameObjectName);
        }
    }
}
