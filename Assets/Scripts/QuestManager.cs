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
    }

    //================================================================

    private Dictionary<string, QuestHandler> questHandlerMap;
    private static QuestHandler NOP = new QuestHandler();

    public object GetQuestContext(string sceneName, int objectId, int talkIndex){
        return new {sceneName, objectId, talkIndex};
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

    void Start() {
        questHandlerMap = new Dictionary<string, QuestHandler>();
        questHandlerMap.Add("townstage$100", new Quest100Handler());
    }

}