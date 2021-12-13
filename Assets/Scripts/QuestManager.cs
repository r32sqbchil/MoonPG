using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<int, QuestData> npcQuestMap = new Dictionary<int, QuestData>();

    void Awake()
    {
        GenerateData();
    }

    void GenerateData()
    {
        npcQuestMap.Add(1000, new QuestDataFor1000(1, 1000, "고블린을 잡아라", 0));
    }

    public void QuestStart(int npcId){
        if(npcQuestMap.ContainsKey(npcId)){
            npcQuestMap[npcId].AddQuestStatus();
        }
    }

    public QuestData GetQuestData(int npcId){
        if(npcQuestMap.ContainsKey(npcId)){
            return npcQuestMap[npcId];
        } else {
            return null;
        }
    }

    public void QuestComplete(int npcId, bool isStop){
        if(npcQuestMap.ContainsKey(npcId)){
            if(isStop){
                npcQuestMap[npcId].ResetQuestStatus();
            } else {
                npcQuestMap[npcId].AddQuestStatus();
            }
        }
    }

    public int GetQuestTalkIndex(int npcId) // Npc Id를 받아 퀘스트 번호를 반환하는 함수 
    {
        if(npcQuestMap.ContainsKey(npcId)){
            return npcQuestMap[npcId].GetQuestStatus();
        } else {
            return 0;
        }
    }

    public void NotifyEvent(Object eventData){
        foreach(KeyValuePair<int, QuestData> item in npcQuestMap) {
            QuestData questData = item.Value;
            if(questData.isDoingQuest()) {
                questData.NotifyEvent(eventData);
            }
        }

        foreach(KeyValuePair<QuestHandler, Dictionary<string, object>> observer in updateObservers){
            Dictionary<string, object> context = new Dictionary<string, object>(observer.Value);
            context[QuestHandler.KEY_OF_EVENT_DATA] = eventData;
            observer.Key.OnAction(QuestHandler.EVENT_NOTIFY, context);
        }
    }

    void Update()
    {
        if(npcQuestMap != null){
            foreach(KeyValuePair<int, QuestData> item in npcQuestMap) {
                QuestData questData = item.Value;
                if(questData.isDoingQuest()) {
                    questData.Update();
                }
            }
        }

        foreach(KeyValuePair<QuestHandler, Dictionary<string, object>> observer in updateObservers){
            observer.Key.OnAction(QuestHandler.EVENT_UPDATE, observer.Value);
        }
    }

    //================================================================

    private Dictionary<string, QuestHandler> questHandlerMap;
    private Dictionary<string, Dictionary<string, object>> questContextMap;

    private Dictionary<QuestHandler, Dictionary<string, object>> updateObservers;

    private static QuestHandler NOP = new QuestHandler();

    public Dictionary<string, object> GetQuestContext(string sceneName, int objectId, int talkIndex){
        string keyName = sceneName + "$" + objectId;

        if(!questContextMap.ContainsKey(keyName)){
            Dictionary<string, object> _context = new Dictionary<string, object>();
            _context[QuestHandler.KEY_OF_SCENE_NAME] = sceneName;
            _context[QuestHandler.KEY_OF_OBJECT_ID] = objectId;
            questContextMap.Add(keyName, _context);
        }

        Dictionary<string, object> context = questContextMap[keyName];
        context[QuestHandler.KEY_OF_TALK_INDEX] = talkIndex;
        return context;
    }

    public QuestHandler GetQuestHandler(string sceneName, int objectId){
        string keyName = sceneName + "$" + objectId;

        if(questHandlerMap.ContainsKey(keyName)){
            return questHandlerMap[keyName];
        } else {
            Debug.Log("Handler Not Found: " + keyName);
            return NOP;
        }
    }

    public void AddUpdateHandler(QuestHandler handler, Dictionary<string, object> context){
        updateObservers[handler] = context;
    }

    public void RemoveUpdateHandler(QuestHandler handler){
        updateObservers.Remove(handler);
    }

    void Start() {
        questContextMap = new Dictionary<string, Dictionary<string, object>>();
        questHandlerMap = new Dictionary<string, QuestHandler>();
        updateObservers = new Dictionary<QuestHandler, Dictionary<string, object>>();
        
        Quest100Handler quest100Handler = new Quest100Handler();
        questHandlerMap.Add("townstage$100", quest100Handler);
        questHandlerMap.Add("townstage1$100", quest100Handler);

        questHandlerMap.Add("townstage$200", new Quest200Handler());
        questHandlerMap.Add("townstage1$300", new Quest300Handler());
    }

}