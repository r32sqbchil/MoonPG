using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneForSpawnMonster : MonoBehaviour
{
    //when
    [SerializeField] private float waitingTimeToStart = 3.0f; //secs
    [SerializeField] private float waitingTimeToNext = 5.0f; //secs

    //where
    [SerializeField] private GameObject spawingPoint;

    //what
    [SerializeField] private GameObject[] monsters;

    //how
    [SerializeField] private bool randomMode;
    
    [SerializeField] private string spawnTag = "default";
    //how many
    [SerializeField] private int spawningCount = 1; // 동시에 스폰되는 몬스터 갯수
    [SerializeField] private int spawningMax = 1;
    [SerializeField] private int bornMax = 0;

    private Vector3[] spawnPoints;

    private int spwanPointNextIndex;

    private int bornCount;

    private GameManager gameManager;
    private Dictionary<string, GameObject> movableAreas = new Dictionary<string, GameObject>();

    private void InitializeSpwanPoints()
    {
        if(spawingPoint){
            List<Vector3> points = new List<Vector3>();
            foreach(Transform transform in spawingPoint.GetComponentsInChildren<Transform>()){
                if(transform.parent != null){
                    points.Add(transform.position);
                }
            }
            spawnPoints = new Vector3[points.Count];
            points.CopyTo(spawnPoints);
            spwanPointNextIndex = 0;
        }
        if(monsters == null || monsters.Length == 0) {
            this.bornMax = 0; // 몬스터 생성 차단
        } else {
            List<GameObject> checkedObjects = new List<GameObject>();
            foreach(GameObject monster in monsters){
                if(monster != null){
                    checkedObjects.Add(monster);
                }
            }
            if(checkedObjects.Count == 0){
                this.bornMax = 0; // 몬스터 생성 차단
            } else {
                monsters = new GameObject[checkedObjects.Count];
                checkedObjects.CopyTo(monsters);
            }
        }
    }

    private GameObject NextSpawnMonster(){
        return monsters[Random.Range(0, monsters.Length)];
    }

    private Vector3 NextSpawnPoint(){
        Vector3 v3;
        if(randomMode){
            int spwanPointNextIndex = Random.Range(0, spawnPoints.Length);
            v3 = spawnPoints[spwanPointNextIndex];
        } else {
            v3 = spawnPoints[spwanPointNextIndex];
            spwanPointNextIndex = (spwanPointNextIndex+1)%spawnPoints.Length;
        }
        v3.x += Random.Range(-0.2f, 0.2f);
        return v3;
    }

    int GetMonsterCountBySpawnTag(){
        int monsterCount = 0;
        foreach(EnemyBase eb in GameObject.FindObjectsOfType<EnemyBase>()){
            if(eb.spawnTag == spawnTag) monsterCount++;
        }
        return monsterCount;
    }

    GameObject GetMovableArea(){
        if(!movableAreas.ContainsKey(spawnTag)){
            string objectName = "EnemyMovable_" + spawnTag;
            GameObject movableArea = GameObject.Find(objectName);
            if(movableArea != null) {
                movableAreas.Add(spawnTag, movableArea);
            } else {
                //Debug.Log("Can't find " + " object");
                return null;
            }
        }
        return movableAreas[spawnTag];
    }

    IEnumerator CreateMonster()
    {
        // 0 보다는 크고 200 보다 작은 bornMax
        int bornMax = Mathf.Min(200, Mathf.Max(this.bornMax, 0));
        // 0 보다는 크고 bornMax 보다 작은 spawningMax
        int spawningMax = Mathf.Min(bornMax, Mathf.Max(this.spawningMax, 0));
        // 1 보다는 크고 spawningMax 보다 작은 spawningCount
        int spawningCount = Mathf.Min(spawningMax, Mathf.Max(this.spawningCount, 1));

        Debug.Log("bornMax: " + bornMax + ", spawningMax: " + spawningMax+ ", spawningCount: " + spawningCount);

        if(waitingTimeToStart > 0){
            //Debug.Log("waitingTimeToStart: " + waitingTimeToStart);
            yield return new WaitForSeconds(waitingTimeToStart);
        }
        
        bool reachAtBornMax = (bornMax == 0);
        int monsterTotalCount = 0;
        while(!gameManager.isGameOver && !reachAtBornMax)
        {
            int monsterCountInScene = GetMonsterCountBySpawnTag();
            //Debug.Log("monsterCount: " + monsterCountInScene);
            if(monsterCountInScene < spawningMax){
                int spwanable = Mathf.Min(spawningMax-monsterCountInScene, spawningCount);
                for(int i=0;i<spwanable;i++) {
                    //Debug.Log("Instantiate Monster - " + (monsterTotalCount+1));

                    GameObject monster = Instantiate(NextSpawnMonster());
                    monster.transform.position = NextSpawnPoint();
                    EnemyBase enemyBase = monster.GetComponent<EnemyBase>();
                    enemyBase.spawnTag = spawnTag;
                    enemyBase.movableArea = GetMovableArea();
                    if(++monsterTotalCount >= bornMax) {
                        reachAtBornMax = true;
                    }
                }
            } else {
                //Debug.Log("spawn max - dont't spwan");
            }
            if(waitingTimeToNext > 0){
                //Debug.Log("waitingTimeToNext: " + waitingTimeToNext);
                yield return new WaitForSeconds(waitingTimeToNext);
            }
        }
    }

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        if(monsters == null || monsters.Length == 0){
            Debug.LogWarning("Check monsters property");
        }

        if(spawingPoint == null) {
            spawingPoint = GameObject.Find("SpawnPoint");
        }

        InitializeSpwanPoints();

        Debug.Log("spawnPoints: " + spawnPoints);
        if(spawnPoints != null && spawnPoints.Length > 0){
            StartCoroutine(CreateMonster());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
