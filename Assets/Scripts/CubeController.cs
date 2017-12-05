﻿using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {

	public GameObject[] currentCubes;

	// Use this for initialization
	void Start () {
		CheckCubeAmt();

		InvokeRepeating("CheckCubeAmt", 10, 10);
	}
	

	public void CheckCubeAmt() {
		

		// yield return new WaitForSeconds(5);

		//--check how many cubes are remaining
		currentCubes = GameObject.FindGameObjectsWithTag("Box");
		Debug.Log("there are "+currentCubes.Length+" cubes");

		//--if less than 5, create another  cube
		if(currentCubes.Length < 8){

			GameObject instance = Instantiate(Resources.Load("Cube", typeof(GameObject)), 
				new Vector3(
					Random.Range(-4.5f, 5f),
					10f,
					Random.Range(-5.9f, 5.9f)
				),
				Random.rotation
			) as GameObject;

			Debug.Log("new cube has arrived");

			if(currentCubes.Length < 1){
				Debug.Log("all cubes gone");

				//--create a new shield pickup if there isn't one already 
				SendMessage("CreateShieldPickup");

			}
		}

		

	}

    
}
