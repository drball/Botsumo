using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityScriptCS : MonoBehaviour {

	public bool abilityActive = false;
	private PlayerScriptCS PlayerScript;
	public int abilityCountDown;
	private GameObject vfxObj;
	private int abilityCountDownInitial= 10;

	// Use this for initialization
	void Start () {
		abilityCountDown = abilityCountDownInitial;
		PlayerScript = GetComponent<PlayerScriptCS>();
		vfxObj = PlayerScript.vfxObj;
		InvokeRepeating("Countdown", 0, 1);
	}
	
	void FixedUpdate () {

		//--debug
		if(Input.GetKey("a") ) {
			ActivateAbility();
		}
	}

	void Countdown(){
		if((abilityCountDown > 0) && (abilityActive == true)){
			abilityCountDown--;
			
			if(abilityCountDown <=0){
				DisableAbility();
			}
		}
	}

	public void ActivateAbility () {

		abilityActive = true;
		Debug.Log("ability active");
		BroadcastMessage("ActivateAbilityBroadcast");
		
		//--pause player for a bit - whilst flashing
		PlayerScript.alive = false;

	    abilityCountDown = abilityCountDownInitial;

		//--make player blink for a bit
		int blinkingAmt = 0;
		
		while(blinkingAmt < 8) {
	        // yield WaitForSeconds(0.05);

	        if(vfxObj.activeSelf == true){
	        	vfxObj.SetActive(false);
	    	} else {
	    		vfxObj.SetActive(true);
	    	}
	        
	        blinkingAmt++;
	    }
	    
	    vfxObj.SetActive(true);
	    
	    PlayerScript.alive = true;
	}

	public void DisableAbility() {

		abilityActive = false;

		Debug.Log("back to normal");

		SendMessage("DisableAbilityBroadcast");
		
		//--make player blink for a bit
		int blinkingAmt = 0;
		
		while(blinkingAmt < 8) {
	        // yield WaitForSeconds(0.05);
	    
			if(vfxObj.activeSelf == true){
	        	vfxObj.SetActive(false);
	    	}else {
	    		vfxObj.SetActive(true);
	    	}
	        
	        blinkingAmt++;
	    }
	    
	    vfxObj.SetActive(true);
	}
}

















