using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownStageSetting : MonoBehaviour
{
    const string KEY_PORTAL = Scene.SCENE_TOWN_STAGE+"$Portal";

    public void ActivatePortal(Portal portal){
        if(portal != null){
            portal.gameObject.SetActive(true);
            PlayerPrefs.SetInt(KEY_PORTAL, 1);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey(Scene.SCENE_TOWN_STAGE)){
            Invoke("BeginThisGame", 1.0f);
            PlayerPrefs.SetInt(Scene.SCENE_TOWN_STAGE, 1);
        }
        if(!PlayerPrefs.HasKey(Scene.SCENE_TOWN_STAGE+"$Portal")){
            Portal portal = GameObject.FindObjectOfType<Portal>();
            if(portal != null) {
                portal.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BeginThisGame() {
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        Player player = GameObject.FindObjectOfType<Player>();
        gameManager.Action(player.gameObject);
    }
}
