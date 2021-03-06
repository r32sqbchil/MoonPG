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


    public GameObject subSettings;
    public GameObject Inspector;
    
    public GameObject talkUI;
    public Text talkText;
    public Animator talkAnim;
    [HideInInspector] public GameObject scanObject;
    [HideInInspector] public bool isAction;
    
    [HideInInspector] public int talkIndex;

    [HideInInspector] public string scanObjectName; 
    public Text UINameText;

    [HideInInspector] public bool isGameOver = false;
    public Fade fade;

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
                    talkUI.SetActive(true); //????????? ????????? ????????? ?????? ????????? ????????? ??????
                    SetTalkingMark(objData, true);
                    this.isAction = true;
                }
                else
                {
                    Debug.Log("No talks found");
                }
                break;
        }
    }

    void SetTalkingMark(ObjData objData, bool value){
        Animator anim = FindTalkMarkOfNPC(objData);
        if(anim){
            anim.SetBool("isTalking", value);
        }
    }

    public static ObjData FindObjData(int objId){
        foreach(ObjData objData in GameObject.FindObjectsOfType<ObjData>()){
            if(objData.id == objId) return objData;
        }
        return null;
    }

    public static Animator FindTalkMarkOfNPC(ObjData objData){
        foreach(Animator anim in objData.GetComponentsInChildren<Animator>()){
            if(anim.name == "TalkMark"){
                return anim;
            }
        }
        if(objData.id > 0){
            Debug.Log("Can't find TalkMark in NPC GameObject");
        }
        return null;
    }

    public void Action()
    {
        // isAction ??? true ??????, ??????????????? ???????????????..
        // ??? Action method ??? ?????????
        ObjData objData = scanObject.GetComponent<ObjData>();
        if(!TalkWith(objData.id)){
            talkUI.SetActive(isAction = false);
            SetTalkingMark(objData, false);
            //????????? ??????!!!
        }
    }

    void TalkSetText(string talk)
    {
        string[] talkPart = talk.Split(':'); //???????????? ????????? ?????????  0: ?????? 1:portraitIndex
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

        //?????? ??????
        talkText.text = talkContent;
        UINameText.text = talkerName;
    }

    bool TalkStart(int objectId)
    {
        // ????????? ????????? => talkIndex=0
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

    public static QuestManager FindQuestManager()
    {
        foreach(QuestManager qm in GameObject.FindObjectsOfType<QuestManager>())
        {
            if(qm.dontDestroyOnLoad)
            {
                return qm;
            }
        }
        Debug.LogWarning("Not Found QuestManager");
        return null;
    }

    void Start () 
    {
        sceneName = SceneManager.GetActiveScene().name;
        talkManager = GameObject.FindObjectOfType<TalkManager>();
        questManager = FindQuestManager();

        SettingForMovingLimitX();

        fade.FadeOut();
    }

    void Update()
    {
        if(Input.GetButtonDown("Cancel")) {  // Project Settings-Input??? Cancel????????? ??????
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
