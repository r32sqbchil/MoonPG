using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleStageSetting : MonoBehaviour
{
    const string KEY_PORTAL_HIDE = "portal";

    private GameManager gameManager;
    private QuestHandler talk4Trigger;

    public void ActivatePortal(Portal portal, Dictionary<string, object> context){
        if(portal != null){
            portal.gameObject.SetActive(true);
            QuestHandler.SetContextValue(context, KEY_PORTAL_HIDE, true);
        }
    }

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        gameManager.SetLimitMoveXMax("MovingAreaX2");

        QuestManager questManager = GameManager.FindQuestManager();
        SetupPortal(questManager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideWall()
    {
        gameManager.SetLimitMoveXMax("MovingAreaX9");
    }

    void SetupPortal(QuestManager questManager)
    {
        Dictionary<string, object> context = questManager.GetQuestContext(Scene.SCENE_CASTLE_STAGE, 800, 0);
        if(!context.ContainsKey(KEY_PORTAL_HIDE))
        {
            Portal portal = GameObject.FindObjectOfType<Portal>();
            if(portal != null) {
                portal.gameObject.SetActive(false);
            }
        }
    }

}
