using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCS : MonoBehaviour {

	public float rotationSpeed = .5f;
	public float speed = 1f;
	public bool moving = false;
	private PlayerScriptCS PlayerController;
	private Rigidbody rb;


	void Start () {
		PlayerController = GetComponent<PlayerScriptCS>();

		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate () 
	{
		if ((moving == true) && (PlayerController.alive == true)){
		
			//-- stop it rotating
			rb.angularVelocity = Vector3.zero;
		
			//-- move forward
			rb.AddRelativeForce (Vector3.forward * speed, ForceMode.Impulse);

			transform.Translate(Vector3.forward * speed * Time.deltaTime);
		
			
			//--if gets too fast, constrain speed
			if (rb.velocity.magnitude > speed) 
			{
		    	//GetComponent.<Rigidbody>().velocity = GetComponent.<Rigidbody>().velocity.normalized * speed;
			}
		
		} else {
			
			//-- rotate the player
			transform.Rotate((Vector3.up * rotationSpeed) * Time.deltaTime);
		}

	}

	public void Move(bool localmoving) {
		//--called when the button is pressed or stopped pressing 
		moving = localmoving; //--assign to public var
		
		if((localmoving == false) && (PlayerController.alive == true)) {

			//--switch spin direction
			rotationSpeed = -rotationSpeed;
		}
		
		if(!PlayerController.hasMoved){
			PlayerController.hasMoved = true;

			PlayerController.HideInstruction();
		}

	}
}
