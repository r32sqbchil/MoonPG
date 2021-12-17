using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CastleStageQuest8Handler : QuestHandler
{
    Portal GetPortalObject(){
        foreach(Portal portal in GameObject.FindObjectsOfType<Portal>(true)){
            return portal;
        }
        return null;
    }
}
