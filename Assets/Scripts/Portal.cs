using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    CapsuleCollider2D col;
    public int sceneNumber;

    public string sceneName;
    Animator anim;
    bool transportingInPortal = false;
    
    void Awake()
    {
        col = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(!transportingInPortal && other.gameObject.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.UpArrow)) {
                transportingInPortal = true;
                anim.SetTrigger("InPortal");
                Fade fade = GameObject.FindObjectOfType<Fade>();
                if(fade) fade.Invoke("FadeIn", 1f);
                Invoke("SceneLoad", 2f);
            }
        }
    }

    void SceneLoad()
    {
        if(string.IsNullOrEmpty(sceneName)){
            if(sceneNumber > 0){
                SceneManager.LoadScene(sceneNumber);
            } else {
                Debug.LogWarning("Check your sceneNumber property of this portal");
            }
        } else {
            SceneManager.LoadScene(sceneName);
        }
    }
}
