using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform[] points;
    public GameObject monsterPrefab;

    public float createTime;
    public int maxMonster = 1;

    public GameObject subSettings;
    public GameObject Inspector;
    
    public GameObject talkUI;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;

    public TalkManager talkManager;
    public int talkIndex;

    public string scanObjectName; 
    public Text UINameText;

    public QuestManager questManager;

    public bool isGameOver = false;

    public float limitMoveXMin = -3.07f;
    public float limitMoveXMax = 17.0f;

    private string sceneName;

    public void Action(GameObject scanObject)
    {
        this.scanObject = scanObject;
        this.scanObjectName = scanObject.name;

        ObjData objData = scanObject.GetComponent<ObjData>();
        
        if(objData == null) {
            Debug.LogError("A game-object["+scanObjectName+"] MUST have ObjData component.");
        }

        if(TalkStart(objData.id))
        {
            talkUI.SetActive(true); //대화창 활성화 상태에 따라 대화창 활성화 변경
            this.isAction = true;
        } else {
            Debug.Log("No talks found");
        }
    }

    public void Action()
    {
        ObjData objData = scanObject.GetComponent<ObjData>();
        if(!TalkWith(objData.id)){
            talkUI.SetActive(isAction = false);
        }
    }

    void TalkSetText(string talk)
    {
        string[] talkers = "슈라켄,어린마하,아줌마,스승".Split(',');

        string[] talkPart = talk.Split(':'); //구분자로 문장을 나눠줌  0: 대사 1:portraitIndex
        string talkContent = talkPart[0];
        int talker = 0;
        if(talkPart.Length > 1) {
            talker = int.Parse(talkPart[1].Trim());
        }

        if(talker<0 || talker >= talkers.Length) {
            Debug.Log("Invalid talker");
            return;
        }

        //대사 출력
        talkText.text = talkContent;
        UINameText.text = talkers[talker];
    }

    bool TalkStart(int objectId)
    {
        this.talkIndex = 0; // 대화가 시작됨을 표기

        int questId = questManager.GetQuestTalkIndex(objectId);
        string talk = talkManager.GetTalk(sceneName, objectId + questId, this.talkIndex);

        if(talk == null) {
            //Debug.Log("Not found talk with object-id[="+objectId+"] and quest-id[="+questId+"]");
            return false;
        }

        TalkSetText(talk);
        return true;
    }

    bool TalkWith(int objectId)
    {
        this.talkIndex++;

        int questId = questManager.GetQuestTalkIndex(objectId);
        string talk = talkManager.GetTalk(sceneName, objectId + questId, this.talkIndex);

        QuestCheck(objectId, questId, talkIndex);

        if(talk == null) {
            //Debug.Log("There is not text for talk");
            return false;
        }

        TalkSetText(talk);
        return true;
    }

    void QuestCheck(int npcId, int questId, int talkIndex){
        //Debug.Log("npcId:"+npcId+", questId:"+questId+", talkIndex:"+talkIndex+"");
        if(npcId == 1000 && questId == 0 && talkIndex == 1) {
            questManager.QuestStart(npcId);
        } else if(npcId == 1000 && questId == 10 && talkIndex == 2) {
            QuestData questData = questManager.GetQuestData(npcId);
            if(!questData.isDoingQuest()){
                questData.AddQuestStatus();
            }
        } else if(npcId == 1000 && questId == 30 && talkIndex == 2) {
            QuestData questData = questManager.GetQuestData(npcId);
            if(!questData.isDoingQuest()){
                questData.AddQuestStatus();
            }
        }
    }

    void Start () 
    {
        sceneName = SceneManager.GetActiveScene().name;
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
                Instantiate(monsterPrefab, points[idx].position, points[idx].rotation);
            }else
            {
                yield return null;
            }
        }
    }
}
