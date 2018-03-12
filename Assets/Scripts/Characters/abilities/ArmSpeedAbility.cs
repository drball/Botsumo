using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmSpeedAbility : MonoBehaviour {

	private float armSpeedInitial;
	private float armForceInitial;
	public Animator Animator;
	public BounceBackScript bounceBackScriptL;
	public BounceBackScript bounceBackScriptR;
	public int level = 1;

	// Use this for initialization
	void Start () {
		armSpeedInitial = 1f;
		Animator.speed = armSpeedInitial;
	}
	
	// called by PlayerAbility using sendmessage
	void ActivateAbilityBroadcast(){

		//--increase speed of arm animation
		level = level += 1;

		if (level == 2){
			Animator.speed = 2f;
			bounceBackScriptL.IncreaseForceAmt(1.2f);
			bounceBackScriptR.IncreaseForceAmt(1.2f);

		} else if (level == 3){
			Animator.speed = 2.75f;
			bounceBackScriptL.IncreaseForceAmt(1.35f);
			bounceBackScriptR.IncreaseForceAmt(1.35f);

		} else if (level == 4){
			Animator.speed = 3.5f;
			bounceBackScriptL.IncreaseForceAmt(1.45f);
			bounceBackScriptR.IncreaseForceAmt(1.45f);

		} else if (level == 5){
			Animator.speed = 4.25f;
			bounceBackScriptL.IncreaseForceAmt(1.55f);
			bounceBackScriptR.IncreaseForceAmt(1.55f);
	
		} else if (level >= 6){
			Animator.speed = 5f;
			bounceBackScriptL.IncreaseForceAmt(1.65f);
			bounceBackScriptR.IncreaseForceAmt(1.65f);
			level = 6; //--this is the max
		}
	}

	void DisableAbilityBroadcast(){
		//--reset anim speed to initial
		level = 1;
		Animator.speed = armSpeedInitial;
		bounceBackScriptL.ResetForceAmt();
		bounceBackScriptR.ResetForceAmt();
	}
}
