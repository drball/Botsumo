using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--ability for bot B

public class AlternateShooting : MonoBehaviour {

	public GameObject BulletEmitter1;
	public GameObject BulletEmitter2;
	public GameObject Bullet;
	public GameObject EmitterContainer; //--to get rotation
	private bool fireFromL = true; //--alternates whether fire from L or R
	public float fireRateNormal = 0.75f; //--rate is actually interval (secs) between firing. Low = faster
	private float fireRate;

	// Use this for initialization
	void Start () {
		fireRate = fireRateNormal;

		if(!EmitterContainer){
			EmitterContainer = transform.gameObject;
		}
		
	}
	
	// called by PlayerAbility using sendmessage
	void ActivateAbilityBroadcast(){
		Debug.Log("Activate yus!");
		InvokeRepeating("FireBullet", 0, fireRate);
	}

	void DisableAbilityBroadcast(){
		Debug.Log("Disable yus!");
		CancelInvoke("FireBullet");
	}

	void FireBullet() {

		if(BulletEmitter2){
			fireFromL = !fireFromL;
		}
		
		Vector3 EmitterPos;
		
		if( fireFromL == true ){
			EmitterPos = BulletEmitter1.transform.position;
			Debug.Log("fir from L");
		}else {
			EmitterPos = BulletEmitter2.transform.position;
			Debug.Log("fir from R");
		}
		
		GameObject bulletInstance = Instantiate(Bullet, EmitterPos, EmitterContainer.transform.rotation);
		Debug.Log("------new bullet rot "+EmitterContainer.transform.rotation);
		
		//--set the owner of this bullet
		// bulletInstance.GetComponent<BulletScript>().Owner = gameObject;

	}
}
