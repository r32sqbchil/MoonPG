using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldStageSetting : MonoBehaviour
{
    const string KEY_PORTAL_HIDE = "portal";

    public void ActivatePortal(Portal portal, Dictionary<string, object> context){
        if(portal != null){
            portal.gameObject.SetActive(true);
            QuestHandler.SetContextValue(context, KEY_PORTAL_HIDE, true);
        }
    }

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
        Dictionary<string, object> context = questManager.GetQuestContext(Scene.SCENE_FIELD_STAGE, 700, 0);
        if(!context.ContainsKey(KEY_PORTAL_HIDE))
        {
            Portal portal = GameObject.FindObjectOfType<Portal>();
            if(portal != null) {
                portal.gameObject.SetActive(false);
            }
        } else {
            Player player = GameObject.FindObjectOfType<Player>();
            GameObject returnPosition = GameObject.Find("ReturnPosition");
            if(player && returnPosition){
                Vector3 playerPosition = player.transform.position;
                playerPosition.x = returnPosition.transform.position.x;
                playerPosition.y = returnPosition.transform.position.y;
                player.transform.position = playerPosition;
            } else {
                Debug.LogWarning("Can't find Player or ReturnPosition");
            }
        }
    }
}
