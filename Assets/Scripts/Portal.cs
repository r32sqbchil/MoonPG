using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    CapsuleCollider2D col;
    
    void Awake()
    {
        col = GetComponent<CapsuleCollider2D>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.UpArrow))
            SceneManager.LoadScene(2);
        }
    }
}
