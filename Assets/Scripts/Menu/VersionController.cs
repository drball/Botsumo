using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionController : MonoBehaviour {

	public bool paidVersion = false;

	void Awake () {

		//--because this is a singleton, we want only one
		if (FindObjectsOfType(GetType()).Length > 1)
		{
			//--destroy others like this
			Debug.Log("destroying this duplicate of LevelsController");
			Destroy(gameObject);
		}
	}

	void SwitchToPaid(){
		//--button has been pressed and the payment has gone through

	}
}