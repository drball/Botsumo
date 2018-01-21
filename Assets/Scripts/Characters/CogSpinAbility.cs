using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogSpinAbility : MonoBehaviour {

	private float cogSpeedInitial; //--get this from spinScript
	// public GameObject cog;
	private Collider cogCollider;
	public rotate SpinScript; //--yes the script is called "rotate"
	public BounceBackScript bounceBackScript;
	private float cogSpinMax = 950f;
	private float cogSpinCollider = 950f;
	public int level = 1;
	public AudioSource spinAudio;
	private float spinAudioVolume;
	private float spinAudioPitchInitial = 0.5f;
	private float spinAudioPitch;

	// Use this for initialization
	void Start () {

		cogSpeedInitial = SpinScript.spinZ;
		spinAudioVolume = 0f;
		spinAudioPitch = spinAudioPitchInitial;
	}

	void Update(){
		spinAudio.volume = Mathf.Lerp (spinAudio.volume, spinAudioVolume, 2f * Time.deltaTime);
		spinAudio.pitch = Mathf.Lerp (spinAudio.pitch, spinAudioPitch, 2 * Time.deltaTime);

		// Debug.Log("lerp volume "+spinAudio.volume+" from "+spinAudioVolume + " to "+spinAudioNewVolume);
	}

	// void FixedUpdate () {

	// 	//--debug
	// 	if(Input.GetKey("up") ) {
	// 		ActivateAbilityBroadcast();
	// 	}
	// }
	
	// called by PlayerAbility using sendmessage
	void ActivateAbilityBroadcast(){
		//--increase speed of spinning cog
		level = level += 1;
		SpinScript.spinZ = SpinScript.spinZ + cogSpeedInitial;
		if(SpinScript.spinZ > cogSpinMax) {
			SpinScript.spinZ = cogSpinMax;
		}
		Debug.Log("new cogspeed is "+SpinScript.spinZ+" level = "+level);
		bounceBackScript.ChangeForceAmt(SpinScript.spinZ);
		spinAudioVolume = spinAudio.volume;
		// spinAudioPitch = spinAudio.pitch;

		if (level == 2){
			spinAudioVolume = 0.15f;
			spinAudioPitch = 0.7f;
		} else if (level == 3){
			spinAudioVolume = 0.2f;
			spinAudioPitch = 1f;
		} else if (level == 4){
			spinAudioVolume = 0.25f;
			spinAudioPitch = 1.2f;
		} else if (level == 5){
			spinAudioVolume = 0.3f;
			spinAudioPitch = 1.4f;
		} else if (level >= 6){
			spinAudioVolume = 0.35f;
			spinAudioPitch = 1.6f;
		}
	}

	void DisableAbilityBroadcast(){
		level = 1;
		spinAudioVolume = 0f;
		// spinAudioNewPitch = spinAudioPitchInitial;
		SpinScript.spinZ = cogSpeedInitial;
		bounceBackScript.ResetForceAmt();
	}
}
