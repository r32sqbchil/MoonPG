using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [HideInInspector] public bool dontDestroyOnLoad;

    void Awake() {
        QuestManager[] us = FindObjectsOfType<QuestManager>();
        if (us.Length == 1) {
            DontDestroyOnLoad(gameObject);
            dontDestroyOnLoad = true;
        }
        else
        {
            dontDestroyOnLoad = false;
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Debug.Log("QuestManager Started!");

        questContextMap = new Dictionary<string, Dictionary<string, object>>();
        questHandlerMap = new Dictionary<string, QuestHandler>();
        updateObservers = new Dictionary<QuestHandler, Dictionary<string, object>>();
        
        questHandlerMap.Add(Scene.SCENE_TOWN_STAGE +"$100", new TownStageQuest1Handler());
        questHandlerMap.Add(Scene.SCENE_TOWN_STAGE +"$200", new TownStageQuest2Handler());
        questHandlerMap.Add(Scene.SCENE_TOWN_STAGE1+"$100", new TownStage1Quest1Handler());
        questHandlerMap.Add(Scene.SCENE_TOWN_STAGE1+"$300", new TownStage1Quest3Handler());
        questHandlerMap.Add(Scene.SCENE_TOWN_STAGE3+"$400", new TownStage3Quest4Handler());
        questHandlerMap.Add(Scene.SCENE_MOUNTAIN_STAGE+"$200", new MountainStageQuest2Handler());
        questHandlerMap.Add(Scene.SCENE_MOUNTAIN_STAGE+"$400", new MountainStageQuest4Handler());
        questHandlerMap.Add(Scene.SCENE_MOUNTAIN_STAGE2+"$400", new MountainStage2Quest4Handler());
        questHandlerMap.Add(Scene.SCENE_MOUNTAIN_STAGE4+"$400", new MountainStage4Quest4Handler());
        questHandlerMap.Add(Scene.SCENE_MOUNTAIN_STAGE5+"$400", new MountainStage5Quest4Handler());
        questHandlerMap.Add(Scene.SCENE_MOUNTAIN_STAGE6+"$500", new MountainStage6Quest5Handler());
        questHandlerMap.Add(Scene.SCENE_MOUNTAIN_STAGE6+"$600", new MountainStage6Quest6Handler());
        questHandlerMap.Add(Scene.SCENE_TESTFIELD_STAGE+"$700", new TestFieldStageQuest7Handler());
        questHandlerMap.Add(Scene.SCENE_TESTFIELD_STAGE+"$800", new TestFieldStageQuest8Handler());
        questHandlerMap.Add(Scene.SCENE_TESTFIELD_STAGE2+"$700", new TestFieldStage2Quest7Handler());
        questHandlerMap.Add(Scene.SCENE_TESTFIELD_STAGE2+"$900", new TestFieldStage2Quest9Handler());
        questHandlerMap.Add(Scene.SCENE_FIELD_STAGE+"$700", new FieldStageQuest7Handler());
        questHandlerMap.Add(Scene.SCENE_FIELD_STAGE+"$800", new FieldStageQuest8Handler());
        questHandlerMap.Add(Scene.SCENE_FIELD_STAGE1+"$500", new FieldStage1Quest5Handler());
        questHandlerMap.Add(Scene.SCENE_CASTLE_STAGE+"$800", new CastleStageQuest8Handler());
        questHandlerMap.Add(Scene.SCENE_CASTLE_STAGE2+"$400", new CastleStage2Quest4Handler());
        questHandlerMap.Add(Scene.SCENE_CASTLE_STAGE3+"$400", new CastleStage3Quest4Handler());
    }

    public void NotifyAction(GameObject actionObject, string actionName)
    {
        if(updateObservers != null){
            foreach (QuestHandler handler in new List<QuestHandler>(updateObservers.Keys))
            {
                if(updateObservers.ContainsKey(handler)){
                    Dictionary<string, object> _origin;
                    Dictionary<string, object> context = new Dictionary<string, object>(_origin=updateObservers[handler]);
                    context[QuestHandler.KEY_OF_PERSISTENCE] = _origin;
                    context[QuestHandler.KEY_OF_ACTION_OBJECT] = actionObject;
                    context[QuestHandler.KEY_OF_NOTIFY_NAME] = actionName;
                    handler.OnAction(QuestHandler.EVENT_NOTIFY, context);
                }
            }
        }
    }

    void Update()
    {
        //Debug.Log("QuestManager::Update "+updateObservers.Count);
        if(updateObservers != null){
            foreach (QuestHandler handler in new List<QuestHandler>(updateObservers.Keys))
            {
                if(updateObservers.ContainsKey(handler)){
                    handler.OnAction(QuestHandler.EVENT_UPDATE, updateObservers[handler]);
                }
            }
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

        if(questHandlerMap != null && questHandlerMap.ContainsKey(keyName)){
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
}
