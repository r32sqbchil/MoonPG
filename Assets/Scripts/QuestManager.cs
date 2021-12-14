using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    

    void Awake()
    {
    }

    public void NotifyAction(GameObject actionObject, string actionName)
    {
        foreach (KeyValuePair<QuestHandler, Dictionary<string, object>> observer in updateObservers)
        {
            Dictionary<string, object> context = new Dictionary<string, object>(observer.Value);
            context[QuestHandler.KEY_OF_ACTION_OBJECT] = actionObject;
            context[QuestHandler.KEY_OF_NOTIFY_NAME] = actionName;
            observer.Key.OnAction(QuestHandler.EVENT_NOTIFY, context);
        }
    }

    void Update()
    {
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

    public QuestHandler GetQuestHandler(string sceneName, int objectId)
    {
        string keyName = sceneName + "$" + objectId;

        if(questHandlerMap.ContainsKey(keyName)){
            return questHandlerMap[keyName];
        } else {
            Debug.Log("Handler Not Found: " + keyName);
            return NOP;
        }
    }

    public void AddUpdateHandler(QuestHandler handler, Dictionary<string, object> context)
    {
        updateObservers[handler] = context;
    }

    public void RemoveUpdateHandler(QuestHandler handler)
    {
        updateObservers.Remove(handler);
    }

    void Start() 
    {
        questContextMap = new Dictionary<string, Dictionary<string, object>>();
        questHandlerMap = new Dictionary<string, QuestHandler>();
        updateObservers = new Dictionary<QuestHandler, Dictionary<string, object>>();
        
        questHandlerMap.Add(QuestHandler.TOWNSTAGE +"$100", new TownStageQuest1Handler());
        questHandlerMap.Add(QuestHandler.TOWNSTAGE +"$200", new TownStageQuest2Handler());
        questHandlerMap.Add(QuestHandler.TOWNSTAGE1+"$100", new TownStage1Quest1Handler());
        questHandlerMap.Add(QuestHandler.TOWNSTAGE1+"$300", new TownStage1Quest3Handler());
    }

}