using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    CapsuleCollider2D col;
    public Fade fade;
    public int sceneNumber;

    public string sceneName;
    
    void Awake()
    {
        col = GetComponent<CapsuleCollider2D>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.UpArrow)) {
                fade.FadeIn();
                Invoke("SceneLoad", 1f);
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
