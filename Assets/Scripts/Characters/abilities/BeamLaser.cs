using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamLaser : MonoBehaviour {

	public float beamRange = 100f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if( Physics.Raycast(transform.position, transform.forward, out hit, beamRange)) {
			Debug.Log("beam hit "+hit.transform.name);
		}

	}
}
