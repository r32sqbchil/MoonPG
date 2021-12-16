using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownStageSetting : MonoBehaviour
{
    const string KEY_MONOLOGUE = "monologue";
    const string KEY_PORTAL_HIDE = "portal";

    public void ActivatePortal(Portal portal, Dictionary<string, object> context){
        if(portal != null){
            portal.gameObject.SetActive(true);
            context[KEY_PORTAL_HIDE] = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        QuestManager questManager = GameObject.FindObjectOfType<QuestManager>();

        SetupMonologue(questManager);
        SetupPortal(questManager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupMonologue(QuestManager questManager)
    {
        Dictionary<string, object> context = questManager.GetQuestContext(Scene.SCENE_TOWN_STAGE, 0, 0);
        if(!context.ContainsKey(KEY_MONOLOGUE))
        {
            context.Add(KEY_MONOLOGUE, true);
            Invoke("BeginThisGame", 1.0f);
        }
    }
    void SetupPortal(QuestManager questManager)
    {
        Dictionary<string, object> context = questManager.GetQuestContext(Scene.SCENE_TOWN_STAGE, 200, 0);
        if(!context.ContainsKey(KEY_PORTAL_HIDE))
        {
            Portal portal = GameObject.FindObjectOfType<Portal>();
            if(portal != null) {
                portal.gameObject.SetActive(false);
            }
        }
    }

    void BeginThisGame() {
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        Player player = GameObject.FindObjectOfType<Player>();
        gameManager.Action(player.gameObject);
    }
}
