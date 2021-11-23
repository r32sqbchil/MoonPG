using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{

    public float HP;

    public float maxHealth = 200f;

    public bool isHit;
    
    public int GoblinDamage;

    public Image healthBar;

    public Text statText;



    public void Start()
    {
        HP = maxHealth;
    }

    public void Update()
    {
        healthBar.fillAmount = HP / maxHealth;
        statText.text = HP + " " + "/" + " " + maxHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            isHit = true;
            if(HP > 0 )
            {
                HP -= GoblinDamage;
            }
            else if(HP <= 0)
            {
                Destroy(this.gameObject, 2f);
                this.gameObject.GetComponent<playermovement>().enabled = false;
            }
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            isHit = false;
            // Debug.Log("rhkdus");
        }
    }
    // public int HP;
    // public bool hit;

    // public GameObject HitBox1;
    // public GameObject HitBox2;

    // public bool leftlooking;
    // public bool righrlooking;

    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.RightArrow))
    //         {
    //             righrlooking = true;
    //             leftlooking =false;
    //         }

    //     if(Input.GetKeyDown(KeyCode.LeftArrow))
    //     {
    //         righrlooking = false;
    //         leftlooking = true;
    //     }

    //     HitBOXActive();


    // }

    // public GameManager manager;
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    // void HitBOXActive()
    // {
    //     if(Input.GetKey(KeyCode.X) && righrlooking = true)
    //     {
    //         HitBox1.SetActive(true);
    //     }
    //     else
    //     {
    //         HitBox1.SetActive(false);
    //     }
    // }
}
