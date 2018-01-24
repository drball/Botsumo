#pragma strict
/* ====================================================
For a collectable object which comes back later
======================================================= */

private var gameController : GameControllerScript;
private var gameControllerObj : GameObject;
private var pickupsController : PickupsController;
private var isCollectable : boolean = true;
private var collectionSfx : AudioSource;
public var theParticle : GameObject;
public var vfxObj : GameObject;

function Start () {
	//--find gameController so we can call functions
	gameControllerObj = GameObject.Find("GameController");
	gameController = gameControllerObj.GetComponent.<GameControllerScript>();
	pickupsController = gameControllerObj.GetComponent.<PickupsController>();
	
	collectionSfx = GetComponent.<AudioSource>();

	NewPickup ();

}

function NewPickup () {

	vfxObj.SetActive(true);
	
	//--come back and blink for a bit
	
	var blinkingAmt : int = 0;
	
	while(blinkingAmt < 8) {
        yield WaitForSeconds(0.04);
        vfxObj.GetComponent.<Renderer>().enabled = !vfxObj.GetComponent.<Renderer>().enabled;
        blinkingAmt++;
    }
    vfxObj.GetComponent.<Renderer>().enabled = true;
    isCollectable = true;
}


function OnTriggerEnter(other: Collider) 
{
	//--when player touches pickup
	if (other.tag == "Player" && isCollectable)
	{
		Debug.Log("pickyup touching player");
	    collectionSfx.Play();

	    //--random chance of airstrike OR give player ability 
	    var rand = Mathf.Floor(Random.Range(0, 22f)); //--make the 2nd number higher for less chance of airstrike
	    Debug.Log("rand"+rand);
	    if(rand <= 1){
    		//--it's airstrike 
    		Debug.Log("airstrike");
    		gameController.SendMessage("ActivateAirstrike");

	    } else {
    		//--it's the normal player ability
			var collidingPlayer : PlayerAbilityScript = other.gameObject.GetComponent.<PlayerAbilityScript>();
				
			if(collidingPlayer != null)
	        {   
	            collidingPlayer.ActivateAbility();
	        } else {
	        	//--maybe we couldn't find the script because this player has a separate mesh collider
	        	//--so look for script on its parent
	        	collidingPlayer = other.transform.parent.gameObject.GetComponent.<PlayerAbilityScript>();

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
	    theParticle.GetComponent.<ParticleSystem>().emissionRate = 0;
	    
	    yield WaitForSeconds (3); //--wait for particles to fade
	    
	    Destroy(gameObject);

		pickupsController.SchedulePickup();
	    
	}

}




