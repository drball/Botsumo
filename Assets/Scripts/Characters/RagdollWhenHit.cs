using UnityEngine;
using System.Collections;

//-- is replaced with a ragdoll character
//-- requires a box collider on the parent object set to trigger

public class RagdollWhenHit : MonoBehaviour {

	private BoxCollider2D coll;
	public bool alive = true;
	public Vector2 initialPosition;
	public Renderer[] childRends;
	public bool doesReset = false;
    public GameObject RagdollRef; //--obj to clone when turning to ragdoll
    private GameObject newRagdoll;
    public bool canFall; //--can this guy turn to ragdoll if there's no collider underneath? 
    public bool onGround;
    public Transform RaycastBottom;
    private Vector3 force;

	// Use this for initialization
	void Start () {
		// rb = body.GetComponent<Rigidbody2D>();
		coll = GetComponent<BoxCollider2D>();
		// childRbs = GetComponentsInChildren<Rigidbody2D>( ) as Rigidbody2D[];
		childRends = GetComponentsInChildren<Renderer>( ) as Renderer[];
		initialPosition = transform.localPosition;
		// rend = GetComponent<Renderer>();
	}

	void Update(){

		//--check if there's ground underneath
		onGround = Physics2D.Linecast(transform.position, RaycastBottom.position, 1 << LayerMask.NameToLayer("Ground"));

		if(!onGround && canFall){
			MakeRagdoll();
		}

		//--debug
		if(Input.GetKey("up") ) {
			MakeRagdoll();
		}
			
	}
	

	void OnTriggerEnter(Collider other) {

		// Debug.Log("other = "+other.name);

        if (other.tag == "DynamicLand" || other.tag == "Pickuppable" || (other.tag == "PlayerVehicle" && gameObject.tag != "Player" || other.tag == "Bullet")){

            Vector3 otherVelocity = other.GetComponent<Rigidbody>().velocity;

            Debug.Log("collided with "+other.name+" mag = "+otherVelocity.magnitude);

            if(alive && otherVelocity.magnitude > 1){
            	force = otherVelocity * 17;
				MakeRagdoll();
            }

            // if(other.GetComponent<BulletScript>()){
            // 	other.GetComponent<BulletScript>().HitPlayer();	
            // }
        }
    }

    void Reset(){
    	gameObject.SetActive(true);
        alive = true;
    }


    void MakeRagdoll(){
        
        alive = false;

        gameObject.SetActive(false);

        newRagdoll = Instantiate(RagdollRef, transform.position, transform.rotation);

        Invoke("Reset",5);

        if(force.magnitude > 0){
        	// AddImpulseForce newRagdollScript = newRagdoll.GetComponent<AddImpulseForce>();
        	// newRagdollScript.AddForce(force);
         //    force = new Vector3(0,0);
        }

    }
 
}