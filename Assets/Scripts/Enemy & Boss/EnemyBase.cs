using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//구조체 Enemystat을 만듭니다. 안에는 Health나 Speed등을 넣습니다. 구조체를 inspector창에 보일수 있게 Attribute[System.Serializable]을 써줍니다.
[System.Serializable]
struct EnemyStat{
    public float health;
}

public class EnemyBase : MonoBehaviour
{
    [SerializeField]private EnemyStat enemyStat;

    private GameManager gameManager;
    private CameraShake cameraShake;

    private bool bTakenEffect = false;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        cameraShake = GameObject.FindObjectOfType<CameraShake>();
    }

    void Update()
    {
        
    }

    public bool isAlive()
    {
        return enemyStat.health > 0;
    }

    //데미지를 받는 함수 입니다. 인자에는 damage값을 설정해줍니다.
    public void TakeDamage(GameObject playerObject, float direction, float damage)
    {
        //체력이 damage만큼 까지게 합니다.
        enemyStat.health -= damage;

        Debug.Log("Enemy-HP: "+ enemyStat.health);

        if(isAlive())
        {
            StartCoroutine(cameraShake.ShakeHorizontalOnly(.1f, .1f));
        }
        else if(!bTakenEffect) 
        {
            bTakenEffect = true;
            gameManager.Action(gameObject, GameManager.ACTION_ON_DIED);
            Destroy(gameObject, 2f);
        }
    }
}
