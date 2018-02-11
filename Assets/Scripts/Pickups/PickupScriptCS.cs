using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	/* ====================================================
	For a collectable object which comes back later
	======================================================= */

public class PickupScriptCS : MonoBehaviour {

	private GameControllerScriptCS gameController;
	private GameObject gameControllerObj;
	private PickupsControllerCS pickupsController;
	private bool isCollectable = true;
	private AudioSource collectionSfx;
	public GameObject theParticle;
	public GameObject vfxObj;


	void Start () {
		//--find gameController so we can call functions
		gameControllerObj = GameObject.Find("GameController");
		gameController = gameControllerObj.GetComponent<GameControllerScriptCS>();
		pickupsController = gameControllerObj.GetComponent<PickupsControllerCS>();
		
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
			Debug.Log("pickyup touching player");
		    collectionSfx.Play();

		    //--random chance of airstrike OR give player ability 
		    var rand = Mathf.Floor(Random.Range(0, 22f)); //--make the 2nd number higher for less chance of airstrike
		    Debug.Log("rand "+rand);
		    if(rand <= 1){
	    		//--it's airstrike 
	    		Debug.Log("airstrike");
	    		gameController.SendMessage("ActivateAirstrike");

		    } else {
	    		//--it's the normal player ability
				PlayerAbilityScriptCS collidingPlayer = other.gameObject.GetComponent<PlayerAbilityScriptCS>();
					
				if(collidingPlayer != null)
		        {   
        			collidingPlayer.ActivateAbility();
		        } else {
		        	//--maybe we couldn't find the script because this player has a separate mesh collider
		        	//--so look for script on its parent
		        	collidingPlayer = other.transform.parent.gameObject.GetComponent<PlayerAbilityScriptCS>();

		        	if(collidingPlayer != null)
		        	{
		    			Debug.Log("getting playerscript of cogplayer");
		        		collidingPlayer.ActivateAbility();
		        	}
		        }
		    }
		        
			//--destory pickup, but schedule a new one
		    vfxObj.SetActive(false);
		    isCollectable = false;
		    theParticle.GetComponent<ParticleSystem>().emissionRate = 0;
		    
		    // yield WaitForSeconds (3); //--wait for particles to fade
		    
		    Destroy(gameObject);

			pickupsController.SchedulePickup();
		    
		}

	}
}









