using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float movePower = 1f;
	public float jumpPower = 1f;

    private CapsuleCollider2D ChCollider;
	Rigidbody2D rigid;

	Vector3 movement;
	bool isJumping = false;
    
    Animator anim2;

	//---------------------------------------------------[Override Function]
	//Initialization
	void Start ()
	{
		rigid = gameObject.GetComponent<Rigidbody2D> ();
	}

	//Graphic & Input Updates	
	void Update ()
	{
		if (Input.GetButtonDown ("Jump")) {
			isJumping = true;
		}
        // Move ();
        // Jump ();
	}

	//Physics engine Updates
	void FixedUpdate ()
	{
		Move ();
		Jump ();
	}

	//---------------------------------------------------[Movement Function]

	void Move ()
	{		
		Vector3 moveVelocity= Vector3.zero;

		if (Input.GetAxisRaw ("Horizontal") < 0) {
			moveVelocity = Vector3.left;
		}
			
		else if(Input.GetAxisRaw ("Horizontal") > 0){
			moveVelocity = Vector3.right;
		}	

		transform.position += moveVelocity * movePower * Time.deltaTime;
	}

	void Jump ()
	{
		if (!isJumping)
			return;

		//Prevent Velocity amplification.
		rigid.velocity = Vector2.zero;

		Vector2 jumpVelocity = new Vector2 (0, jumpPower);
		rigid.AddForce (jumpVelocity, ForceMode2D.Impulse);

		isJumping = false;

        // if(Input.GetButtonDown("Jump"))
        // {
        //     rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        // }
	}
}
