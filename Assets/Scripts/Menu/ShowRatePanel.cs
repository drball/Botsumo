using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRatePanel : MonoBehaviour {

	public GameObject Panel;
	public int timesPlayed;
	public int askedToRate; //--can't be a bool. So just 0 or 1

	// Use this for initialization
	void Start () {

		timesPlayed = PlayerPrefs.GetInt("timesPlayed");
		askedToRate = PlayerPrefs.GetInt("askedToRate");

		if((timesPlayed > 2) && (askedToRate != 1)){
			Debug.Log("played over 3 times");
			Panel.SetActive(true);
			PlayerPrefs.SetInt("askedToRate", 1);
		}
	}
	
}
