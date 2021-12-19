using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldStage2Setting : MonoBehaviour
{
    const string KEY_MONOLOGUE = "monologue";

    void Start()
    {
        QuestManager questManager = GameManager.FindQuestManager();
        SetupMonologue(questManager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupMonologue(QuestManager questManager)
    {
        Dictionary<string, object> context = questManager.GetQuestContext(Scene.SCENE_FIELD_STAGE2, 0, 0);
        if(!context.ContainsKey(KEY_MONOLOGUE))
        {
            context.Add(KEY_MONOLOGUE, true);
            Invoke("ShowMonologue", 1.0f);
        }
    }

    void ShowMonologue()
    {
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        Player player = GameObject.FindObjectOfType<Player>();
        gameManager.Action(player.gameObject);
    }
}
