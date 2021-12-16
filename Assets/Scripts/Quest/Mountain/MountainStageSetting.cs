using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainStageSetting : MonoBehaviour
{
    const string KEY_PORTAL_HIDE = "portal";

    public void ActivatePortal(Portal portal, Dictionary<string, object> context){
        if(portal != null){
            portal.gameObject.SetActive(true);
            QuestHandler.SetContextValue(context, KEY_PORTAL_HIDE, true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        QuestManager questManager = GameManager.FindQuestManager();
        SetupPortal(questManager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupPortal(QuestManager questManager)
    {
        Dictionary<string, object> context = questManager.GetQuestContext(Scene.SCENE_MOUNTAIN_STAGE, 200, 0);
        if(!context.ContainsKey(KEY_PORTAL_HIDE))
        {
            Portal portal = GameObject.FindObjectOfType<Portal>();
            if(portal != null) {
                portal.gameObject.SetActive(false);
            }
        }
    }
}
