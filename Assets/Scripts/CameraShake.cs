using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	public float customIntensity = .11f;
	public float customDecay = 0.005f;

	private Vector3 originPosition;
	private Quaternion originRotation;
	private float shake_decay;
	private float shake_intensity;
	public bool constantShaking;
	private float constantShakeIntensity;
	 
	// void Start(){
	// 	InvokeRepeating("Shake",2,2);
	// }

	// void FixedUpdate () {

	// 	//--debug
	// 	if(Input.GetKey("up") ) {
	// 		Shake();
	// 	}
	// }
	 
	void Update(){

	   originPosition = transform.position;

	   if(shake_intensity > 0){
	      originPosition = transform.position;
	      transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
	      transform.rotation = new Quaternion(
	         originRotation.x + Random.Range(-shake_intensity,shake_intensity)*.2f,
	         originRotation.y + Random.Range(-shake_intensity,shake_intensity)*.2f,
	         originRotation.z + Random.Range(-shake_intensity,shake_intensity)*.2f,
	         originRotation.w + Random.Range(-shake_intensity,shake_intensity)*.2f
	      );
	      shake_intensity -= shake_decay;
	   }

	   if(constantShaking == true){

	      transform.position = originPosition + Random.insideUnitSphere * constantShakeIntensity;
	      transform.rotation = new Quaternion(
	         originRotation.x + Random.Range(-constantShakeIntensity,constantShakeIntensity)*.2f,
	         originRotation.y + Random.Range(-constantShakeIntensity,constantShakeIntensity)*.2f,
	         originRotation.z + Random.Range(-constantShakeIntensity,constantShakeIntensity)*.2f,
	         originRotation.w + Random.Range(-constantShakeIntensity,constantShakeIntensity)*.2f
	      );
	      
	      constantShakeIntensity += 0.00002f;
	   }
	}
	 
	public void Shake(){
	   // originPosition = transform.position;
	   originRotation = transform.rotation;
	   shake_intensity = customIntensity;
	   shake_decay = customDecay;
	}

	void LongShake(){
	   constantShaking = true;
	   originRotation = transform.rotation;
	   constantShakeIntensity = 0.001f;
	}
}
