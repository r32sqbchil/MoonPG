using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const string ACTION_ON_TOUCH = "OnTouch";
    public const string ACTION_ON_DIED = "OnDied";
    public const string ACTION_ON_TOUCH_LEFT_BOUNDARY = "OnTouchLeftBoundary";
    public const string ACTION_ON_TOUCH_RIGHT_BOUNDARY = "OnTouchRightBoundary";

    public Transform[] points;
    public GameObject[] monsterPrefab;

    public float createTime;
    public int maxMonster = 1;

    public GameObject subSettings;
    public GameObject Inspector;
    
    public GameObject talkUI;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;
    
    public int talkIndex;

    public string scanObjectName; 
    public Text UINameText;

    public bool isGameOver = false;

    [HideInInspector] public float limitMoveXMin = -3.07f;
    [HideInInspector] public float limitMoveXMax = 17.0f;

    private string sceneName;
    private QuestManager questManager;
    private TalkManager talkManager;

    public void Action(GameObject scanObject){
        Action(scanObject, ACTION_ON_TOUCH);
    }

    public void Action(GameObject scanObject, string actionName)
    {
        switch(actionName){
            case ACTION_ON_TOUCH_LEFT_BOUNDARY:
            case ACTION_ON_TOUCH_RIGHT_BOUNDARY:
            case ACTION_ON_DIED:
                questManager.NotifyAction(scanObject, actionName);
                break;
            case ACTION_ON_TOUCH:
                this.scanObject = scanObject;
                this.scanObjectName = scanObject.name;

                ObjData objData = scanObject.GetComponent<ObjData>();

                if (objData == null)
                {
                    Debug.LogError("A game-object[" + scanObjectName + "] MUST have ObjData component.");
                    return;
                }

                if (TalkStart(objData.id))
                {
                    talkUI.SetActive(true); //대화창 활성화 상태에 따라 대화창 활성화 변경
                    this.isAction = true;
                }
                else
                {
                    Debug.Log("No talks found");
                }
                break;
        }
    }

    public void Action()
    {
        // isAction 이 true 이고, 스페이스바 이벤트이면..
        // 이 Action method 가 호출됨
        ObjData objData = scanObject.GetComponent<ObjData>();
        if(!TalkWith(objData.id)){
            talkUI.SetActive(isAction = false);
            //이벤트 호출!!!
        }
    }

    void TalkSetText(string talk)
    {
        string[] talkPart = talk.Split(':'); //구분자로 문장을 나눠줌  0: 대사 1:portraitIndex
        string talkContent = talkPart[0];

        int talker = 0;
        if(talkPart.Length > 1) {
            talker = int.Parse(talkPart[1].Trim());
        }
        string talkerName = talkManager.GetTalker(talker);

        if(talkerName == null) {
            Debug.Log("Invalid talker");
            return;
        }

        //대사 출력
        talkText.text = talkContent;
        UINameText.text = talkerName;
    }

    bool TalkStart(int objectId)
    {
        // 대화가 시작됨 => talkIndex=0
        Dictionary<string, object> questContext = questManager.GetQuestContext(sceneName, objectId, talkIndex=0);
        QuestHandler questHandler = questManager.GetQuestHandler(sceneName, objectId);
        int questStep = questHandler.GetQuestStep(questContext);
        string talk = talkManager.GetTalk(sceneName, objectId + questStep, this.talkIndex);

        //int questId = questManager.GetQuestTalkIndex(objectId);
        //string talk = talkManager.GetTalk(sceneName, objectId + questId, this.talkIndex);

        if(talk == null) {
            //Debug.Log("Not found talk with object-id[="+objectId+"] and quest-id[="+questId+"]");
            return false;
        }

        TalkSetText(talk);
        return true;
    }

    bool TalkWith(int objectId)
    {
        Dictionary<string, object> questContext = questManager.GetQuestContext(sceneName, objectId, ++talkIndex);
        QuestHandler questHandler = questManager.GetQuestHandler(sceneName, objectId);
        int questStep = questHandler.GetQuestStep(questContext);
        string talk = talkManager.GetTalk(sceneName, objectId + questStep, this.talkIndex);

        if(talk == null) {
            questHandler.OnAction(QuestHandler.EVENT_END_OF_TALK, questContext);
            return false;
        }

        TalkSetText(talk);
        return true;
    }

    void Start () 
    {
        sceneName = SceneManager.GetActiveScene().name;
        questManager = GameObject.FindObjectOfType<QuestManager>();
        talkManager = GameObject.FindObjectOfType<TalkManager>();

        SettingForMovingLimitX();

        GameObject spawnPoint = GameObject.Find("SpawnPoint");
        if(spawnPoint != null)
        {
            //Hierarchy View의 Spawn Point를 찾아 하위에 있는 모든 Transform 컴포넌트를 찾아옴
            points = spawnPoint.GetComponentsInChildren<Transform>();
    
            if(points.Length > 0)
            {
                //몬스터 생성 코루틴 함수 호출
                StartCoroutine(this.CreateMonster());
            }
        }
    }

    void Update()
    {
        if(Input.GetButtonDown("Cancel")) {  // Project Settings-Input에 Cancel있는지 확인
            if(subSettings.activeSelf)
                subSettings.SetActive(false);
            else
                subSettings.SetActive(true);
        }


        if(Input.GetKeyDown(KeyCode.I)) {
            if(Inspector.activeSelf)
                Inspector.SetActive(false);
            else
                Inspector.SetActive(true);
        }
    }

    IEnumerator CreateMonster()
    {
        //게임 종료 시까지 무한 루프
        while( !isGameOver )
        {
            //현재 생성된 몬스터 개수 산출
            int monsterCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;
 
            if(monsterCount < maxMonster)
            {
                //몬스터의 생성 주기 시간만큼 대기
                yield return new WaitForSeconds(createTime);
 
                //불규칙적인 위치 산출
                int idx = Random.Range(1, points.Length);
                //몬스터의 동적 생성
                int monsterIndex = Random.Range(0,monsterPrefab.Length);
                Instantiate(monsterPrefab[monsterIndex], points[idx].position, points[idx].rotation);
            }else
            {
                yield return null;
            }
        }
    }

    void SettingForMovingLimitX(){
        SetLimitMoveXMin("MovingAreaX1");
        SetLimitMoveXMax("MovingAreaX9");
    }

    public void SetLimitMoveXMin(string gameObjectName){
        GameObject movingAreaX = GameObject.Find(gameObjectName);
        if(movingAreaX!=null){
            Transform transform = movingAreaX.transform;
            limitMoveXMin = transform.position.x + transform.localScale.x/2;
        } else {
            Debug.LogWarning("not found game object - " + gameObjectName);
        }
    }

    public void SetLimitMoveXMax(string gameObjectName){
        GameObject movingAreaX = GameObject.Find(gameObjectName);
        if(movingAreaX!=null){
            Transform transform = movingAreaX.transform;
            limitMoveXMax = transform.position.x - transform.localScale.x/2;
        } else {
            Debug.LogWarning("not found game object - " + gameObjectName);
        }
    }
}
