using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldControllerCS : MonoBehaviour {

	public GameObject shieldObj;
	public bool shieldActive;
	private float forceAmt = 220f;
	private int abilityCountDownInitial = 10;
	private int abilityCountDown;

	// Use this for initialization
	void Start () {
		abilityCountDown = abilityCountDownInitial;

		shieldObj.SetActive(false);
	}
	
	public void ActivateShield(){
		shieldObj.SetActive(true);
		shieldActive = true;
		InvokeRepeating("Countdown",0,1);

		StartCoroutine(BlinkOn());
	}

	void Countdown(){
		if((abilityCountDown > 0) && (shieldActive == true)){
			abilityCountDown--;
			Debug.Log("shield countdown: "+abilityCountDown);
			
			if(abilityCountDown <=0){
				DisableShield();
			}
		}
	}

	void DisableShield(){

		Debug.Log("disable the shield");

		//-blink the shield out and disable it
		StartCoroutine(BlinkOff());
		shieldActive = false;
	}

	IEnumerator BlinkOn(){
		//--make blink on and stay on
	    int blinkingAmt = 0;
		
		while(blinkingAmt < 8) {
	        yield return new WaitForSeconds(0.04f);
	        shieldObj.GetComponent<Renderer>().enabled = !shieldObj.GetComponent<Renderer>().enabled;
	        blinkingAmt++;
	    }

	    shieldObj.GetComponent<Renderer>().enabled = true;

		shieldObj.SetActive(true);
	}

	IEnumerator BlinkOff(){
		//--make blink and stay off
	    int blinkingAmt = 0;
		
		while(blinkingAmt < 8) {
	        yield return new WaitForSeconds(0.04f);
	        shieldObj.GetComponent<Renderer>().enabled = !shieldObj.GetComponent<Renderer>().enabled;
	        blinkingAmt++;
	    }

	    shieldObj.GetComponent<Renderer>().enabled = false;

		shieldObj.SetActive(false);
	}

	void OnCollisionEnter (Collision collision) {

		if(shieldActive == true){
			ContactPoint contact = collision.contacts[0];

			GameObject other = contact.otherCollider.gameObject;

			Debug.Log("a shield collision has happened between "+contact.thisCollider.name +" and "+other.name);

			if(other.transform.name == transform.name){
				Debug.Log("NOPE");
				return;
			}
			if(other.tag == "Player" || other.tag == "Box") {

				//--create some sparks when we hit the other
				Vector3 pos = contact.point;
				Debug.Log(contact.thisCollider.name + " hit " + contact.otherCollider.name+"at position "+pos);

				GameObject sparkInstance = Instantiate(Resources.Load("ShieldSpark", typeof(GameObject)),
					pos, 
					Quaternion.identity
				) as GameObject;

				Destroy(sparkInstance,3);

				Rigidbody otherRb = other.GetComponent<Rigidbody>();

				if(!otherRb){
					//-maybe this object has a separate rb & collider - try the parent
					otherRb = other.transform.parent.gameObject.GetComponent<Rigidbody>();
				}

				if (otherRb) {
				
					// Apply force to the target object - calculate force

					float forceAmtLocal = forceAmt;
					Vector3 directionToOther = other.transform.position - gameObject.transform.position;
				
					if(other.tag == "Player") {
						//--apply more force when hitting player
						forceAmtLocal = forceAmt + 700;
					} 
					Debug.Log("---shield apply force of "+forceAmtLocal+" to "+other.name+" forcemat = "+forceAmt);

					otherRb.AddForce((directionToOther * forceAmtLocal), ForceMode.Impulse);
					otherRb.AddTorque(transform.right * 550, ForceMode.Impulse);
				}
			}
		}
	}
}










