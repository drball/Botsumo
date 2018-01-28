using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScriptCS : MonoBehaviour {

	public GameControllerScriptCS GameController;
	public bool alive = true;
	public int score = 0;
	public bool hasMoved = false; //--use this to determine when to hide instruction
	public GameObject vfxObj;
	public int playerNum = 1;
	public string playerCharacter;

	private int count = 0;
	private float fallingYPos = -2f;
	private int badRotationTimer;
	private Vector3 startingPos;
	private Quaternion startingRotation;
	private GameObject Btn; //--the button for this player (used for hiding the instruction)
	private Rigidbody rb;

	void Start () {
		GameController = GameObject.Find("GameController").GetComponent<GameControllerScriptCS>();
		
		InvokeRepeating("Timer", 1, 1);
		
		//--save start locations to variables
		startingPos = transform.position;
		
		startingRotation = transform.rotation;
		
		Respot();

		rb = GetComponent<Rigidbody>();
		
		if(playerNum == 1){
			Btn = GameObject.Find("LInstruction");
		} else {
			Btn = GameObject.Find("RInstruction");
		}
	}


	void Update() {

		if ((transform.position.y < fallingYPos) && alive == true){
			
			alive = false;
					
			if(GameController.roundActive == true){

				GameController.EndRound();
			}
		}
	}

	public void Respot(){
		//--reset position
		transform.position = startingPos;
		
		//--reset rotation
		transform.rotation = startingRotation;
		
		//--make player blink for a bit
		// var blinkingAmt : int = 0;
		
		// while(blinkingAmt < 8) {
	 //        yield WaitForSeconds(0.06);
	    
		// 	if(vfxObj.activeSelf == true){
	 //        	vfxObj.SetActive(false);
	 //    	}else {
	 //    		vfxObj.SetActive(true);
	 //    	}
	        
	 //        blinkingAmt++;
	 //    }
	    
	    vfxObj.SetActive(true);
		
	}

	void Timer(){
		//--1 second cron
		//--count how long it's been on it's side.
		
		//Debug.Log("z = "+transform.eulerAngles.z+"x = "+transform.eulerAngles.x);
		if( ((transform.eulerAngles.x >= 70) && (transform.eulerAngles.x <= 300)) || ((transform.eulerAngles.z >= 70) && (transform.eulerAngles.z <= 300)) )
		{
			badRotationTimer++;
			// Debug.Log("-------------------bad rot. x="+transform.eulerAngles.x+" y="+transform.eulerAngles.y+" z ="+transform.eulerAngles.z);
		} else {
			badRotationTimer = 0;
		}
		
		//--restart if been on it's side for more than 3 seconds
		if((badRotationTimer > 3) && (alive == true)){
			badRotationTimer = 0;
			Respot();
		}
		
		//Debug.Log("rotTimer = "+badRotationTimer);
		
	}

	public void HideInstruction(){
		Debug.Log("hide btn for"+ Btn.name);

		//--fade the instruction out for this player's control btn
		Btn.GetComponent<Animator>().Play("FadeOut");
	}


}
