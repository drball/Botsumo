using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--uses oncollisionenter to push back another rb, with sparks created at point of collision 

public class BounceBackScript : MonoBehaviour {

	public float forceAmtInitial = 110; //force - before modifications
	public float forceAmt;
	public GameObject Spark;
	public CameraShake CameraShake;
	private Rigidbody otherRb;

	// Use this for initialization
	void Start () {
		forceAmt = forceAmtInitial;
		CameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();

	}
	
	public void ChangeForceAmt (float cogSpeed){
		//--called when player collects pickup or pickup times out - based on speed of cog
		forceAmt = cogSpeed * 0.50f;
		Debug.Log("change force amt = "+forceAmt+". cogspeed = "+cogSpeed);
	}

	public void ResetForceAmt(){
		// Debug.Log("reset forceamt");
		forceAmt = forceAmtInitial;
	}

	// void FixedUpdate () {

	// 	//--debug
	// 	if(Input.GetKey("up") ) {
	// 		Debug.Log("forcemt = "+forceAmt);
	// 		forceAmt += 10f;
	// 		Debug.Log("new forcemt = "+forceAmt);
	// 	}
	// }

	void OnCollisionEnter (Collision collision) 
	{

		ContactPoint contact = collision.contacts[0];

		GameObject other = contact.otherCollider.gameObject;

		Debug.Log("a collision has happened between "+contact.thisCollider.name +" and "+other.name);
		
		//--is the collider a sub-collider of the same object? 
		if(transform.parent != null){
			if(other.transform.name == transform.parent.name){
				// Debug.Log("collided with parent");
				return;
			}
		}
		

		//--see if this is hitting another player - or an obstacle (layer 12)
		if(other.CompareTag("Player") || other.layer == 12) {

			//--create some sparks when we hit the other
			Vector3 pos = contact.point;
			// Debug.Log(contact.thisCollider.name + " hit " + contact.otherCollider.name+"at position "+pos);

			GameObject sparkInstance = Instantiate(Spark,
				pos, 
				Quaternion.identity
			);

			Destroy(sparkInstance,3);

			otherRb = other.GetComponent<Rigidbody>();

			if(!otherRb){
				//-maybe this object has a separate rb & collider - try the parent
				otherRb = other.transform.parent.gameObject.GetComponent<Rigidbody>();
			}

			if (otherRb) {
			
				// Apply force to the target object - calculate force

				float forceAmtLocal = forceAmt;
				Vector3 directionToOther = other.transform.position - gameObject.transform.position;

				if(forceAmtLocal > 450f){
					CameraShake.Shake();
				}
				
				if(other.tag == "Player") {
					//--apply more force when hitting player
					forceAmtLocal = forceAmt + 300;
				} 
				// Debug.Log("---cog apply force of "+forceAmtLocal+" to "+other.name+" forceamt = "+forceAmt);

				otherRb.AddForce((directionToOther * forceAmtLocal), ForceMode.Impulse);
				otherRb.AddTorque(transform.up * 450, ForceMode.Impulse);

				
				
			}
		}
	}
}
