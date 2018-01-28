using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ====================================================
Creates a collectable object
======================================================= */

public class PickupsControllerCS : MonoBehaviour {

	private float yPos = 0.33f;
	private float appearAfter = 1;//10; //-- how many seconds pickup should show again
	public GameObject Pickup;
	public GameObject ShieldPickup;

	void Start () {
		SchedulePickup();
		InvokeRepeating("CreateShieldPickup",60f,60f); //--create a shield after a minute
	}

	void FixedUpdate () {

		//--debug
		if(Input.GetKey("s") ) {
			CreateShieldPickup();
		}
	}

	public void SchedulePickup(){
		Invoke("CreatePickup",appearAfter);
	}

	void CreatePickup() {
	
		//--select placement location 
		Vector3 location = new Vector3(
				Random.Range(-5.9f, 5.9f),
				yPos, 
				Random.Range(-5.9f, 5.9f)
			);
			
		float radius = .7f; //--radius of the hit area

		Collider[] objectsInRange  = Physics.OverlapSphere(location, radius);
	        
	    if(objectsInRange.Length > 0){
	    	//--this would collide with an object, so try again with a different location
	    	// CreatePickup();
	    	Invoke("CreatePickup",0.5f); 
	    }else {

			//--there's nothing to collide with here, so create pickup
	    	GameObject pickupInstance  = Instantiate(
	    		Pickup,
				location, 
				transform.rotation
			);	
	    }
	}

	void CreateShieldPickup() {

		//--create a new shield pickup if there isn't one already 

		//--first find out if there's one already
		var shieldPickup = GameObject.Find("ShieldPickup(Clone)");

		if(!shieldPickup){
			Debug.Log("create new shieldd pickup "+shieldPickup);

			//--select placement location 
			Vector3 location = new Vector3(
					Random.Range(-5.9f, 5.9f),
					yPos, 
					Random.Range(-5.9f, 5.9f)
				);
				
			float radius = .7f; //--radius of the hit area

			Collider[] objectsInRange = Physics.OverlapSphere(location, radius);
		        
		    if(objectsInRange.Length > 0){
		    	//--this would collide with an object, so try again with a different location
		    	// CreatePickup();
		    	Invoke("CreateShieldPickup",0.5f); 
		    } else {

				//--there's nothing to collide with here, so create pickup
		    	GameObject pickupInstance = Instantiate(
		    		ShieldPickup,
					location, 
					transform.rotation
				);	
		    }
		} else {
			Debug.Log("there is a shield pickup already");
		}
	}

}












