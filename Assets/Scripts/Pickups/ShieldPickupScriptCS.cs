using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ====================================================
For a collectable object which comes back later
======================================================= */

public class ShieldPickupScriptCS : MonoBehaviour {

	private GameControllerScriptCS gameController;
	// private PickupsControllerCS pickupsController;
	private bool isCollectable = true;
	private AudioSource collectionSfx;
	public GameObject theParticle;
	public GameObject vfxObj;

	// Use this for initialization
	void Start () {
		//--find gameController so we can call functions
		gameController = GameObject.Find("GameController").GetComponent<GameControllerScriptCS>();
		
		// pickupsController = GameObject.Find("GameController").GetComponent<PickupsControllerCS>();
		
		collectionSfx = GetComponent<AudioSource>();

		NewPickup ();
	}
	
	void NewPickup () {

		vfxObj.SetActive(true);
		
		StartCoroutine(Blink());
	}

	IEnumerator Blink(){
		//--make blink for a bit
	    int blinkingAmt = 0;
		
		while(blinkingAmt < 8) {
	        yield return new WaitForSeconds(0.04f);
	        vfxObj.GetComponent<Renderer>().enabled = !vfxObj.GetComponent<Renderer>().enabled;
	        blinkingAmt++;
	    }

	    vfxObj.GetComponent<Renderer>().enabled = true;

	    isCollectable = true;
	}

	void OnTriggerEnter(Collider other) 
	{
		//--when player touches pickup
		if (other.tag == "Player" && isCollectable)
		{
			Debug.Log("shield touching player");
		    collectionSfx.Play();
		        
			ShieldControllerCS collidingPlayer  = other.gameObject.GetComponent<ShieldControllerCS>();
					
			if(collidingPlayer != null)
	        {   
	            collidingPlayer.ActivateShield();
	        } else {

	        	//--maybe we couldn't find the script because this player has a separate mesh collider
	        	//--so look for script on its parent
	        	collidingPlayer = other.transform.parent.gameObject.GetComponent<ShieldControllerCS>();

	        	if(collidingPlayer != null)
	        	{
	    			Debug.Log("getting playerscript of cogplayer");
	        		collidingPlayer.ActivateShield();
	        	}
	        }
			
			//--destory pickup, but schedule a new one
		    vfxObj.SetActive(false);
		    isCollectable = false;
		    theParticle.GetComponent<ParticleSystem>().emissionRate = 0;
		    
		    // yield WaitForSeconds (3);
		    
		    Destroy(gameObject);
		    
		}

	}

}










