using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirstrikeController : MonoBehaviour {

	public Transform spawnPos;
	public GameObject Bullet;
	public GameObject AirstrikeObj;
	public bool active;

	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate () {

		//--debug
		if(Input.GetKey("up") ) {
			ActivateAirstrike();
		}
	}
	
	void ActivateAirstrike(){

		if(active != true){
			active = true;
			AirstrikeObj.SetActive(true);
			Debug.Log("aistrike pos "+spawnPos.position);
			// Debug.Log("aistrike rot "+spawnPos.rotation);


			float initialDelay = 1f;

			Invoke("CreateBullet", initialDelay);
			Invoke("CreateBullet", initialDelay + 0.25f);
			Invoke("CreateBullet", initialDelay + 0.5f);
			Invoke("CreateBullet", initialDelay + 0.75f);
			Invoke("CreateBullet", initialDelay + 1f);
			Invoke("CreateBullet", initialDelay + 1.25f);
			Invoke("CreateBullet", initialDelay + 1.5f);
			Invoke("CreateBullet", initialDelay + 1.6f);
			Invoke("CreateBullet", initialDelay + 1.7f);
			Invoke("CreateBullet", initialDelay + 1.75f);
			Invoke("CreateBullet", initialDelay + 2f);
			Invoke("CreateBullet", initialDelay + 2.25f);
			Invoke("CreateBullet", initialDelay + 2.5f);
			Invoke("CreateBullet", initialDelay + 2.75f);
			Invoke("CreateBullet", initialDelay + 3f);

			Invoke("EndAirstrike", 3);
		}
		
	}

	void CreateBullet(){
		Vector3 bulletStartPos = new Vector3(
			Random.Range(spawnPos.position.x - 7.5f, spawnPos.position.x + 7f),
			spawnPos.position.y,
			Random.Range(spawnPos.position.z - 7f, spawnPos.position.z + 7f)
		);
		Instantiate(Bullet, bulletStartPos, spawnPos.rotation);
	}

	void EndAirstrike(){
		active = false;
		AirstrikeObj.SetActive(false);
	}
}
